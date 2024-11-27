using Photon.Pun;
using UnityEngine;
using Photon.Realtime;

public class InstatiateScorpion : MonoBehaviour
{
    [SerializeField] GameObject scorpion;
    [SerializeField] Transform spawnPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        PhotonNetwork.Instantiate("Scorpion", spawnPoint.position, Quaternion.identity);
    }
}
