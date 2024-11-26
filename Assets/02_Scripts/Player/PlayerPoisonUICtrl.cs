using UnityEngine;

public class PlayerPoisonUICtrl : MonoBehaviour
{
    [SerializeField] GameObject controller;
    [SerializeField] GameObject PoisonImg;

    [SerializeField] PlayerState player_s;

    void Update()
    {
        transform.localPosition = controller.transform.localPosition + Vector3.up * 0.2f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (player_s.isPoisoned) PoisonImg.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        PoisonImg.SetActive(false);
    }
}


// 해당 스크립트는 Scorpion의 공격의 효과로
// 독에 중독되었을 때 상태창 UI에 독 관련 UI를 띄우게 하는 스크립트입니다.
// 혹시 모르는 Conflict를 우려하여 따로 빼놓았음을 알립니다.
