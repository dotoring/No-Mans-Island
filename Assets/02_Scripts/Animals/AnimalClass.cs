using System.Collections;
using UnityEngine;

public class AnimalClass : MonoBehaviour
{
    public int animal_hp;               // 동물의 체력
    public int animal_atk;              // 동물의 공격력
    public int corpse_hp;               // 시체의 체력

    public bool is_alive;               // 동물이 살아있는지 죽었는지 여부

    public Rigidbody animal_rb;         // 동물의 물리를 받는 변수
    public Animator animal_anim;        // 동물의 애니메이션을 받는 변수


    public GameObject meat;                    // 생고기 오브젝트      // 프리팹 받아오면 public 으로 바꿀 예정

    public GameObject Player;

    public float find_area;
    public float attack_area;
    public float attack_time;



    public void InitStat()              // 스탯 초기화
    {
        animal_hp = 0;
        animal_atk = 0;
        corpse_hp = 0;
        is_alive = true;

        find_area = 10f;
        attack_area = 4f;
        attack_time = 3f;

        animal_rb = this.GetComponent<Rigidbody>();
        animal_anim = this.GetComponent<Animator>();
        Player = GameObject.Find("Player");
    }

    public void GetDamage(int damage)   // 데미지를 받는다.
    {
        if (is_alive)
        {
            animal_hp -= damage;
            print($"{damage} 만큼 피해를 입었습니다. 남은 체력은 {animal_hp} 입니다.");

        }
        else
        {
            corpse_hp -= damage;
            print($"{damage} 만큼 피해를 입었습니다. 남은 체력은 {corpse_hp} 입니다.");
        }
    }

    public void Hit(int other_hp)       // 데미지를 가한다.
    {
        other_hp -= animal_atk;
    }

    public void Die()                   // 시체가 된다.     // 시체의 체력이 동물의 체력을 대체한다.
    {

        is_alive = false;



    }

    public void ChangeToMeat()          // 생고기로 변한다.      // 동물 오브젝트가 소멸하고 생고기 오브젝트가 대체한다.
    {
        if (this.gameObject != null)    // 동물 오브젝트가 아직 존재할 경우
        {
            Instantiate(meat, this.transform.position, this.transform.rotation);        // 동물 오브젝트 위치에 생고기를 생성하고
            Destroy(this.gameObject);                                                   // 동물 오브젝트를 삭제한다.
        }
    }








}
