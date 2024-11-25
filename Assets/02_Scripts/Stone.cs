using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Stone : MonoBehaviour
{
    [SerializeField] int childcount;
    private Rigidbody rig;
    [SerializeField]XRGrabInteractable inter;

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        inter.hoverEntered.AddListener((args) => { print(args+"Hover"); });
        inter.selectEntered.AddListener((args) => { print(args+"Select"); });
    }


    private void Update()
    {
       


        //중력을 사용중이면 잡히지 않은 것이고
        //중력을 사용중이 아니면 잡힌 것이다
        if (rig.useGravity)
        {
            print("안잡힙");
            return;
        }
        else
        {
            print("잡힘");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (rig.useGravity)
           return;
        
        //데미지를 받는 오브젝트면 체력을 깎고
        //나무판자의 경우 대나무랑 같이 있으면 연결됨
        
    }
    
}
