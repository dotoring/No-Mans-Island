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
    [SerializeField] private GameObject statusUI;
    [SerializeField] private GameObject mouth;



    private int hasstickX = Animator.StringToHash("stickX");//stick�� hash���� �����ͼ� �����صδ� ����
    private int hasstickY = Animator.StringToHash("stickY");



    private void Start()
    {
        //�� ��ü�� ��쿡 ���� ���ְ� 
        if (pv.IsMine)
        {
            Transform tr = GameObject.Find("XR Origin (VR)").transform;
            Vector3 temp = transform.position;
            this.transform.root.parent = tr;
            transform.position = Vector3.zero;
            tr.position=temp;
            //���� XR Origin�� ī�޶� ������ ������ �ű�
            leftCont.transform.parent=tr.GetChild(0);
            rightCont.transform.parent=tr.GetChild(0);
            statusUI.transform.parent = tr.GetChild(0);
            mouth.transform.parent = Camera.main.transform;
            mouth.transform.localPosition = new Vector3(0, -0.06f, 0.05f);


            models[0].SetActive(false);
            models[1].SetActive(false);
        }
        //�� ��ü�� �ƴ� ���
        else
        {
            leftCont.transform.parent = null;
            rightCont.transform.parent =null;
            statusUI.transform.parent = null;
        }

      
        NotMine(pv.IsMine);
    }


    private void NotMine(bool isMine)
    {
        //�� ī�޶� �ƴϸ� ��� ����
        //pv.transform.GetChild(0).GetChild(0).gameObject.SetActive(isMine);

        //�޼հ� �������� ��Ʈ�� ���õ� �κ��� ��� ����
        //�޼�
        leftCont.transform.GetComponent<ControllerInputActionManager>().enabled = isMine;
        leftCont.transform.GetComponent<TrackedPoseDriver>().enabled = isMine;
        leftCont.transform.GetChild(2).gameObject.SetActive(isMine);
        //������
        rightCont.transform.GetComponent<ControllerInputActionManager>().enabled = isMine;
        rightCont.transform.GetComponent<TrackedPoseDriver>().enabled = isMine;
        rightCont.transform.GetChild(2).gameObject.SetActive(isMine);

        //��Ʈ�ѷ��� �������� ��� ��ü�� �ޱ� ������ ���ش�
        //Locomotion
        //pv.transform.GetChild(1).gameObject.SetActive(isMine);
    }


    private void Update()
    {
        //�� ��ü�� �ƴϸ� �۵������ʴ´�
        if (!pv.IsMine)
            return;

        //�̵��� ���Ͱ��� �޾ƿͼ� �ִϸ��̼ǿ� ��������
        inputVec = moveAction.action.ReadValue<Vector2>();

        //ī�޶� �ޱ��� ���󰡵��� ��
        Vector3 temp = Camera.main.transform.eulerAngles;
        temp.x = temp.z=0;
        this.transform.eulerAngles = temp;
        

        //�� ĳ������ �ִϸ��̼��� ������
        anim.SetFloat(hasstickX, inputVec.x);
        anim.SetFloat(hasstickY, inputVec.y);
    }
}
