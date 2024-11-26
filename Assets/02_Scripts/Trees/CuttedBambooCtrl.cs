using NUnit.Framework.Internal;
using Unity.VisualScripting;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.OpenXR.Input;

public class CuttedBambooCtrl : InteractableObject
{
    bool isBuildReady;
    //int grabCount;
    public bool isFixed;
    bool isTrigger;
    Rigidbody rb;
    XRGrabInteractable interactable;
    [SerializeField] private InputActionProperty leftTriggerAction;
    [SerializeField] private InputActionProperty rightTriggerAction;

    [SerializeField] GameObject contackPoints;

    public override void OnEnable()
    {
        //���� �̺�Ʈ ���
        leftTriggerAction.action.performed += LeftTriggerEnter;
        rightTriggerAction.action.performed += RightTriggerEnter;

        leftTriggerAction.action.canceled += LeftTriggerExit;
        rightTriggerAction.action.canceled += RightTriggerExit;
    }

    public override void OnDisable()
    {
        leftTriggerAction.action.performed -= LeftTriggerEnter;
        rightTriggerAction.action.performed -= RightTriggerEnter;

        leftTriggerAction.action.canceled -= LeftTriggerExit;
        rightTriggerAction.action.canceled -= RightTriggerExit;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        interactable = GetComponent<XRGrabInteractable>();

        //���� �÷��̾ ����ִ��� üũ�ϴ� �ڵ�
        //interactable.selectEntered.AddListener((var) =>
        //{
        //    grabCount++;
        //});
        //interactable.selectExited.AddListener((var) =>
        //{
        //    grabCount--;
        //});
    }

    public override void TakeDamage(int dmg) 
    {
        //�Ǽ� �غ� �Ϸ��
        if(isBuildReady)
        {
            Fix();
        }
    }

    //���� ��Ű�� �Լ�
    void Fix()
    {
        //���� �ȹޱ�
        rb.isKinematic = true;
        isFixed = true;
        //�׷� ��ȣ�ۿ� ���̾� �������� ���� �� ������ ����
        interactable.interactionLayers = 1 << InteractionLayerMask.NameToLayer("Fixed");
    }

    //Ʈ���� �Է� üũ �Լ���
    void LeftTriggerEnter(InputAction.CallbackContext context)
    {
        if(grabCount > 0)
        {
            isTrigger = true;
        }
    }
    void RightTriggerEnter(InputAction.CallbackContext context)
    {
        if (grabCount > 0)
        {
            isTrigger = true;
        }
    }
    void LeftTriggerExit(InputAction.CallbackContext context)
    {
        if (grabCount > 0)
        {
            isTrigger = false;
        }
    }
    void RightTriggerExit(InputAction.CallbackContext context)
    {
        if (grabCount > 0)
        {
            isTrigger = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //�ٴڿ� ���� �� �ֵ��� ���ִ� �ڵ�
        if (collision.gameObject.CompareTag("Land"))
        {
            //�浹�� �ӵ��� ���� �̻�, Ʈ���Ÿ� ���� ����
            if (rb.linearVelocity.magnitude > 0.7f && isTrigger)
            {
                Fix();
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        //�Ǽ� ���� ��ü�� ������
        if (collision.gameObject.CompareTag("CanBuild"))
        {
            //�ݸ��� ���� ������ �� ���� Ʈ���� �ݸ����� ����
            contackPoints.SetActive(true);
            ContactPoint contactPoint = collision.GetContact(0);
            contackPoints.transform.position = contactPoint.point;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        //�Ǽ� ���� ��ü�� ��������
        if (collision.gameObject.CompareTag("CanBuild"))
        {
            //���� �ݸ��� ��� ��Ȱ��ȭ, �Ǽ� �غ� ����
            contackPoints.SetActive(false);
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
