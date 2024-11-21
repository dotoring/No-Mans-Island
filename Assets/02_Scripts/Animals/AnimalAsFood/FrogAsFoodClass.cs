using UnityEngine;

public class FrogAsFoodClass : FoodClass
{



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fullness = 5;
        Reduce_Hp = 3;

    }

    private void Update()
    {
        if (this.GetComponent<FrogClass>().t_state == AnimalState.Die)
        {
            is_Sticked = true;
        }

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CanBuild"))
        {
            if (is_Sticked) this.transform.parent = collision.transform;

        }
    }
}
