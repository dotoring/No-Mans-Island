using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class HandAnimation : MonoBehaviour
{
    [SerializeField]private bool isleft;
    //public InputActionProperty leftPinch;
    public InputActionProperty leftGrip;

    //public InputActionProperty rightPinch;
    public InputActionProperty rightGrip;

    public Animator lanim;
    public Animator ranim;

    void Update()
    {
        
            //float leftTriggerValue = leftPinch.action.ReadValue<float>();
            //animator.SetFloat("Left Trigger", leftTriggerValue);

            float leftGripValue = leftGrip.action.ReadValue<float>();
            lanim.SetFloat("TriggerL", leftGripValue);
       
            //float rightTriggerValue = rightPinch.action.ReadValue<float>();
            //animator.SetFloat("Right Trigger", rightTriggerValue);

            float rightGripValue = rightGrip.action.ReadValue<float>();
            ranim.SetFloat("TriggerR", rightGripValue);
        
    }
}
