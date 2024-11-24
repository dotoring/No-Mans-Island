using Unity.VisualScripting;
using UnityEngine;

public class MeatClass : FoodClass
{
    public GameObject roast;

    void Start()
    {

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
        if (this.transform.parent.gameObject != null)
        {
            roast.transform.parent = this.transform.parent;
            roast.GetComponent<Rigidbody>().isKinematic = true;
        }
        Destroy(this.gameObject, 0.1f);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Fire"))
        {
            CookMeat();
        }
    }
}
