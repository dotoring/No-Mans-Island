using UnityEngine;

public class BambooCtrl : InteractableObject
{
    [SerializeField] GameObject pref;

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
        Instantiate(pref, transform.position, transform.rotation);
    }
}
