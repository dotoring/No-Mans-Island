using System.Collections;
using UnityEngine;

public class FireWoodCtrl : MonoBehaviour
{
    [SerializeField] float burningTime;
    [SerializeField] GameObject fireEffect;

    //점화
    public void FlameOn()
    {
        //불 이펙트 켜기
        fireEffect.SetActive(true);
        //일정 시간동안 불타고 사라지기
        Destroy(gameObject, burningTime);
    }
}
