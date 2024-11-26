using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using Photon.Pun;
using Photon;
using Photon.Realtime;
using System.Collections;
using Unity.VisualScripting;

public class PhotonGrabObject : MonoBehaviourPunCallbacks
{
    private Rigidbody rig;
    [SerializeField] XRGrabInteractable inter;
    public bool isGriped;
    [SerializeField] PhotonView pv;


    private void Start()
    {
        isGriped = false;

        rig = GetComponent<Rigidbody>();

        if(inter != null)
        {
            inter.selectEntered.AddListener((args) =>
            {
                //������Ʈ�� PhotonView���� Ownership Transfer�� Takeover�� �����ϸ� ������(��Ʈ�ѷ� ����)�� ������ ������ �� �ֵ��� �Ѵ�
                //TransferOwnership(Player) -> ���� PhotonView�� �������� Player�� �ٲٴ� �Լ�
                pv.TransferOwnership(PhotonNetwork.LocalPlayer);
                isGriped = true;
                pv.RPC(nameof(Griped), RpcTarget.AllViaServer, isGriped);
            });
            inter.selectExited.AddListener((args) =>
            {
                isGriped = false;
                pv.RPC(nameof(Griped), RpcTarget.AllViaServer, isGriped);
            });
        }
    }

    [PunRPC]
    void Griped(bool isGriped)
    {
        rig.useGravity = !isGriped;
    }


}