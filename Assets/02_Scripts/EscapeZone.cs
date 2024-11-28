using UnityEngine;
using Photon.Pun;
using System;

public class EscapeZone : MonoBehaviour
{
    [SerializeField] TempGameMgr TempGameMgr;
    //int playerCount;
    public event Action<int> OnPlayerIn;
    readonly Observable<int> playerCount = new Observable<int>(0);
    [SerializeField] GameObject go;

    private void Start()
    {
        TempGameMgr = FindFirstObjectByType<TempGameMgr>();
        OnPlayerIn += (count) => CheckEscapePlayer(count);
        playerCount.AddListener(OnPlayerIn);
    }

    void CheckEscapePlayer(int count)
    {
        //탈출지역 인원이 생존인원보다 많거나 같을 때
        if(count >= PhotonNetwork.CurrentRoom.PlayerCount - TempGameMgr.deadPlayerCount)
        {
            go.SetActive(true);
        }
        else
        {
            Debug.Log("인원부족");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerCount.Value++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerCount.Value--;
        }
    }
}
