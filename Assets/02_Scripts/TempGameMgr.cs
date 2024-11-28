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


    public void SpawnAnimals()
    {
        foreach (var animal in nightAnimals)
        {
            Vector3 randomPoint = UnityEngine.Random.insideUnitCircle * 5;
            randomPoint += player.transform.position;
            randomPoint.y = 100f;
            RaycastHit hit;
            Physics.Raycast(randomPoint, Vector3.down, out hit);

            PhotonNetwork.Instantiate(animal, hit.point, Quaternion.identity);
        }
    }

    public static void GameOver()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount <= deadPlayerCount)
            if (PhotonNetwork.CurrentRoom.MasterClientId == PhotonNetwork.LocalPlayer.ActorNumber)
                SceneManager.LoadScene("4_RoomScene");
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
