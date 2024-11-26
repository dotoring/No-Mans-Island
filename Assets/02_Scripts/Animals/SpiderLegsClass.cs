using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SpiderLegsClass : MonoBehaviour
{
    SpiderClass sc;
    XRGrabInteractable animal_grab;

    private void Start()
    {
        sc = this.transform.root.GetComponent<SpiderClass>();
        animal_grab = this.GetComponent<XRGrabInteractable>();
        animal_grab.enabled = false;
    }


    private void Update()
    {
        if (sc.t_state == AnimalState.Die)
        {
            animal_grab.enabled = true;
        }
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
