using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class StartTest : PhotonGrabObject
{
    List<IXRInteractor> a=new List<IXRInteractor>();

    string Gn;

    protected override void Start()
    {
        base.Start();
        inter.hoverEntered.AddListener((args) => 
        {
            inter.interactionManager.GetInteractionGroup(Gn);
        });
        inter.hoverExited.AddListener((args) =>
        {
            inter.interactionManager.GetInteractionGroup(Gn);
            //inter.interactionManager.GetRegisteredInteractors(a);
        });
    }

    private void Update()
    {
        
        
        
        

        print(Gn);
    }

    void GameStart()
    {
        SceneManager.LoadScene("3_GameScene");
    }
}
