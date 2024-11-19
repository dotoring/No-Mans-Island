using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private InputActionProperty moveAction;
    private Vector2 inputVec;

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
        print("Move : " + inputVec);
        anim.SetFloat(hasstickX, inputVec.x);
        anim.SetFloat(hasstickY, inputVec.y);
    }

    
}
