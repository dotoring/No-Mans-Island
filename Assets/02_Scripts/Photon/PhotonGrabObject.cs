using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using Photon.Pun;
using Photon;
using Photon.Realtime;
using System.Collections;
using Unity.VisualScripting;

public class PhotonGrabObject : MonoBehaviourPunCallbacks
{
    protected Rigidbody rig;
    [SerializeField] protected XRGrabInteractable inter;
    protected int grabCount;
    [SerializeField] protected PhotonView pv;


    protected virtual void Start()
    {
        grabCount = 0;

        rig = GetComponent<Rigidbody>();

        if (inter != null)
        {
            inter.selectEntered.AddListener((args) =>
            {
                //오브젝트의 PhotonView에서 Ownership Transfer를 Takeover로 설정하면 소유권(컨트롤러 포함)을 강제로 가져올 수 있도록 한다
                //TransferOwnership(Player) -> 현재 PhotonView의 소유권을 Player로 바꾸는 함수
                pv.TransferOwnership(PhotonNetwork.LocalPlayer);
                grabCount++;
                pv.RPC(nameof(Griped), RpcTarget.AllViaServer, grabCount);
                OnGrabChangeLayer(grabCount);
            });

            inter.selectExited.AddListener((args) =>
            {
                if(gameObject.activeSelf == true)
                {
                    grabCount--;
                    pv.RPC(nameof(Griped), RpcTarget.AllViaServer, grabCount);
                    OnGrabChangeLayer(grabCount);
                }
            });
        }
    }

    protected virtual void GripEvent()
    {

    }

    [PunRPC]
    public void Griped(int count)
    {
        if(count > 0)
        {
            rig.useGravity = false;
        }
        else
        {
            rig.useGravity = true;
        }
        //그립 상태면 중력 끄기
        //그립 상태가 아니면 중력 키키
        //rig.useGravity = !isGriped;
    }
   
    void OnGrabChangeLayer(int count)
    {
        int grabLayer = LayerMask.NameToLayer("GrabObject");
        int normalLayer = LayerMask.NameToLayer("Default");
        if (count > 0)
        {
            gameObject.layer = grabLayer;
        }
        else
        {
            gameObject.layer = normalLayer;
        }
    }
}