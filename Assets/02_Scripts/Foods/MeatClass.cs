using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

public class MeatClass : FoodClass
{
    float infireTime;

    private void CookMeat()
    {
        infireTime += Time.deltaTime;
        if (infireTime >= foodSO.cookTime)
        {
            infireTime = 0;
            ChangeToRoast();
        }
    }

    private void ChangeToRoast()
    {
        //GameObject tmp = Instantiate(roast, this.transform.position, this.transform.rotation);

        PhotonNetwork.Instantiate("RoastMeat", transform.position, transform.rotation);
        PhotonNetwork.Destroy(this.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Fire"))
        {
            CookMeat();
        }
    }
}
