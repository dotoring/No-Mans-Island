using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;
using Photon.Realtime;

public class PlankCtrl : InteractableObject
{
    bool isBuildReady;
    bool isFixed;
    [SerializeField] GameObject[] contactPoints;

    public override void TakeDamage(int dmg)
    {
        //건설 준비 완료 중 공격을 받으면 고정(건설)
        if (isBuildReady)
        {
            Fix();
            pv.RPC(nameof(Fix), RpcTarget.AllViaServer);
        }
    }

    [PunRPC]
    void Fix()
    {
        rig.isKinematic = true;
        isFixed = true;

        //그랩 상호작용 레이어 변경으로 잡을 수 없도록 변경
        inter.interactionLayers = 1 << InteractionLayerMask.NameToLayer("Fixed");
    }

    private void OnCollisionStay(Collision collision)
    {
        //건설 가능 물체와 닿으면
        if (collision.gameObject.CompareTag("CanBuild"))
        {
            ContactPoint contactPoint;
            int contactCount = collision.GetContacts(collision.contacts);
            for (int i = 0; i < contactCount; i++)
            {
                //콜리더 접촉 지점에 돌 감지 트리거 콜리더를 세팅
                contactPoint = collision.GetContact(i);
                contactPoints[i].SetActive(true);
                contactPoints[i].transform.position = contactPoint.point;

                //접촉 지점 감지 포인트 갯수 만큼만
                if(i == contactPoints.Length - 1)
                {
                    break;
                }
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        //건설 가능 물체와 떨어지면
        if (collision.gameObject.CompareTag("CanBuild"))
        {
            for(int i = 0; i < contactPoints.Length; i++)
            {
                //감지 콜리더 모두 비활성화
                contactPoints[i].SetActive(false);
            }
            //건설 준비 해제
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
