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
       


        //�߷��� ������̸� ������ ���� ���̰�
        //�߷��� ������� �ƴϸ� ���� ���̴�
        if (rig.useGravity)
        {
            print("������");
            return;
        }
        else
        {
            print("����");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (rig.useGravity)
           return;
        
        //�������� �޴� ������Ʈ�� ü���� ���
        //���������� ��� �볪���� ���� ������ �����
        
    }
    
}
