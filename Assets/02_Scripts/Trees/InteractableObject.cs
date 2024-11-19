using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] protected int Hp;

    public virtual void TakeDamage(int dmg)
    {
        Hp -= dmg;
    }
}
