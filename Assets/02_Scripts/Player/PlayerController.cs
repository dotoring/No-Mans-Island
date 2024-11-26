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

public class PlayerController : MonoBehaviourPunCallbacks
{

    [SerializeField] private Animator anim;
    [SerializeField] private InputActionProperty moveAction;

    private Vector2 inputVec;
    private Quaternion rotQ;
    private Vector3 rotV;
    [SerializeField]private PhotonView pv;
    private Rigidbody rb;
    [SerializeField] private GameObject[] models;

    [SerializeField] private GameObject leftCont;
    [SerializeField] private GameObject rightCont;



    private int hasstickX = Animator.StringToHash("stickX");//stick의 hash값을 가져와서 저장해두는 변수
    private int hasstickY = Animator.StringToHash("stickY");



    private void Start()
    {
        //내 객체일 경우에 모델을 꺼주고 
        if (pv.IsMine)
        {
            models[0].SetActive(false);
            models[1].SetActive(false);
        }
      
        NotMine(pv.IsMine);
    }


    private void NotMine(bool isMine)
    {
        //내 카메라가 아니면 모두 끈다
        pv.transform.GetChild(0).GetChild(0).gameObject.SetActive(isMine);

        //왼손과 오른손의 컨트롤 관련된 부분을 모두 끈다
        //왼손
        leftCont.transform.GetComponent<ControllerInputActionManager>().enabled = isMine;
        leftCont.transform.GetComponent<TrackedPoseDriver>().enabled = isMine;
        leftCont.transform.GetChild(2).gameObject.SetActive(isMine);
        //오른손
       rightCont.transform.GetComponent<ControllerInputActionManager>().enabled = isMine;
       rightCont.transform.GetComponent<TrackedPoseDriver>().enabled = isMine;
       rightCont.transform.GetChild(2).gameObject.SetActive(isMine);

        //컨트롤러의 움직임을 모든 객체가 받기 때문에 꺼준다
        //Locomotion
        pv.transform.GetChild(1).gameObject.SetActive(isMine);
    }


    private void Update()
    {
        //내 객체가 아니면 작동하지않는다
        if (!pv.IsMine)
            return;

        //이동시 벡터값을 받아와서 애니메이션에 연결해줌
        inputVec = moveAction.action.ReadValue<Vector2>();

        //카메라 앵글을 따라가도록 함
        Vector3 temp = Camera.main.transform.eulerAngles;
        temp.x = temp.z=0;
        this.transform.eulerAngles = temp;
        

        //내 캐릭터의 애니메이션을 보여줌
        anim.SetFloat(hasstickX, inputVec.x);
        anim.SetFloat(hasstickY, inputVec.y);
    }

    


    public void IncreaseHp(int val)
    {

    }
    public void DecreaseHp(int val)
    {

    }

    public void IncreaseFullness(int val)
    {
        Debug.Log("포만도 증가 " + val);
    }

    public void DecreaseFullness(int val)
    {
    }

    public void IncreaseThirst(int val)
    {
        Debug.Log("수분 섭취 " + val);
    }

    public void DecreaseThirst(int val)
    {
    }
}
