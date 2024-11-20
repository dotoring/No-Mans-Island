using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class StoneCtrl : MonoBehaviour
{
    [SerializeField] int power;
    Rigidbody rb;
    XRGrabInteractable interactable;
    int grabCount;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        interactable = GetComponent<XRGrabInteractable>();

        interactable.selectEntered.AddListener((var) =>
        {
            grabCount++;
        });

        interactable.selectExited.AddListener((var) =>
        {
            grabCount--;
        });
    }

    private void OnCollisionEnter(Collision collision)
    {
        //�浹�� �ӵ��� ���� �̻��� �� & �÷��̾ ������� ��
        if(rb.linearVelocity.magnitude > 0.7f && grabCount > 0)
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
