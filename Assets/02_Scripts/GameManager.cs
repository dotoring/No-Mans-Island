using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    Room ri = PhotonNetwork.CurrentRoom;
    int id = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.2f);
        CreatePlayer();
        yield return new WaitForSeconds(0.2f);
        CreatRock();
    }

    // Update is called once per frame
    void CreatRock()
    {
        Vector3 pos = new Vector3(-5f,1f,0f);
        //PhotonNetwork.Instantiate("Rock", pos, Quaternion.identity);
        PhotonNetwork.InstantiateRoomObject("Rock", pos, Quaternion.identity);
    }

    private void CreatePlayer()
    {
        //Vector3 pos = new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f));
        GameObject obj = PhotonNetwork.Instantiate("Player", spawnPoint.position, Quaternion.identity);
        id = obj.GetComponent<PhotonView>().ViewID;
    }
}
