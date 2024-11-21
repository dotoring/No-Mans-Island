using Photon.Pun;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform tr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        yield return null;
        CreatePlayer();
    }

    // Update is called once per frame
    private void CreatePlayer()
    {
        Vector3 pos = new Vector3(Random.Range(-100.0f, 100.0f), 5.0f, Random.Range(-100.0f, 100.0f));
        GameObject temp =PhotonNetwork.Instantiate("Charactor", pos, Quaternion.identity);
        if (temp.GetPhotonView().IsMine)
        {
            temp.transform.parent= tr;
            temp.transform.position = Vector3.zero;
        }
    }
}
