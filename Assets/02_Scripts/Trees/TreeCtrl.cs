using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class TreeCtrl : InteractableObject
{
    [SerializeField] GameObject pref;
    [SerializeField] Transform[] spawnPoints;

    public override void TakeDamage(int dmg)
    {
        base.TakeDamage(dmg);
        if (Hp <= 0)
        {
            ChangeToWood();
            pv.RPC(nameof(ChangeToWood), RpcTarget.AllViaServer);
        }
    }

    [PunRPC]
    public void ChangeToWood()
    {
        Destroy(gameObject);
        SpawnWood();
    }

    public void SpawnWood()
    {
        for(int i = 0; i < spawnPoints.Length; i++)
        {
            Instantiate(pref, spawnPoints[i].position, Quaternion.identity);
        }
    }
}
