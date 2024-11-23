using Unity.VisualScripting;
using UnityEngine;

public class MeatClass : FoodClass
{



    public GameObject roast;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        fullness = 3;
        Reduce_Hp = 5;
        cookTime = 10f;
        infireTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        CookMeat();
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
        if (this.transform.parent.gameObject != null)
        {
            roast.transform.parent = this.transform.parent;
            roast.GetComponent<Rigidbody>().isKinematic = true;
        }
        Destroy(this.gameObject, 0.1f);
    }


}
