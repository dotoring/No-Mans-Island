using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    Room ri = PhotonNetwork.CurrentRoom;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.2f);
        CreatePlayer();
    }

    // Update is called once per frame
    

    private void CreatePlayer()
    {
        //Vector3 pos = new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f));
        PhotonNetwork.Instantiate("Charactor", spawnPoint.position, Quaternion.identity);
    }
}
