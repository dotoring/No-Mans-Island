using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class TreeCtrl : InteractableObject
{
    [SerializeField] Transform[] spawnPoints;
    protected string dropLog;

    public override void TakeDamage(int dmg)
    {
        base.TakeDamage(dmg);
        if (Hp <= 0)
        {
            //ChangeToWood();
            pv.RPC(nameof(ChangeToWood), RpcTarget.AllViaServer);
        }
    }

    [PunRPC]
    public void ChangeToWood()
    {
        if (pv.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
            SpawnWood();
        }

    }

    public virtual void SpawnWood()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            PhotonNetwork.Instantiate(dropLog, spawnPoints[i].position, Quaternion.identity);
        }
    }
}
