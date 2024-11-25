using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;
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

    private int hasstickX = Animator.StringToHash("stickX");
    private int hasstickY = Animator.StringToHash("stickY");

    private void Start()
    {

        //rb.isKinematic = !pv.IsMine;
        if (pv.IsMine)
        {
            models[0].SetActive(false);
            models[1].SetActive(false);
            Vector3 temp = this.transform.position;
            temp.y = 0;
        }
      
        CameraOff(pv.IsMine);
        HandOff(pv.IsMine);
    }

    

    void CameraOff(bool mine)
    {
        pv.transform.GetChild(0).gameObject.SetActive(mine);
    }

    //NEAR-FAR, CONTROLLER�� INPUTACTION,TRACKER ���� �ƴϸ� ����
    private void HandOff(bool isMine)
    {
        //this.transform.Find("Left Controller").GetComponent<ControllerInputActionManager>().enabled = false;
        //this.transform.Find("Left Controller").GetComponent<TrackedPoseDriver>().enabled = false;
        //this.transform.Find("Left_NearFarInteractor").gameObject.SetActive(false);
        //�޼�
        this.transform.GetChild(0).GetComponent<ControllerInputActionManager>().enabled = isMine;
        this.transform.GetChild(0).GetComponent<TrackedPoseDriver>().enabled = isMine;
        this.transform.GetChild(0).GetChild(2).gameObject.SetActive(isMine);
        this.transform.GetChild(0).GetChild(0).GetComponent<Animator>().enabled = isMine;
        //������
        this.transform.GetChild(1).GetComponent<ControllerInputActionManager>().enabled = isMine;
        this.transform.GetChild(1).GetComponent<TrackedPoseDriver>().enabled = isMine;
        this.transform.GetChild(1).GetChild(2).gameObject.SetActive(isMine);
        this.transform.GetChild(1).GetChild(0).GetComponent<Animator>().enabled = isMine;

        pv.transform.GetChild(1).gameObject.SetActive(isMine);
    }


    private void Update()
    {
        if (!pv.IsMine)
            return;

        //�÷��̾� �̵� �ִϸ��̼� ���� ������
        inputVec = moveAction.action.ReadValue<Vector2>();
        //rotQ = rotHead.action.ReadValue<Quaternion>();
       
        //this.transform.forward=Camera.main.transform.forward;
        Vector3 temp = Camera.main.transform.eulerAngles;
       // print("temp1 :"+ temp);
        temp.x = temp.z=0;
        //print("temp2 :" + temp);git 
     
        this.transform.eulerAngles = temp;
        

        //rotV = rotHead.action.ReadValue<Vector3>();
        //print("Move : " + inputVec);
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
        Debug.Log("������ ���� " + val);
    }

    public void DecreaseFullness(int val)
    {
    }

    public void IncreaseThirst(int val)
    {
        Debug.Log("���� ���� " + val);
    }

    public void DecreaseThirst(int val)
    {
    }
}
