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
        //�浹�� �ӵ��� ���� �̻��� ���� �۵�
        if(rb.linearVelocity.magnitude > 0.7f)
        {
            if(collision.gameObject.GetComponent<InteractableObject>())
            {
                collision.gameObject.GetComponent<InteractableObject>().TakeDamage(power);
            }
        }
    }
}
