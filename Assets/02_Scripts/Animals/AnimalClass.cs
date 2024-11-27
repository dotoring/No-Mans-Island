using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimalState
{
    Idle,
    Move,
    Watch,
    Eat,
    Attack,
    Damage,
    Die
}

public class AnimalClass : PhotonGrabObject
{
    public int animal_hp;               // 동물의 체력
    public int animal_atk;              // 동물의 공격력
    public int corpse_hp;               // 시체의 체력

    public bool is_alive;               // 동물이 살아있는지 죽었는지 여부


    public Animator animal_anim;        // 동물의 애니메이션을 받는 변수



    public GameObject meat;                    // 생고기 오브젝트      // 프리팹 받아오면 public 으로 바꿀 예정

    public GameObject Player;

    public float find_area;
    public float attack_area;
    public float attack_time;
    public float short_distance = 400.0f;


    public AnimalState t_state = new AnimalState();

    public List<GameObject> PhotonPlayer = new List<GameObject>();

    public PlayerState player_s;



    public virtual void InitStat()              // 스탯 초기화
    {
        is_alive = true;
        animal_anim = this.GetComponent<Animator>();

        StartCoroutine(AddPlayer());


    }

    public virtual void GetDamage(int damage)   // 데미지를 받는다.
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

    public void FirstAddListPlayer()                 // Player 들을 PhotonPlayer list 에 추가
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            PhotonPlayer.Add(player);
        }
    }

    IEnumerator AddPlayer()
    {
        yield return new WaitForSeconds(3.0f);
        FirstAddListPlayer();
        ShortDistance();
        yield return null;
    }




    public void ShortDistance()                 // Player와의 거리를 계산한 뒤, 젤 가까운 Player를 타겟으로 설정
    {
        foreach (GameObject short_player in PhotonPlayer)
        {
            float Distance_m = Vector3.Distance(this.transform.position, short_player.transform.position);
            if (Distance_m < short_distance)
            {
                short_distance = Distance_m;
                Player = short_player;
            }
        }
    }

    public virtual void Hit(PlayerState player_sv)       // 데미지를 가한다.
    {
        player_sv.TakeDamage(animal_atk);
        print($"{this.transform.root.gameObject.name} 이가 {Player.gameObject.name} 를 공격");
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
