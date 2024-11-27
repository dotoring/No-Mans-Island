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
            CutCoconut();
        }
    }

    void CutCoconut()
    {
        PhotonNetwork.Destroy(gameObject);
        PhotonNetwork.InstantiateRoomObject("CuttedCoconuts", transform.position, transform.rotation);
    }
}
