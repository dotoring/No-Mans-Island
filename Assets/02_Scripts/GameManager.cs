using Photon.Pun;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform tr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.2f);
        CreatePlayer();
    }

    // Update is called once per frame
    private void CreatePlayer()
    {
        Vector3 pos = new Vector3(Random.Range(-10.0f, 10.0f), 1.18f, Random.Range(-10.0f, 10.0f));
        PhotonNetwork.Instantiate("Charactor", pos, Quaternion.identity);
    }
}
