using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StickClass : AnimalClass
{
    protected Collider attachedStick;                               // 현재 붙어있는 stick
    private List<Collider> stick_collider = new List<Collider>();   // 충돌 중인 stick 리스트
    protected float shakeSpeed;                                     // stick 속도 최댓값 => 이 속도를 넘어가면 붙어있는 물체가 떨어짐.
    protected float short_distance_s;                               // 물체로부터 제일 가까운 stick까지의 거리


    public void Awake()
    {
        attachedStick = null;
        shakeSpeed = 5f;
        short_distance_s = 5000;

    }




    public void ThisStick()
    {
        if (is_alive) return;
        ShakeStick();
    }

    public void ShakeStick()    // stick 의 속도가 shakeSpeed를 넘어가면 붙어있던 물체가 떨어짐.
    {
        if (attachedStick != null)
        {
            Rigidbody stick_rb = attachedStick.GetComponent<Rigidbody>();
            if (stick_rb != null && stick_rb.linearVelocity.magnitude > shakeSpeed)
            {
                DetachStick();
            }

        }
    }

    public void UpdateAttachedStick()   //제일 가까운 stick에 부착
    {
        if (stick_collider.Count == 0)
        {
            return;
        }

        Collider short_stick = null;
        foreach (Collider stick in stick_collider)
        {
            float Distance_s = Vector3.Distance(this.transform.position, stick.transform.position);
            if (Distance_s < short_distance_s)
            {
                short_distance_s = Distance_s;
                short_stick = stick;
            }


        }

        if (short_stick != null && short_stick != attachedStick)
        {
            DetachStick();
            AttachStick(short_stick);

        }


    }

    public void AttachStick(Collider stick)         // 물체를 stick 에 부착시킴.
    {
        attachedStick = stick;
        Rigidbody stick_rb = attachedStick.GetComponent<Rigidbody>();
        if (stick_rb != null && animal_rb != null)
        {
            this.transform.position = GetCollisionPosition(stick);
            this.transform.SetParent(stick.transform);
            animal_rb.isKinematic = true;
        }

    }

    public void DetachStick()                       // 물체가 stick에서 떨어짐.
    {
        if (attachedStick != null)
        {
            Rigidbody stick_rb = attachedStick.GetComponent<Rigidbody>();
            if (stick_rb != null)
            {
                this.transform.SetParent(null);
                animal_rb.isKinematic = false;
            }
            attachedStick = null;
        }
    }

    public Vector3 GetCollisionPosition(Collider stick)         // stick과 물체의 충돌 지점
    {
        return stick.bounds.center;
    }


    public virtual void OnCollisionEnter(Collision collision)
    {
        if (!is_alive && collision.gameObject.CompareTag("CanBuild") && collision.gameObject.name.Contains("WoodStick"))
        {
            stick_collider.Add(collision.collider);
            UpdateAttachedStick();
        }
    }
    public void OnCollisionExit(Collision collision)
    {
        if (!is_alive && collision.gameObject.CompareTag("CanBuild") && collision.gameObject.name.Contains("WoodStick"))
        {
            stick_collider.Remove(collision.collider);
            if (attachedStick == collision.collider) DetachStick();
        }
    }
}
