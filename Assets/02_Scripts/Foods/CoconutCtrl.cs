using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CoconutCtrl : InteractableObject
{
    //[SerializeField] GameObject halfCoconut;
    [SerializeField] Transform[] spawnPoint;

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
        foreach (Transform t in spawnPoint)
        {
            PhotonNetwork.Instantiate("Coconut_Half", t.position, t.rotation);
        }
        PhotonNetwork.Destroy(gameObject);
    }
}
