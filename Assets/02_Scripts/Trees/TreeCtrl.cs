using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class TreeCtrl : InteractableObject
{
    [SerializeField] Transform[] spawnPoints;

    public override void TakeDamage(int dmg)
    {
        base.TakeDamage(dmg);
        if (Hp <= 0)
        {
            ChangeToWood();
        }
    }

    public void ChangeToWood()
    {
        PhotonNetwork.Destroy(gameObject);
        SpawnWood();
    }

    public void SpawnWood()
    {
        for(int i = 0; i < spawnPoints.Length; i++)
        {
            PhotonNetwork.Instantiate("WoodStick", spawnPoints[i].position, Quaternion.identity);
        }
    }
}
