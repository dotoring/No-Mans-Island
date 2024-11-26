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



    //ExitGames.Client.Photon.Hashtable ht = new ExitGames.Client.Photon.Hashtable();
    

    private void Start()
    {
        isGriped = false;
        pv.ControllerActorNr = 0;
        //PhotonNetwork.SetPlayerCustomProperties(ht);
        //ht.Add("Grip",false);
        rig = GetComponent<Rigidbody>();
        inter.hoverEntered.AddListener((args) => { print(args+"Hover"); });
        inter.selectEntered.AddListener((args) =>
        { 
            isGriped = true;
            //ht["Grip"] = true;
            pv.RPC(nameof(Griped), RpcTarget.AllViaServer, isGriped);
        });
        inter.selectExited.AddListener((args) =>
        {
            isGriped = false;
            pv.RPC(nameof(Griped), RpcTarget.AllViaServer, isGriped);
        });

        //PhotonNetwork.SetPlayerCustomProperties(ht);
    }

    //public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    //{
        //print(changedProps[0]);
        //if ((bool)changedProps["Grip"]==true)
        //{
            //print("Grip");
        //}
        //else if ((bool)changedProps["Grip"] == false)
        //{
            //print("NO Grip");
        //}
    //}



    private void Update()
    {
        //�߷��� ������̸� ������ ���� ���̰�
        //�߷��� ������� �ƴϸ� ���� ���̴�
        if (rig.useGravity)
        {
            print("������");
            return;
        }
        else
        {
            print("����");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (rig.useGravity)
           return;
        
        //�������� �޴� ������Ʈ�� ü���� ���
        //���������� ��� �볪���� ���� ������ �����
        
    }

    [PunRPC]
    void Griped(bool isGriped)
    {
        //rig.isKinematic = isGriped;
        rig.useGravity = !isGriped;
        print("bool = " + isGriped);
    }

    
}
