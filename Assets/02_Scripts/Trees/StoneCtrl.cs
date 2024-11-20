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
        //�浹�� �ӵ��� ���� �̻��� ���� �۵�
        if(rb.linearVelocity.magnitude > 0.7f)
        {
            if(collision.gameObject.GetComponent<InteractableObject>())
            {
                collision.gameObject.GetComponent<InteractableObject>().TakeDamage(power);
            }

            //������ �ε�����
            if(collision.gameObject.CompareTag("Stone"))
            {
                int count = 0;
                //�ֺ��� ���� üũ
                Collider[] colliders = Physics.OverlapSphere(transform.position, 0.5f);

                //�ֺ��� ���� ���� üũ
                foreach (Collider collider in colliders)
                {
                    if (collider.CompareTag("FireWood"))
                    {
                        count++;
                    }
                }

                //������ ������ 4�� �̻��̸� ��ȭ
                if( count >= 4)
                {
                    foreach (Collider collider in colliders)
                    {
                        if (collider.CompareTag("FireWood"))
                        {
                            collider.GetComponent<FireWoodCtrl>().FlameOn();
                        }
                    }
                }
            }
        }
    }
}
