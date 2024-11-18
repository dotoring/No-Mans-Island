using UnityEngine;

public class AnimalClass : MonoBehaviour
{
    public int animal_hp;               // 동물의 체력
    public int animal_atk;              // 동물의 공격력
    public int corpse_hp;               // 시체의 체력

    public bool is_alive;               // 동물이 살아있는지 죽었는지 여부

    GameObject meat;                    // 생고기 오브젝트      // 프리팹 받아오면 public 으로 바꿀 예정




    public void GetDamage(int damage)   // 동물이 Player로부터 데미지를 받는다.
    {
        animal_hp -= damage;
    }

    public void Hit(int other_hp)       // 동물이 Player에게 데미지를 가한다.
    {
        other_hp -= animal_atk;
    }

    public void Die()                   // 동물이 죽어 시체가 된다.     // 시체의 체력이 동물의 체력을 대체한다.
    {
        animal_hp = corpse_hp;
    }

    public void AnimalToMeat()          // 시체가 생고기로 변한다.      // 시체 오브젝트가 소멸하고 생고기 오브젝트가 대체한다.
    {
        if (this.gameObject != null)
        {
            Instantiate(meat, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }
    }

}
