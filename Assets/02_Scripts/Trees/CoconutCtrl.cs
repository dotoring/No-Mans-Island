using UnityEngine;

public class CoconutCtrl : InteractableObject
{
    [SerializeField] GameObject halfCoconut;

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
        Destroy(gameObject);
        Instantiate(halfCoconut, transform.position, transform.rotation);
    }
}
