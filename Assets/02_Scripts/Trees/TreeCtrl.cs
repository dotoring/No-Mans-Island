using UnityEngine;

public class TreeCtrl : InteractableObject
{
    [SerializeField] GameObject pref;

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
        Instantiate(pref, transform.position, Quaternion.identity);
    }
}
