using Unity.VisualScripting;
using UnityEngine;

public enum TigerState
{
    Idle,
    Move,
    Watch,
    Attack,
    Damage,
    Die
}
public class TigerClass : AnimalClass
{

    TigerState t_state = new TigerState();


    float rest_Time;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitStat();
        animal_hp = 30;
        animal_atk = 10;
        corpse_hp = 10;
        is_alive = true;

        t_state = TigerState.Idle;

        rest_Time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        TigerCheck();
    }

    private void TigerCheck()
    {
        rest_Time += Time.deltaTime;
        switch (t_state)
        {
            case TigerState.Idle:
                Animal_Idle();
                break;
            case TigerState.Move:
                Animal_Move();
                break;
            case TigerState.Watch:
                Animal_Watch();
                break;
            case TigerState.Attack:
                Animal_Watch();
                break;
            case TigerState.Damage:
                Animal_Damage();
                if (animal_hp <= 0) t_state = TigerState.Die;
                break;
            case TigerState.Die:
                Animal_Die();
                break;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Stone"))   // Stone의 공격력을 5로 설정
        {
            GetDamage(5);
            t_state = TigerState.Damage;
        }
    }
}
