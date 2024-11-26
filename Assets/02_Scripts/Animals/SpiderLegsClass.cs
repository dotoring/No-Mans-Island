using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SpiderLegsClass : SpiderClass
{








    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (t_state == AnimalState.Attack)
            {
                Hit(animal_atk);
            }


        }
    }
}
