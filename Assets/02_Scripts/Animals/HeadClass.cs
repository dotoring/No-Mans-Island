using Unity.VisualScripting;
using UnityEngine;

public class HeadClass : MonoBehaviour
{

    [SerializeField] public TigerClass tiger;




    private void Update()
    {
        if (tiger.t_state == AnimalState.Attack)
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
            tiger.Hit(tiger.animal_atk);

        }
    }



}
