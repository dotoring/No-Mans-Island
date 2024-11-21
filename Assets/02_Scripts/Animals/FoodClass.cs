using UnityEngine;

public class FoodClass : MonoBehaviour
{
    //playerClass pc;
    //pc.Eat(int val);




    public int baseHp;
    int curHp;
    public int fullness;
    public int Reduce_Hp;
    public float cookTime;
    public float infireTime;
    public bool is_Sticked;








    public void InitData()
    {

        cookTime = 0f;
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

    public virtual void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.name == "Player Mouse")
        {
            GetFullness(fullness);
            ReduceHP(Reduce_Hp);
            Destroy(this.gameObject);
        }

    }




}
