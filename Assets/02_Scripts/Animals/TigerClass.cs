using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

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

    //XRGrabInteractable xrgrab;


    float rest_Time;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        InitStat();
        animal_hp = 30;
        animal_atk = 10;
        corpse_hp = 30;
        is_alive = true;

        t_state = TigerState.Idle;

        rest_Time = 0f;
        //xrgrab = GetComponent<XRGrabInteractable>();
        //xrgrab.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        TigerCheck();


        if (corpse_hp <= 0)
        {
            ChangeToMeat();
        }
    }

    private void TigerCheck()
    {
        rest_Time += Time.deltaTime;
        switch (t_state)
        {
            case TigerState.Idle:

                break;
            case TigerState.Move:

                break;
            case TigerState.Watch:

                break;
            case TigerState.Attack:


                break;
            case TigerState.Damage:

                if (animal_hp <= 0) t_state = TigerState.Die;

                break;
            case TigerState.Die:

                //xrgrab.enabled = true;
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
