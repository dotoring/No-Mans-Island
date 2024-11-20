using UnityEngine;

public class FoodClass : MonoBehaviour
{
    public int fullness;
    public int Reduce_Hp;
    public float cookTime;
    public float infireTime;



    public void InitData()
    {
        fullness = 0;
        Reduce_Hp = 0;
        cookTime = 0f;
        infireTime = 0f;
    }

    public void GetFullness(int PlayerFullness)
    {
        PlayerFullness += fullness;
        print($"포만감이 {fullness} 만큼 올랐습니다.");
    }

    public void ReduceHP(int PlayerHP)
    {
        PlayerHP -= Reduce_Hp;

        print($"체력이 {Reduce_Hp} 만큼 감소했습니다.");
    }


}
