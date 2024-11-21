using UnityEngine;

public class BeeClass : AnimalClass
{
    //XRGrabInteractable xrgrab;


    protected float rest_Time;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        InitStat();
        animal_hp = 30;
        animal_atk = 10;

        is_alive = true;



        rest_Time = 0f;
        //xrgrab = GetComponent<XRGrabInteractable>();
        //xrgrab.enabled = false;



    }

    // Update is called once per frame
    void Update()
    {
        BeeCheck();


        if (corpse_hp <= 0)
        {
            ChangeToMeat();
        }
    }

    private void BeeCheck()
    {
        rest_Time += Time.deltaTime;


        switch (t_state)
        {
            case AnimalState.Idle:
                Animal_Idle();
                break;

            case AnimalState.Watch:
                Animal_Watch();
                break;
            case AnimalState.Attack:
                Animal_Attack();

                break;

            case AnimalState.Die:
                Animal_Die();
                //xrgrab.enabled = true;
                break;
        }
    }


    public void Animal_Idle()
    {


        if (Vector3.Distance(this.transform.position, Player.transform.position) < attack_area)
        {
            t_state = AnimalState.Attack;

        }
        else if (Vector3.Distance(this.transform.position, Player.transform.position) < find_area)
        {


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

        }
    }
    public void Animal_Attack()
    {

        if (Vector3.Distance(this.transform.position, Player.transform.position) > attack_area)
        {
            rest_Time = 0;
            t_state = AnimalState.Watch;


        }
        else
        {
            if (rest_Time >= attack_time)
            {

                rest_Time = 0;
            }
        }
    }




    public void Animal_Die()
    {

        Die();
    }




    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Stone"))   // Stone의 공격력을 5로 설정
        {
            GetDamage(5);

        }
    }
}
