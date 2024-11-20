using System.Collections;
using UnityEngine;

public class FireWoodCtrl : MonoBehaviour
{
    [SerializeField] float burningTime;
    [SerializeField] GameObject fireEffect;
    bool isBurn = false;

    //��ȭ
    public void FlameOn()
    {
        //�� ����Ʈ �ѱ�
        fireEffect.SetActive(true);
        isBurn = true;
        //���� �ð����� ��Ÿ�� �������
        Destroy(gameObject, burningTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Fire"))
        {
            FlameOn();
        }
    }
}
