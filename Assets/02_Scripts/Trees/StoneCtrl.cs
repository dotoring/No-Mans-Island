using UnityEngine;

public class StoneCtrl : MonoBehaviour
{
    [SerializeField] int power;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(rb.linearVelocity.magnitude);
        //충돌시 속도가 일정 이상일 때만 작동
        if(rb.linearVelocity.magnitude > 0.7f)
        {
            if(collision.gameObject.GetComponent<InteractableObject>())
            {
                collision.gameObject.GetComponent<InteractableObject>().TakeDamage(power);
            }
        }
    }
}
