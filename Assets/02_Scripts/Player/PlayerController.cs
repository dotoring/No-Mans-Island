using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private InputActionProperty moveAction;
    [SerializeField] private InputActionProperty rotHead;
    private Vector2 inputVec;
    private Quaternion rotQ;
    private Vector3 rotV;

    private int hasstickX = Animator.StringToHash("stickX");
    private int hasstickY = Animator.StringToHash("stickY");

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

  
    

    private void Update()
    {
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
