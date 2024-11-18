using Unity.VisualScripting;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.OpenXR.Input;

public class CuttedBambooCtrl : InteractableObject
{
    bool isBuildReady;
    bool isGrabed;
    bool isFixed;
    bool isTrigger;
    Rigidbody rb;
    XRGrabInteractable interactable;
    [SerializeField] private InputActionProperty leftTriggerAction;
    [SerializeField] private InputActionProperty rightTriggerAction;

    private void OnEnable()
    {
        //���� �̺�Ʈ ���
        leftTriggerAction.action.performed += LeftTriggerEnter;
        rightTriggerAction.action.performed += RightTriggerEnter;

        leftTriggerAction.action.canceled += LeftTriggerExit;
        rightTriggerAction.action.canceled += RightTriggerExit;
    }

    private void OnDisable()
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

        interactable.selectEntered.AddListener((var) =>
        {
            Debug.Log("��Ҵ�");
            Debug.Log(var);

            isGrabed = true;
        });

        interactable.selectExited.AddListener((var) =>
        {
            Debug.Log("����");
            Debug.Log(var);
            if(isFixed)
            {
                Fix();
            }
            isGrabed = false;
        });
    }

    public override void TakeDamage(int dmg) 
    {
        if(isBuildReady)
        {
            Fix();
        }
    }

    void Fix()
    {
        Debug.Log("Fix");
        rb.useGravity = false;
        rb.isKinematic = true;
        isFixed = true;

        interactable.enabled = false;
    }

    void LeftTriggerEnter(InputAction.CallbackContext context)
    {
        if(isGrabed)
        {
            isTrigger = true;
        }
    }

    void RightTriggerEnter(InputAction.CallbackContext context)
    {
        if (isGrabed)
        {
            isTrigger = true;
        }
    }

    void LeftTriggerExit(InputAction.CallbackContext context)
    {
        if (isGrabed)
        {
            isTrigger = false;
        }
    }

    void RightTriggerExit(InputAction.CallbackContext context)
    {
        if (isGrabed)
        {
            isTrigger = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Land"))
        {
            //�浹�� �ӵ��� ���� �̻��� ���� �۵�
            if (rb.linearVelocity.magnitude > 0.7f && isTrigger)
            {
                Fix();
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("CanBuild"))
        {
            isBuildReady = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("CanBuild"))
        {
            isBuildReady = false;
        }
    }
}
