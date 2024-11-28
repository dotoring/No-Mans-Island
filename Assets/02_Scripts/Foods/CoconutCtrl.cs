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
        PhotonNetwork.Instantiate("CuttedCoconuts", transform.position, transform.rotation);
        PhotonNetwork.Destroy(gameObject);
    }
}
