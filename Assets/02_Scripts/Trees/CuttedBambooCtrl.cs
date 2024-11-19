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
    int grabCount;
    bool isFixed;
    bool isTrigger;
    Rigidbody rb;
    XRGrabInteractable interactable;
    [SerializeField] private InputActionProperty leftTriggerAction;
    [SerializeField] private InputActionProperty rightTriggerAction;

    private void OnEnable()
    {
        //공격 이벤트 등록
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
            Debug.Log("잡았다");
            Debug.Log(var);

            grabCount++;
        });

        interactable.selectExited.AddListener((var) =>
        {
            Debug.Log("놨다");
            Debug.Log(var);
            if(isFixed)
            {
                Fix();
            }
            grabCount--;
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
        //rb.useGravity = false;
        rb.isKinematic = true;
        isFixed = true;

        interactable.interactionLayers = 1 << InteractionLayerMask.NameToLayer("Fixed");
    }

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
        if (collision.gameObject.CompareTag("Land"))
        {
            //충돌시 속도가 일정 이상일 때만 작동
            if (rb.linearVelocity.magnitude > 0.7f && isTrigger)
            {
                Debug.Log("박히기");
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
