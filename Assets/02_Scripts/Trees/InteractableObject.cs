using UnityEngine;

public class InteractableObject : PhotonGrabObject
{
    [SerializeField] protected int Hp;

    public virtual void TakeDamage(int dmg)
    {
        Hp -= dmg;
    }
}
