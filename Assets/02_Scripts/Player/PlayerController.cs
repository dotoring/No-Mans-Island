using Photon.Pun;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class PlayerController : MonoBehaviour
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
            Transform tr =GameObject.Find("XR Origin (VR)").transform;
            Vector3 temp = this.transform.position;
            temp.y = 0;
            this.transform.parent = tr;
            this.transform.position = new Vector3(0, 0f, 0);
            tr.position= temp;
        }
    }



    private void Update()
    {
        if (!pv.IsMine)
            return;
        //플레이어 이동 애니메이션 연결 적용중
        inputVec = moveAction.action.ReadValue<Vector2>();
        //rotQ = rotHead.action.ReadValue<Quaternion>();
       
        //this.transform.forward=Camera.main.transform.forward;
        Vector3 temp = Camera.main.transform.eulerAngles;
       // print("temp1 :"+ temp);
        temp.x = temp.z=0;
        //print("temp2 :" + temp);
     
        this.transform.eulerAngles = temp;
        

        //rotV = rotHead.action.ReadValue<Vector3>();
        //print("Move : " + inputVec);
        anim.SetFloat(hasstickX, inputVec.x);
        anim.SetFloat(hasstickY, inputVec.y);
    }

    
}
