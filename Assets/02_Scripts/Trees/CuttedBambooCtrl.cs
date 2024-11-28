using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class CuttedBambooCtrl : InteractableObject
{
    bool isBuildReady;
    public bool isFixed;
    bool isTrigger;
    [SerializeField] int power;
    [SerializeField] private InputActionProperty leftTriggerAction;
    [SerializeField] private InputActionProperty rightTriggerAction;

    [SerializeField] GameObject contackPoints;

    public override void OnEnable()
    {
        //공격 이벤트 등록
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

    public override void TakeDamage(int dmg)
    {
        //건설 준비 완료시
        if (isBuildReady)
        {
            //Fix();
            pv.RPC(nameof(Fix), RpcTarget.AllViaServer);
        }
    }

    //고정 시키는 함수
    [PunRPC]
    void Fix()
    {
        //물리 안받기
        rig.isKinematic = true;
        isFixed = true;
        //그랩 상호작용 레이어 변경으로 잡을 수 없도록 변경
        inter.interactionLayers = 1 << InteractionLayerMask.NameToLayer("Fixed");
    }

    //트리거 입력 체크 함수들
    void LeftTriggerEnter(InputAction.CallbackContext context)
    {
        if (grabCount > 0)
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
        //바닥에 꽂을 수 있도록 해주는 코드
        if (collision.gameObject.CompareTag("Land"))
        {
            //충돌시 속도가 일정 이상, 트리거를 누른 상태
            if (rig.linearVelocity.magnitude > 0.7f && isTrigger)
            {
                //Fix();
                pv.RPC(nameof(Fix), RpcTarget.AllViaServer);
            }
        }

        if (rig.linearVelocity.magnitude > 0.7f && grabCount > 0)
        {
            if (collision.gameObject.GetComponentInParent<AnimalClass>() != null)
            {
                collision.gameObject.GetComponentInParent<AnimalClass>().CallDamageRPC(power);
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        //건설 가능 물체와 닿으면
        if (collision.gameObject.CompareTag("CanBuild"))
        {
            //콜리더 접촉 지점에 돌 감지 트리거 콜리더를 세팅
            contackPoints.SetActive(true);
            ContactPoint contactPoint = collision.GetContact(0);
            contackPoints.transform.position = contactPoint.point;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        //건설 가능 물체와 떨어지면
        if (collision.gameObject.CompareTag("CanBuild"))
        {
            //감지 콜리더 모두 비활성화, 건설 준비 해제
            contackPoints.SetActive(false);
            isBuildReady = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //콜리더 접촉 지점에 돌이 들어오면 건설준비
        if (other.CompareTag("Stone"))
        {
            isBuildReady = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //콜리더 접촉 지점에서 돌이 나가면 건설준비 해제
        if (other.CompareTag("Stone"))
        {
            isBuildReady = false;
        }
    }
}
