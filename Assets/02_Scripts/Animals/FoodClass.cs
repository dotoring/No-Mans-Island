using UnityEngine;

public class FoodClass : MonoBehaviour
{
    public int fullness;
    public int thirst;
    public int Reduce_Hp;
    public float cookTime;
    protected float infireTime;
    public bool is_Sticked;

    public void InitData()
    {
        infireTime = 0f;
        is_Sticked = false;
    }

    public void GetFullness(int fullness_val)
    {
        ////pc.Eat(fullness_val);
        print($"포만감이 {fullness_val} 만큼 올랐습니다.");
    }

    public void ReduceHP(int Reduce_Hp_val)
    {
        //pc.TakeDamage(Reduce_Hp_val);

        print($"체력이 {Reduce_Hp_val} 만큼 감소했습니다.");
    }
}
