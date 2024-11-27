using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CoconutCtrl : InteractableObject
{
    [SerializeField] GameObject halfCoconut;

    public override void TakeDamage(int dmg)
    {
        base.TakeDamage(dmg);
        if (Hp <= 0)
        {
            //CutCoconut();
            pv.RPC(nameof(CutCoconut), RpcTarget.AllViaServer);
        }
    }

    [PunRPC]
    void CutCoconut()
    {
        //Destroy(gameObject);
        //Instantiate(halfCoconut, transform.position, transform.rotation);
        PhotonNetwork.Destroy(gameObject);
        PhotonNetwork.Instantiate("CuttedCoconuts", transform.position, transform.rotation);
    }
}
