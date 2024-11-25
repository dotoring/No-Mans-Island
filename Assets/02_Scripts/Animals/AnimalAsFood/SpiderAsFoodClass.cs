using UnityEngine;

public class SpiderAsFoodClass : FoodClass
{
    protected Transform stick;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fullness = 5;
        Reduce_Hp = 3;

    }

    private void Update()
    {
        if (is_Sticked)
        {
            this.transform.parent = stick;
            this.transform.position = stick.position;
            this.transform.rotation = stick.rotation;
        }

    }


}
