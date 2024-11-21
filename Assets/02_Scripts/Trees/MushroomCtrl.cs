using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class MushroomCtrl : FoodClass
{
    Rigidbody rb;
    XRGrabInteractable interactable;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        interactable = GetComponent<XRGrabInteractable>();

        interactable.selectExited.AddListener((var) =>
        {
            rb.isKinematic = false;
        });
    }
}
