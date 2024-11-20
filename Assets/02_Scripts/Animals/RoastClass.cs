using UnityEngine;

public class RoastClass : FoodClass
{
    public int fullness_roast = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitData();
        fullness = fullness_roast;
    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player Mouse")
        {
            GetFullness(0);

            Destroy(this.gameObject);
        }
    }
}
