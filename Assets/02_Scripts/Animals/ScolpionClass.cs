using UnityEngine;

public class ScolpionClass : AnimalClass
{
    //XRGrabInteractable xrgrab;


    protected float rest_Time;
    protected float damage_Time;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        InitStat();
        animal_hp = 30;
        animal_atk = 10;
        corpse_hp = 30;
        is_alive = true;
        attack_area = 2.5f;



        rest_Time = 0f;
        //xrgrab = GetComponent<XRGrabInteractable>();
        //xrgrab.enabled = false;



    }

    public override void InitStat()
    {
        base.InitStat();
        animal_anim = this.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ScolpionCheck();



    }

    private void ScolpionCheck()
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
                //xrgrab.enabled = true;
                break;
        }
    }


    public void Animal_Idle()
    {
        if (rest_Time >= 5f)
        {
            rest_Time = 0;

            t_state = AnimalState.Move;
            animal_anim.SetTrigger("Move");
        }

        if (Vector3.Distance(this.transform.position, Player.transform.position) < attack_area)
        {
            t_state = AnimalState.Attack;

        }
        else if (Vector3.Distance(this.transform.position, Player.transform.position) < find_area)
        {
            rest_Time = 0;

            t_state = AnimalState.Watch;

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
            t_state = AnimalState.Attack;

        }
        else
        {
            this.transform.Translate(Vector3.forward * 1.0f * Time.deltaTime, Space.Self);
            animal_anim.SetTrigger("Move");
        }
    }
    public void Animal_Attack()
    {
        rest_Time += Time.deltaTime;
        if (Vector3.Distance(this.transform.position, Player.transform.position) > attack_area)
        {
            rest_Time = 0;
            t_state = AnimalState.Watch;


        }
        else
        {
            if (rest_Time >= attack_time)
            {
                animal_anim.SetTrigger("Attack");
                rest_Time = 0;
            }
        }
    }
    public void Animal_Damage()
    {
        if (damage_Time >= 5f)
        {
            damage_Time = 0;
            animal_anim.SetTrigger("Damage");
        }

        if (animal_hp <= 0) t_state = AnimalState.Die;
        else
        {
            t_state = AnimalState.Idle;
            animal_anim.SetTrigger("Idle");
        }


    }



    public void Animal_Die()
    {
        animal_anim.SetTrigger("Die");
        Die();
    }




    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Stone"))   // Stone의 공격력을 5로 설정
        {
            GetDamage(5);
            t_state = AnimalState.Damage;
        }
    }
}
