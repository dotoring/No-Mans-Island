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
    public bool isFixed;
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
            grabCount++;
        });

        interactable.selectExited.AddListener((var) =>
        {
            grabCount--;
        });
    }

    public override void TakeDamage(int dmg) 
    {
        //건설 준비 완료시
        if(isBuildReady)
        {
            Fix();
        }
    }

    //고정 시키는 함수
    void Fix()
    {
        //물리 안받기
        rb.isKinematic = true;
        isFixed = true;
        //그랩 상호작용 레이어 변경해서, 잡기 막기
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
            //충돌시 속도가 일정 이상, 트리거를 누른 상태
            if (rb.linearVelocity.magnitude > 0.7f && isTrigger)
            {
                Fix();
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        //건설 가능 물체와 접촉중이면
        if (collision.gameObject.CompareTag("CanBuild"))
        {
            isBuildReady = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        //건설 가능 물체와 떨어지면
        if (collision.gameObject.CompareTag("CanBuild"))
        {
            isBuildReady = false;
        }
    }
}
