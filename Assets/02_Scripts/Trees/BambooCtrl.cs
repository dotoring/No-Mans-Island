using UnityEngine;

public class BambooCtrl : InteractableObject
{
    [SerializeField] GameObject pref;
    [SerializeField] Transform spawnPoint;

    public override void TakeDamage(int dmg)
    {
        base.TakeDamage(dmg);
        if (Hp <= 0)
        {
            CutBamboo();
        }
    }

    public void CutBamboo()
    {
        Destroy(gameObject);
        SpawnBamboo();
    }

    public void SpawnBamboo()
    {
        Instantiate(pref, spawnPoint.position, transform.rotation);
    }
}
