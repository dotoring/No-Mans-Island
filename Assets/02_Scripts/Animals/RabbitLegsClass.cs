using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class RabbitLegsClass : MonoBehaviour
{
    RabbitClass rc;
    XRGrabInteractable animal_grab;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rc = this.transform.root.GetComponent<RabbitClass>();
        animal_grab = this.GetComponent<XRGrabInteractable>();
        animal_grab.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (rc.t_state == AnimalState.Die)
        {
            animal_grab.enabled = true;
        }
    }
}
