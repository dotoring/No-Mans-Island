using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SpiderLegsClass : MonoBehaviour
{
    SpiderClass sc;


    private void Start()
    {
        sc = this.transform.root.GetComponent<SpiderClass>();

    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (sc.t_state == AnimalState.Attack)
            {
                sc.Hit(sc.animal_atk);
            }


        }
    }
}
