using Photon.Pun;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ReturnLobbyTable : MonoBehaviourPunCallbacks
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
        inter.selectEntered.AddListener((args) => ReturnLobby());
    }

    private void OnDestroy()
    {
        inter.hoverEntered.RemoveAllListeners();
        inter.hoverExited.RemoveAllListeners();
        inter.selectEntered.RemoveAllListeners();
    }
    private void ReturnLobby()
    {
        PhotonNetwork.LeaveRoom();
        
    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("SJHLobbyTestScene");
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
