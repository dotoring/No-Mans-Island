using System.Collections;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class FireWoodCtrl : PhotonGrabObject
{
    [SerializeField] float burningTime;
    [SerializeField] GameObject fireEffect;

    //점화
    [PunRPC]
    public void FlameOn()
    {
        //불 이펙트 켜기
        fireEffect.SetActive(true);
        //일정 시간동안 불타고 사라지기
        Destroy(gameObject, burningTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Fire"))
        {
            FlameOn();
            pv.RPC(nameof(FlameOn), RpcTarget.AllViaServer);
        }
    }
}
