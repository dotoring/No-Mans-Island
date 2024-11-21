using UnityEngine;

public class TailClass : ScolpionClass
{
    private void Update()
    {
        if (t_state == AnimalState.Attack)
        {
            this.GetComponent<SphereCollider>().enabled = true;
        }
        else
        {
            this.GetComponent<SphereCollider>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Hit(animal_atk);

        }
    }
}
