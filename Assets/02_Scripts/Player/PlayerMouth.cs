using UnityEngine;

public class PlayerMouth : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Food"))
        {
            //������ ��ġ��ŭ ȸ�� �Լ���
            Destroy(other.gameObject);
        }
    }
}
