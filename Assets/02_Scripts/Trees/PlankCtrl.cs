using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class PlankCtrl : InteractableObject
{
    bool isBuildReady;
    bool isFixed;
    Rigidbody rb;
    XRGrabInteractable interactable;
    [SerializeField] private InputActionProperty leftTriggerAction;
    [SerializeField] private InputActionProperty rightTriggerAction;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        interactable = GetComponent<XRGrabInteractable>();

        interactable.selectExited.AddListener((var) =>
        {
            if (isFixed)
            {
                Fix();
            }
        });
    }

    public override void TakeDamage(int dmg)
    {
        if (isBuildReady)
        {
            Fix();
        }
    }

    void Fix()
    {
        Debug.Log("Fix");
        rb.useGravity = false;
        rb.isKinematic = true;
        isFixed = true;

        interactable.enabled = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("CanBuild"))
        {
            isBuildReady = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("CanBuild"))
        {
            isBuildReady = false;
        }
    }
}
