using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using Photon.Pun;
using Photon;
using Photon.Realtime;
using System.Collections;
using Unity.VisualScripting;
using System.Security;

[RequireComponent(typeof(XRGrabInteractable))]

public class PhotonGrabObject : MonoBehaviourPunCallbacks
{
    protected Rigidbody rig;
    [SerializeField] protected XRGrabInteractable inter;
    public int grabCount;
    [SerializeField] protected PhotonView pv;


    protected virtual void Start()
    {
        //isGriped = false;
        grabCount = 0;

        rig = GetComponent<Rigidbody>();

        //if(inter != null)
        //{
            inter.selectEntered.AddListener((args) =>
            {
                //������Ʈ�� PhotonView���� Ownership Transfer�� Takeover�� �����ϸ� ������(��Ʈ�ѷ� ����)�� ������ ������ �� �ֵ��� �Ѵ�
                //TransferOwnership(Player) -> ���� PhotonView�� �������� Player�� �ٲٴ� �Լ�
                pv.TransferOwnership(PhotonNetwork.LocalPlayer);
                grabCount++;
                pv.RPC(nameof(Griped), RpcTarget.AllViaServer, grabCount);
                OnGrabChangeLayer(grabCount);
                SelectFunc();
            });
            inter.selectExited.AddListener((args) =>
            {
                grabCount--;
                pv.RPC(nameof(Griped), RpcTarget.AllViaServer, grabCount);
                OnGrabChangeLayer(grabCount);
            });
        //}
    }

    protected virtual void SelectFunc()
    {

    }
    

    [PunRPC]
    protected void Griped(int count)
    {
        if(count > 0)
        {
            rig.useGravity = false;
            rig.isKinematic = true;
        }
        else
        {
            rig.useGravity = true;
            rig.isKinematic= false;
        }
        //�׸� ���¸� �߷� ����
        //�׸� ���°� �ƴϸ� �߷� ŰŰ
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