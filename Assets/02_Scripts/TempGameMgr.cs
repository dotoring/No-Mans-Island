using UnityEngine;
using Photon.Pun;
using System;
using UnityEngine.SceneManagement;

public class TempGameMgr : MonoBehaviour
{
    [SerializeField] string[] nightAnimals;
    [SerializeField] GameObject player;
    public static int deadPlayerCount = 0;

    Action<bool> gameOver;

    private void Start()
    {
        deadPlayerCount = 0;
    }
    public void SpawnAnimals()
    {
        foreach (var animal in nightAnimals)
        {
            Vector3 randomPoint = UnityEngine.Random.insideUnitCircle * 5;
            randomPoint += player.transform.position;
            randomPoint.y = 100f;
            RaycastHit hit;
            Physics.Raycast(randomPoint, Vector3.down, out hit, 150f, LayerMask.NameToLayer("Land"));

            PhotonNetwork.Instantiate(animal, hit.point, Quaternion.identity);
        }
    }

    public static void GameOver()
    {
        print("Á×Àº»ç¶÷¼ö : "+deadPlayerCount);
        if (PhotonNetwork.CurrentRoom.PlayerCount <= deadPlayerCount)
            if (PhotonNetwork.CurrentRoom.MasterClientId == PhotonNetwork.LocalPlayer.ActorNumber)
            { 
                SceneManager.LoadScene("4_RoomScene");
                PhotonNetwork.CurrentRoom.IsVisible = true;
                PhotonNetwork.CurrentRoom.IsOpen = true;
            }
    }


    public void OnDay()
    {
        player.GetComponentInChildren<PlayerState>().isCold = false;
    }

    public void OnNight()
    {
        player.GetComponentInChildren<PlayerState>().isCold = true;
    }
}
