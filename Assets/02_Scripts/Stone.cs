using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using Photon.Pun;
using Photon;
using Photon.Realtime;
using System.Collections;
using Unity.VisualScripting;

public class Stone : MonoBehaviourPunCallbacks
{
    [SerializeField] int childcount;
    private Rigidbody rig;
    [SerializeField]XRGrabInteractable inter;
    public bool isGriped;
    [SerializeField] PhotonView pv;
    

    private void Start()
    {
        isGriped = false;
        pv.ControllerActorNr = 0;

        rig = GetComponent<Rigidbody>();

        inter.selectEntered.AddListener((args) =>
        {
            //오브젝트의 PhotonView에서 Ownership Transfer를 Takeover로 설정하면 소유권(컨트롤러 포함)을 강제로 가져올 수 있도록 한다
            //TransferOwnership(Player) -> 현재 PhotonView의 소유권을 Player로 바꾸는 함수
            pv.TransferOwnership(PhotonNetwork.LocalPlayer);
            isGriped = true;
            //ht["Grip"] = true;
            //pv.OwnershipTransfer = OwnershipOption.Fixed;
            pv.RPC(nameof(Griped), RpcTarget.AllViaServer, isGriped);
        });
        inter.selectExited.AddListener((args) =>
        {
            isGriped = false;
            pv.RPC(nameof(Griped), RpcTarget.AllViaServer, isGriped);
        });

        
    }

    [PunRPC]
    void Griped(bool isGriped)
    {
        //if(!pv.IsMine)
        //rig.isKinematic = isGriped;
        rig.useGravity = !isGriped;
    }

    
}
