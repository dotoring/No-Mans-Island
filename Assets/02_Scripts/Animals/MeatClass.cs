using Unity.VisualScripting;
using UnityEngine;

public class MeatClass : MonoBehaviour
{

    public int fullness;
    public int Reduce_Hp;
    public float cookTime;
    public float infireTime;

    public GameObject roast;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitData();
    }

    // Update is called once per frame
    void Update()
    {
        CookMeat();
    }

    void InitData()
    {
        fullness = 3;
        Reduce_Hp = 5;
        cookTime = 10f;
        infireTime = 0f;
    }

    public void GetFullness(int PlayerFullness)
    {
        PlayerFullness += fullness;
        print($"포만감이 {fullness} 만큼 올랐습니다.");
    }

    public void ReduceHP(int PlayerHP)
    {
        PlayerHP -= Reduce_Hp;

        print($"체력이 {Reduce_Hp} 만큼 감소했습니다.");
    }

    private void CookMeat()
    {

        Collider[] colliders = Physics.OverlapSphere(this.transform.position, 0.1f);
        foreach (Collider col in colliders)
        {
            if (col.name.Contains("Fire"))
            {
                infireTime += Time.deltaTime;
                if (infireTime >= cookTime)
                {
                    infireTime = 0;
                    ChangeToRoast();
                }
            }
        }
    }

    private void ChangeToRoast()
    {
        print("요리가 완성되었습니다.");
        GameObject tmp = Instantiate(roast, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject, 0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player Mouse")
        {
            GetFullness(0);
            ReduceHP(5);
            Destroy(this.gameObject);
        }
    }
}
