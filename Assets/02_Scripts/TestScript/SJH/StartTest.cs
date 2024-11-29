using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class StartTest : PhotonGrabObject
{

    protected override void Start()
    {
       
    }

    void GameStart()
    {
        SceneManager.LoadScene("3_GameScene");
    }
}
