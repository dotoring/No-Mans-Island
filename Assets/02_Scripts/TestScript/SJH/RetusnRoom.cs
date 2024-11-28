using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ReturnRoom : MonoBehaviour
{


    [SerializeField] private XRGrabInteractable inter;
    [SerializeField] private PhotonView pv;
    [SerializeField] private GameObject uiObj;
    void Start()
    {
        inter.hoverEntered.AddListener((args) =>
        {
            UIOpen();
        });
        inter.hoverExited.AddListener((args) =>
        {
            UIClose();
        });
        inter.selectEntered.AddListener((args) => GoRoom());
    }

    private void ReturnLobby()
    {
        PhotonNetwork.LeaveRoom();

    }
    void GoRoom()
    {
        SceneManager.LoadScene("SJHRoomTestScene");
    }

    private void UIOpen()
    {
        uiObj.SetActive(true);
    }
    private void UIClose()
    {
        uiObj.SetActive(false);
    }
}