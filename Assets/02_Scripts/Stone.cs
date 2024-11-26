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
            //������Ʈ�� PhotonView���� Ownership Transfer�� Takeover�� �����ϸ� ������(��Ʈ�ѷ� ����)�� ������ ������ �� �ֵ��� �Ѵ�
            //TransferOwnership(Player) -> ���� PhotonView�� �������� Player�� �ٲٴ� �Լ�
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
