using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class FrogClass : AnimalClass
{



    protected float rest_Time;
    protected float damage_Time;

    protected float jump_Time;


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
        attack_area = 2f;
        attack_time = 5f;


        rest_Time = 0f;
        jump_Time = 0f;

        inter.enabled = false;
        t_state = AnimalState.Idle;



    }

    // Update is called once per frame
    void Update()
    {
        ShortDistance();
        FrogCheck();
        //ThisStick();


        if (corpse_hp <= 0)
        {
            ChangeToMeat(1);
        }
    }

    private void FrogCheck()
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
            if (rest_Time >= 7f)
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
        jump_Time += Time.deltaTime;
        if (jump_Time <= 1.2f)
        {
            this.transform.Translate(Vector3.forward * 0.4f * Time.deltaTime, Space.Self);
        }
        else if (jump_Time > 1.5f)
        {
            jump_Time = 0;
        }


        if (rest_Time >= 3f)
        {
            rest_Time = 0;
            jump_Time = 0;

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

        jump_Time += Time.deltaTime;
        if (jump_Time <= 1.2f)
        {
            this.transform.Translate(Vector3.forward * 0.4f * Time.deltaTime, Space.Self);

        }
        else if (jump_Time > 1.5f)
        {
            jump_Time = 0f;
        }



        if (Vector3.Distance(this.transform.position, Player.transform.position) > find_area + 3.0f)
        {
            jump_Time = 0f;

            animal_anim.SetTrigger("Idle");

            t_state = AnimalState.Idle;

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

        this.GetComponent<BoxCollider>().enabled = false;


    }





}
