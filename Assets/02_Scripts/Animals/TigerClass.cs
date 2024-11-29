using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;


public class TigerClass : AnimalClass
{



    //XRGrabInteractable xrgrab;


    protected float rest_Time;
    protected float damage_Time;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        InitStat();
        animal_hp = 200;
        animal_atk = 20;
        corpse_hp = 30;
        is_alive = true;

        find_area = 3.0f;
        attack_area = 1.55f;
        attack_time = 5f;

        moveSpeed = 1.5f;
        watchSpeed = 2.0f;

        rest_Time = 0f;

        t_state = AnimalState.Idle;
        //xrgrab = GetComponent<XRGrabInteractable>();
        //xrgrab.enabled = false;




    }



    // Update is called once per frame
    void Update()
    {
        TigerCheck();
        ShortDistance();



        if (corpse_hp <= 0)
        {
            ChangeToMeat(6);
        }
    }

    private void TigerCheck()
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
            case AnimalState.Attack:
                Animal_Attack();

                break;
            case AnimalState.Damage:
                Animal_Damage();


                break;
            case AnimalState.Die:
                Animal_Die();
                //xrgrab.enabled = true;
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
        }



    }
    public void Animal_Move()
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
    public void Animal_Watch()
    {
        Vector3 watch_v = Player.transform.position - this.transform.position;
        watch_v.Normalize();
        watch_v.y = 0;
        this.transform.forward = watch_v;

        if (Vector3.Distance(this.transform.position, Player.transform.position) < attack_area)
        {
            animal_anim.SetTrigger("Attack");
            t_state = AnimalState.Attack;
            rest_Time = 0f;



        }
        else
        {
            this.transform.Translate(Vector3.forward * watchSpeed * Time.deltaTime, Space.Self);

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
            animal_anim.SetTrigger("Move");
            t_state = AnimalState.Watch;
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





}
