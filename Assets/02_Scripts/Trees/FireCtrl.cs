using UnityEngine;

public class FireCtrl : MonoBehaviour
{
    //�÷��̾ ������ ���� �����ֱ�
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("�� �߰�");
        }
    }

    //�÷��̾ ��ó�� ������ ü�� �÷��ֱ�
    //�÷��̾�� ���� ����or �ҿ��� �÷��̾� ����
    //�ҿ��� �÷��̾ �����ؼ� ü���� �ø��� ��� �Ҹ��� �÷��� �ߺ����� �ö�
    //�÷��̾�� ���� �����ϴ°ɷ�
}
