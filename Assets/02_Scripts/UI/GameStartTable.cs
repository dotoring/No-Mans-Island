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
        inter.hoverEntered.AddListener((args) =>
        {

            if (pv.IsMine)
            {
                pv.RPC(nameof(UIOpen), RpcTarget.AllViaServer);
            }
        });
        inter.hoverExited.AddListener((args) =>
        {
            if (pv.IsMine)
            {
                pv.RPC(nameof(UIClose), RpcTarget.AllViaServer);
            }
        });
        inter.selectEntered.AddListener((args) => GameStart());
    }

    private void GameStart()
    {
        if ((pv.IsMine))
        {
            //SceneManager.LoadScene("3_GameScene");

            //테스트용_CSY
            SceneManager.LoadScene("3_GameScene_Test");

        }
    }

    [PunRPC]
    private void UIOpen()
    {
        uiObj.SetActive(true);
    }
    [PunRPC]
    private void UIClose()
    {
        uiObj.SetActive(false);
    }
}
