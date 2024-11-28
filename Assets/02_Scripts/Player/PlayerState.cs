using Photon.Pun;
using System;
using TMPro;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [SerializeField] int Hp = 100;
    [SerializeField] int hunger = 100;
    [SerializeField] int thirst = 100;
    [SerializeField] int temperature = 100;
    float tik;
    [SerializeField] float tikTime = 3f;
    public bool isCold;

    [SerializeField] TMP_Text tHp;
    [SerializeField] TMP_Text tHunger;
    [SerializeField] TMP_Text tThirst;
    [SerializeField] TMP_Text tTemp;
    [SerializeField] PhotonView pv;

    public event Action<bool> OnDie;
    readonly Observable<bool> isPlayerDie = new Observable<bool>(false);

    private void Start()
    {
        isPlayerDie.AddListener(OnDie);
    }

    private void Update()
    {
        if (pv.IsMine)
        {
            if (tik <= 0)
            {
                if (hunger <= 0 || thirst <= 0)
                {
                    TakeDamage(5);
                }

                if (hunger > 0)
                {
                    DecreaseFullness(1);
                }

                if (thirst > 0)
                {
                    DecreaseThirst(3);
                }

                if (hunger >= 75 && thirst >= 75)
                {
                    RecureHp(5);
                }

                if (isCold)
                {
                    DecreaseTemp(2);
                }
                else
                {
                    IncreaseTemp(2);
                }

                tik = tikTime;
            }
            else
            {
                tik -= Time.deltaTime;
            }

            if (Hp <= 0)
            {
                pv.RPC(nameof(DiePlayer), RpcTarget.AllViaServer);
            }

            tHp.text = "HP " + Hp.ToString();
            tHunger.text = "Hunger " + hunger.ToString();
            tThirst.text = "Thirst " + thirst.ToString();
            tTemp.text = "Temp " + temperature.ToString();
        }
    }

    public void TakeDamage(int dmg)
    {
        Hp -= dmg;
    }

    public void RecureHp(int val)
    {
        Hp += val;
        if (Hp > 100)
        {
            Hp = 100;
        }
    }

    public void IncreaseFullness(int val)
    {
        Debug.Log("������ ���� " + val);
        hunger += val;
        if (hunger > 100)
        {
            hunger = 100;
        }
    }

    public void DecreaseFullness(int val)
    {
        hunger -= val;
        if (hunger < 0)
        {
            hunger = 0;
        }
    }

    public void IncreaseThirst(int val)
    {
        Debug.Log("���� ���� " + val);
        thirst += val;
        if (thirst > 100)
        {
            thirst = 100;
        }
    }

    public void DecreaseThirst(int val)
    {
        thirst -= val;
        if (thirst < 0)
        {

            thirst = 0;
        }
    }

    public void IncreaseTemp(int val)
    {
        temperature += val;
        if (temperature > 100)
        {
            temperature = 100;
        }
    }

    public void DecreaseTemp(int val)
    {
        temperature -= val;
        if (temperature < 0)
        {
            temperature = 0;
        }
    }

    [PunRPC]
    void DiePlayer()
    {
        isPlayerDie.Value = true;
        TempGameMgr.deadPlayerCount++;
    }



    public bool isPoisoned = false;
}
