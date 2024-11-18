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


    GameObject meat;                    // 생고기 오브젝트      // 프리팹 받아오면 public 으로 바꿀 예정


    public void InitStat()              // 스탯 초기화
    {
        animal_hp = 0;
        animal_atk = 0;
        corpse_hp = 0;
        is_alive = false;

        animal_rb = this.GetComponent<Rigidbody>();
        animal_anim = this.GetComponentInChildren<Animator>();
    }

    public void GetDamage(int damage)   // 데미지를 받는다.
    {
        animal_hp -= damage;
    }

    public void Hit(int other_hp)       // 데미지를 가한다.
    {
        other_hp -= animal_atk;
    }

    public void Die()                   // 시체가 된다.     // 시체의 체력이 동물의 체력을 대체한다.
    {
        animal_hp = corpse_hp;
    }

    public void AnimalToMeat()          // 생고기로 변한다.      // 동물 오브젝트가 소멸하고 생고기 오브젝트가 대체한다.
    {
        if (this.gameObject != null)    // 동물 오브젝트가 아직 존재할 경우
        {
            Instantiate(meat, this.transform.position, this.transform.rotation);        // 동물 오브젝트 위치에 생고기를 생성하고
            Destroy(this.gameObject);                                                   // 동물 오브젝트를 삭제한다.
        }
    }






    public void Animal_Idle()
    {
        animal_rb.isKinematic = true;
    }

    public void Animal_Move()
    {
        this.transform.Translate(Vector3.forward * Time.deltaTime * 2.0f, Space.Self);
    }

    public void Animal_Watch()
    {
        GameObject player = GameObject.Find("Player");
        this.transform.LookAt(player.transform);
    }

    public void Animal_Attack()
    {
        StartCoroutine(PrintAttack());
    }

    IEnumerator PrintAttack()
    {
        yield return new WaitForSeconds(1.0f);
        print("동물이 공격합니다.");
    }

    public void Animal_Damage()
    {

    }

    public void Animal_Die()
    {
        Die();
    }

}
