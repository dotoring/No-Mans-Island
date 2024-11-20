using UnityEngine;

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
        }
    }

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
