using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ScolpionClass : AnimalClass
{



    public float rest_Time;
    public float damage_Time;

    public float cool_Time_max = 3f;
    public float cool_Time;

    public bool is_poison;
    public int duration_posion;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        InitStat();
        animal_hp = 30;
        animal_atk = 10;
        corpse_hp = 30;
        is_alive = true;
        attack_area = 2.5f;

        find_area = 3f;
        attack_area = 0.6f;
        attack_time = 5f;
        rest_Time = 0f;

        inter.enabled = false;

        is_poison = false;
        cool_Time = cool_Time_max;
        t_state = AnimalState.Idle;



    }



    // Update is called once per frame
    public void Update()
    {
        ShortDistance();
        ScolpionCheck();
        //poison(duration_posion);
        //ThisStick();



    }

    public void ScolpionCheck()
    {
        rest_Time += Time.deltaTime;
        damage_Time += Time.deltaTime;

        switch (t_state)
        {
            case AnimalState.Idle:
                Animal_Idle();
                break;
            case AnimalState.Move:
                Animal_Move();
                break;
            case AnimalState.Watch:
                Animal_Watch();
                break;
            case AnimalState.Attack:
                Animal_Attack();

                break;
            case AnimalState.Damage:
                Animal_Damage();


                break;
            case AnimalState.Die:
                Animal_Die();
                inter.enabled = true;
                break;
        }
    }


    public void Animal_Idle()
    {
        if (Player != null)
        {
            if (rest_Time >= 10.0f)
            {
                rest_Time = 0;

                t_state = AnimalState.Move;
                animal_anim.SetTrigger("Move");
            }

            else if (Vector3.Distance(this.transform.position, Player.transform.position) < find_area)
            {
                rest_Time = 0;
                animal_anim.SetTrigger("Move");
                t_state = AnimalState.Watch;

            }
            if (Vector3.Distance(this.transform.position, Player.transform.position) < attack_area)
            {
                t_state = AnimalState.Attack;
            }
        }



    }
    public void Animal_Move()
    {
        this.transform.Translate(Vector3.forward * 1.0f * Time.deltaTime, Space.Self);
        if (rest_Time >= 5f)
        {
            rest_Time = 0;

            t_state = AnimalState.Idle;
            animal_anim.SetTrigger("Idle");
            this.transform.forward = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        }
        if (Vector3.Distance(this.transform.position, Player.transform.position) < find_area)
        {
            rest_Time = 0;
            t_state = AnimalState.Watch;

        }

    }
    public void Animal_Watch()
    {
        Vector3 watch_v = Player.transform.position - this.transform.position;
        watch_v.Normalize();
        watch_v.y = 0;
        this.transform.forward = watch_v;

        if (Vector3.Distance(this.transform.position, Player.transform.position) < attack_area)
        {
            rest_Time = 0;
            animal_anim.SetTrigger("Attack");
            t_state = AnimalState.Attack;
        }
        else
        {
            this.transform.Translate(Vector3.forward * 1.0f * Time.deltaTime, Space.Self);


        }
    }
    public void Animal_Attack()
    {
        Vector3 watch_v = Player.transform.position - this.transform.position;
        watch_v.Normalize();
        watch_v.y = 0;
        this.transform.forward = watch_v;


        if (rest_Time >= attack_time)
        {
            rest_Time = 0;
            animal_anim.SetTrigger("Attack");

        }




        if (Vector3.Distance(this.transform.position, Player.transform.position) >= attack_area + 1.0f)
        {
            rest_Time = 0;

            t_state = AnimalState.Watch;
            animal_anim.SetTrigger("Move");
        }
        else
        {

        }

    }


    public void Animal_Damage()
    {


        if (animal_hp <= 0) t_state = AnimalState.Die;
        else
        {
            t_state = AnimalState.Attack;
        }


    }



    public void Animal_Die()
    {
        if (is_alive)
        {

            animal_anim.SetTrigger("Die");
            Die();
        }
    }




    public void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Stone"))   // Stone의 공격력을 5로 설정
        {
            GetDamage(5);
            t_state = AnimalState.Damage;
        }
    }

    public override void Hit(PlayerState player_sv)
    {
        base.Hit(player_sv);
        int ran = Random.Range(0, 2);
        if (ran == 0 && !is_poison)
        {
            duration_posion = 3;
        }
    }

    public void poison(int duration)
    {
        if (duration > 0)
        {
            is_poison = true;
            if (cool_Time <= 0f)
            {

                print($"{Player.gameObject.name}가 독 데미지 2를 받습니다.");
                cool_Time = cool_Time_max;
                duration--;
            }
            else
            {
                cool_Time -= Time.deltaTime;
            }
        }
        else
        {
            is_poison = false;
        }
    }
}
