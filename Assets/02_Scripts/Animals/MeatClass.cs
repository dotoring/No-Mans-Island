using Unity.VisualScripting;
using UnityEngine;

public class MeatClass : FoodClass
{
    public GameObject roast;

    void Start()
    {
        InitData();
    }

    private void CookMeat()
    {
        infireTime += Time.deltaTime;
        if (infireTime >= cookTime)
        {
            infireTime = 0;
            ChangeToRoast();
        }
    }

    private void ChangeToRoast()
    {
        print("요리가 완성되었습니다.");
        GameObject tmp = Instantiate(roast, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Fire"))
        {
            CookMeat();
        }
    }
}
