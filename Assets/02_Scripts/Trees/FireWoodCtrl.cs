using System.Collections;
using UnityEngine;

public class FireWoodCtrl : MonoBehaviour
{
    [SerializeField] float burningTime;
    [SerializeField] GameObject fireEffect;

    //��ȭ
    public void FlameOn()
    {
        //�� ����Ʈ �ѱ�
        fireEffect.SetActive(true);
        //���� �ð����� ��Ÿ�� �������
        Destroy(gameObject, burningTime);
    }
}
