using Photon.Pun;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class GameStartTable : MonoBehaviour
{

    [SerializeField] private XRGrabInteractable inter;
    [SerializeField] private PhotonView pv;
    [SerializeField] private GameObject uiObj;
    void Start()
    {
        inter.hoverEntered.AddListener((args) => pv.RPC(nameof(UIOpen), RpcTarget.AllViaServer));
        inter.selectEntered.AddListener((args) => GameStart());
        inter.hoverExited.AddListener((args) => pv.RPC(nameof(UIClose), RpcTarget.AllViaServer));
    }

    private void GameStart()
    {
        if ((pv.IsMine))
        {
            SceneManager.LoadScene("3_GameScene");
        }
    }

    [PunRPC]
    private void UIOpen()
    {
        if ((pv.IsMine))
        {
            uiObj.SetActive(true);
        }
    }
    [PunRPC]
    private void UIClose()
    {
        if ((pv.IsMine))
        {
            uiObj.SetActive(false);
        }
    }
}
