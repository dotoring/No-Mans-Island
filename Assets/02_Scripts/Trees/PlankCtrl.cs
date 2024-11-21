using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class PlankCtrl : InteractableObject
{
    bool isBuildReady;
    bool isFixed;
    Rigidbody rb;
    XRGrabInteractable interactable;
    [SerializeField] GameObject[] contactPoints;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        interactable = GetComponent<XRGrabInteractable>();
    }

    public override void TakeDamage(int dmg)
    {
        //�Ǽ� �غ� �Ϸ� �� ������ ������ ����(�Ǽ�)
        if (isBuildReady)
        {
            Fix();
        }
    }

    void Fix()
    {
        rb.isKinematic = true;
        isFixed = true;

        //�׷� ��ȣ�ۿ� ���̾� �������� ���� �� ������ ����
        interactable.interactionLayers = 1 << InteractionLayerMask.NameToLayer("Fixed");
    }

    private void OnCollisionStay(Collision collision)
    {
        //�Ǽ� ���� ��ü�� ������
        if (collision.gameObject.CompareTag("CanBuild"))
        {
            ContactPoint contactPoint;
            int contactCount = collision.GetContacts(collision.contacts);
            for (int i = 0; i < contactCount; i++)
            {
                //�ݸ��� ���� ������ �� ���� Ʈ���� �ݸ����� ����
                contactPoint = collision.GetContact(i);
                contactPoints[i].SetActive(true);
                contactPoints[i].transform.position = contactPoint.point;

                //���� ���� ���� ����Ʈ ���� ��ŭ��
                if(i == contactPoints.Length - 1)
                {
                    break;
                }
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        //�Ǽ� ���� ��ü�� ��������
        if (collision.gameObject.CompareTag("CanBuild"))
        {
            for(int i = 0; i < contactPoints.Length; i++)
            {
                //���� �ݸ��� ��� ��Ȱ��ȭ
                contactPoints[i].SetActive(false);
            }
            //�Ǽ� �غ� ����
            isBuildReady = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //�ݸ��� ���� ������ ���� ������ �Ǽ��غ�
        if (other.CompareTag("Stone"))
        {
            isBuildReady = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //�ݸ��� ���� �������� ���� ������ �Ǽ��غ� ����
        if (other.CompareTag("Stone"))
        {
            isBuildReady = false;
        }
    }
}
