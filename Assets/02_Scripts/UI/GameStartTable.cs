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
                UIOpen();
            }
        });
        inter.hoverExited.AddListener((args) =>
        {
            UIClose();
        });
        inter.selectEntered.AddListener((args) => GameStart());
    }

    private void GameStart()
    {
        if ((pv.IsMine))
        {
            //SceneManager.LoadScene("3_GameScene");

            //테스트용_CSY
            SceneManager.LoadScene("4_GameScene");
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.CurrentRoom.IsOpen    = false;

        }
    }

    private void OnDestroy()
    {
        inter.hoverEntered.RemoveAllListeners();
        inter.hoverExited.RemoveAllListeners();
        inter.selectEntered.RemoveAllListeners();
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
