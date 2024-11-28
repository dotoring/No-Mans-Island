using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class LobbyPlayerController : MonoBehaviourPunCallbacks
{

    [SerializeField] private Animator anim;
    [SerializeField] private InputActionProperty moveAction;

    private Vector2 inputVec;

    private int hasstickX = Animator.StringToHash("stickX");//stick의 hash값을 가져와서 저장해두는 변수
    private int hasstickY = Animator.StringToHash("stickY");


    private void Update()
    { 
        //이동시 벡터값을 받아와서 애니메이션에 연결해줌
        inputVec = moveAction.action.ReadValue<Vector2>();

        //카메라 앵글을 따라가도록 함
        Vector3 temp = Camera.main.transform.eulerAngles;
        temp.x = temp.z = 0;
        this.transform.eulerAngles = temp;


        //내 캐릭터의 애니메이션을 보여줌
        anim.SetFloat(hasstickX, inputVec.x);
        anim.SetFloat(hasstickY, inputVec.y);
    }
}
