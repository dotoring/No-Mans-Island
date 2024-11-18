using UnityEngine;

public class WoodCtrl : InteractableObject
{
    [SerializeField] GameObject pref;

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
        Destroy(gameObject);
        SpawnPlank();
    }

    public void SpawnPlank()
    {
        for(int i = 0; i < 4; i++)
        {
            Instantiate(pref, transform.position, transform.rotation);
        }
    }
}
