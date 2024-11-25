using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class FrogClass : StickClass
{
    XRGrabInteractable xrgrab;


    protected float rest_Time;
    protected float damage_Time;

    protected float jump_Time;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        InitStat();
        animal_hp = 30;
        animal_atk = 10;
        corpse_hp = 30;
        is_alive = true;





        rest_Time = 0f;
        jump_Time = 0f;
        xrgrab = GetComponent<XRGrabInteractable>();
        xrgrab.enabled = false;



    }

    // Update is called once per frame
    void Update()
    {
        ShortDistance();
        FrogCheck();
        ThisStick();


        if (corpse_hp <= 0)
        {
            ChangeToMeat();
        }
    }

    private void FrogCheck()
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
            case AnimalState.Damage:
                Animal_Damage();
                break;
            case AnimalState.Die:
                Animal_Die();
                xrgrab.enabled = true;
                break;
        }
    }


    public void Animal_Idle()               // 가만히 서기      // 울음소리 내기
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

            t_state = AnimalState.Watch;

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
            animal_anim.SetTrigger("Move");
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

    public override void GetDamage(int damage)
    {
        if (is_alive)
        {
            animal_hp -= damage;
            print($"{damage} 만큼 피해를 입었습니다. 남은 체력은 {animal_hp} 입니다.");

        }
    }




    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        if (collision.gameObject.CompareTag("Stone"))   // Stone의 공격력을 5로 설정
        {
            GetDamage(5);
            t_state = AnimalState.Damage;
        }
    }




}
