using UnityEngine;

public class CuttedCoconutCtrl : FoodClass
{
    protected override void Start()
    {
        base.Start();

        transform.SetParent(null);
    }
}
