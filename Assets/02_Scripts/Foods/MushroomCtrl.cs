using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class MushroomCtrl : FoodClass
{
    protected override void Start()
    {
        base.Start();

        rig = GetComponent<Rigidbody>();
        inter = GetComponent<XRGrabInteractable>();

        inter.selectExited.AddListener((var) =>
        {
            rig.isKinematic = false;
        });
    }
}
