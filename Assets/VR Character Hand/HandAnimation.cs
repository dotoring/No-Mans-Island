using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class HandAnimation : MonoBehaviour
{
    public InputActionProperty leftPinch;
    public InputActionProperty leftGrip;

    public InputActionProperty rightPinch;
    public InputActionProperty rightGrip;

    public Animator lanim;
    public Animator ranim;

    void Update()
    {
        
            float leftTriggerValue = leftPinch.action.ReadValue<float>();
            lanim.SetFloat("TriggerL", leftTriggerValue);

            float leftGripValue = leftGrip.action.ReadValue<float>();
            lanim.SetFloat("GripL", leftGripValue);
       
            float rightTriggerValue = rightPinch.action.ReadValue<float>();
            ranim.SetFloat("TriggerR", rightTriggerValue);

            float rightGripValue = rightGrip.action.ReadValue<float>();
            ranim.SetFloat("GripR", rightGripValue);
        
    }
}
