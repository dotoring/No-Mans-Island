using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class TestObj : MonoBehaviourPunCallbacks
{
    [SerializeField]XRGrabInteractable di;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        di.selectEntered.AddListener((args) => { SceneManager.LoadScene("3_GameScene"); });
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }
}
