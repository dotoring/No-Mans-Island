using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public enum TigerState
{
    Idle,
    Move,
    Watch,
    Attack,
    Damage,
    Die
}
public class TigerClass : AnimalClass
{

    TigerState t_state = new TigerState();

    //XRGrabInteractable xrgrab;


    float rest_Time;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        InitStat();
        animal_hp = 30;
        animal_atk = 10;
        corpse_hp = 30;
        is_alive = true;

        t_state = TigerState.Idle;

        rest_Time = 0f;
        //xrgrab = GetComponent<XRGrabInteractable>();
        //xrgrab.enabled = false;



    }

    // Update is called once per frame
    void Update()
    {
        TigerCheck();


        if (corpse_hp <= 0)
        {
            ChangeToMeat();
        }
    }

    private void TigerCheck()
    {
        rest_Time += Time.deltaTime;
        switch (t_state)
        {
            case TigerState.Idle:
                Animal_Idle();
                break;
            case TigerState.Move:
                Animal_Move();
                break;
            case TigerState.Watch:
                Animal_Watch();
                break;
            case TigerState.Attack:
                Animal_Attack();

                break;
            case TigerState.Damage:
                Animal_Damage();


                break;
            case TigerState.Die:
                Animal_Die();
                //xrgrab.enabled = true;
                break;
        }
    }


    public void Animal_Idle()
    {
        if (rest_Time >= 5f)
        {
            rest_Time = 0;

            t_state = TigerState.Move;
            animal_anim.SetTrigger("Move");
        }
        if (Vector3.Distance(this.transform.position, Player.transform.position) < find_area)
        {
            rest_Time = 0;

            t_state = TigerState.Watch;
            animal_anim.SetTrigger("Move");
        }
        if (Vector3.Distance(this.transform.position, Player.transform.position) < attack_area)
        {
            t_state = TigerState.Attack;

        }
    }
    public void Animal_Move()
    {
        this.transform.Translate(Vector3.forward * 1.0f * Time.deltaTime, Space.Self);
        if (rest_Time >= 5f)
        {
            rest_Time = 0;

            t_state = TigerState.Idle;
            animal_anim.SetTrigger("Idle");
            this.transform.forward = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        }
        if (Vector3.Distance(this.transform.position, Player.transform.position) < find_area)
        {
            rest_Time = 0;

            t_state = TigerState.Watch;

        }
    }
    public void Animal_Watch()
    {
        Vector3 watch_v = Player.transform.position - this.transform.position;
        watch_v.Normalize();
        watch_v.y = 0;
        this.transform.forward = watch_v;

        if (Vector3.Distance(this.transform.position, Player.transform.position) < attack_area)
        {
            t_state = TigerState.Attack;

        }
        else
        {
            this.transform.Translate(Vector3.forward * 1.0f * Time.deltaTime, Space.Self);
        }
    }
    public void Animal_Attack()
    {
        rest_Time += Time.deltaTime;
        if (Vector3.Distance(this.transform.position, Player.transform.position) > attack_area)
        {
            rest_Time = 0;
            t_state = TigerState.Watch;
            animal_anim.SetTrigger("Move");

        }
        else
        {
            if (rest_Time >= attack_time)
            {
                animal_anim.SetTrigger("Attack");
                rest_Time = 0;
            }
        }
    }
    public void Animal_Damage()
    {
        animal_anim.SetTrigger("Damage");
        if (animal_hp <= 0) t_state = TigerState.Die;
        else t_state = TigerState.Idle;


    }



    public void Animal_Die()
    {
        animal_anim.SetTrigger("Die");
        Die();
    }




    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Stone"))   // Stone의 공격력을 5로 설정
        {
            GetDamage(5);
            t_state = TigerState.Damage;
        }
    }
}
