using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class RabbitClass : AnimalClass
{
    protected float rest_Time;

    [SerializeField] protected float moveSpeed = 1.0f;
    [SerializeField] protected float runSpeed = 1.3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();


        InitStat();
        animal_hp = 30;
        animal_atk = 10;
        corpse_hp = 30;
        is_alive = true;

        find_area = 3f;

        inter.enabled = false;

        rest_Time = 0f;
        t_state = AnimalState.Idle;


    }

    // Update is called once per frame
    void Update()
    {
        ShortDistance();
        RabbitCheck();

        if (corpse_hp <= 0)
        {
            ChangeToMeat();
        }
    }

    private void RabbitCheck()
    {
        rest_Time += Time.deltaTime;

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

            case AnimalState.Damage:
                Animal_Damage();


                break;
            case AnimalState.Die:
                Animal_Die();
                inter.enabled = true;
                break;
        }
    }


    public void Animal_Idle()               // 가만히 서기      // 울음소리 내기
    {
        if (Player != null)
        {
            if (rest_Time >= 5f)
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
        }

    }
    public void Animal_Move()           // 움직이기
    {
        this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);


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
    public void Animal_Watch()          // 도망치기
    {
        Vector3 watch_v = this.transform.position - Player.transform.position;
        watch_v.Normalize();
        watch_v.y = 0;
        this.transform.forward = watch_v;


        this.transform.Translate(Vector3.forward * runSpeed * Time.deltaTime, Space.Self);


        if (Vector3.Distance(this.transform.position, Player.transform.position) >= find_area + 3.0f)
        {


            animal_anim.SetTrigger("Move");

            t_state = AnimalState.Watch;
        }
    }

    public void Animal_Damage()
    {
        if (animal_hp <= 0) t_state = AnimalState.Die;
        else
        {
            animal_anim.SetTrigger("Move");
            t_state = AnimalState.Watch;

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

    public override void GetDamage(int damage)
    {
        if (is_alive)
        {
            animal_hp -= damage;
            print($"{damage} 만큼 피해를 입었습니다. 남은 체력은 {animal_hp} 입니다.");

        }
    }











}