using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class PlayerController : MonoBehaviourPunCallbacks
{

    [SerializeField] private Animator anim;
    [SerializeField] private InputActionProperty moveAction;

    private Vector2 inputVec;
    private Quaternion rotQ;
    private Vector3 rotV;
    [SerializeField] private PhotonView pv;
    [SerializeField] private CharacterController cctr;
    private Rigidbody rb;
    [SerializeField] private GameObject[] models;

    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;
    [SerializeField] private GameObject statusUI;
    [SerializeField] private GameObject statusUISencer;
    [SerializeField] private GameObject mouth;



    private int hasstickX = Animator.StringToHash("stickX");//stick의 hash값을 가져와서 저장해두는 변수
    private int hasstickY = Animator.StringToHash("stickY");



    private void Start()
    {
        //내 객체일 경우에 모델을 꺼주고 
        if (pv.IsMine)
        {
            Transform tr = GameObject.Find("XR Origin (VR)").transform;
            Vector3 temp = transform.position;
            this.transform.root.parent = tr;
            transform.position = Vector3.zero;
            tr.position=temp;
            //손을 XR Origin에 카메라 오프셋 하위로 옮김
            leftHand.transform.parent=tr.GetChild(0).GetChild(1);
            statusUISencer.transform.parent=tr.GetChild(0).GetChild(1);
            rightHand.transform.parent=tr.GetChild(0).GetChild(2);
            statusUI.transform.parent = tr.GetChild(0);
            mouth.transform.parent = Camera.main.transform;
            mouth.transform.localPosition = new Vector3(0, -0.06f, 0.05f);
            //cctr.enabled = false;

            leftHand.transform.localPosition = Vector3.zero;
            rightHand.transform.localPosition = Vector3.zero;
            leftHand.transform.localRotation = Quaternion.identity;
            rightHand.transform.localRotation = Quaternion.identity;
            statusUISencer.transform.localPosition = Vector3.right * 0.2f;

            //룸 씬이면 스테이터스 관련을 모두 끔
            if(SceneManager.GetActiveScene().name== "3_RoomScene")
            {
                GetComponent<PlayerState>().enabled = false;
                statusUISencer.gameObject.SetActive(false);
                statusUI.gameObject.SetActive(false);
            }    



            models[0].SetActive(false);
            models[1].SetActive(false);
        }
        //내 객체가 아닐 경우
        else
        {
            statusUISencer.gameObject.SetActive(false);
            statusUI.gameObject.SetActive(false);
        }
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
}
