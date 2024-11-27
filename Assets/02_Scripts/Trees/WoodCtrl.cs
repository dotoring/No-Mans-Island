using Photon.Pun;
using UnityEngine;

public class WoodCtrl : InteractableObject
{
    public override void TakeDamage(int dmg)
    {
        base.TakeDamage(dmg);
        if (Hp <= 0)
        {
            ChangeToPlank();
        }
    }

    public void ChangeToPlank()
    {
        PhotonNetwork.Destroy(gameObject);
        SpawnPlank();
    }

    public void SpawnPlank()
    {
        for(int i = 0; i < 4; i++)
        {
            PhotonNetwork.Instantiate("Plank", transform.position, transform.rotation);
        }
    }
}
