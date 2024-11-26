using UnityEngine;

public class TailClass : MonoBehaviour
{
    [SerializeField] ScolpionClass scorpion;
    private void Update()
    {
        if (scorpion.t_state == AnimalState.Attack)
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
            scorpion.Hit(scorpion.animal_atk);

        }
    }
}
