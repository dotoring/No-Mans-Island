using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class TestObj : PhotonGrabObject
{
    protected override void Start()
    {
        base.Start();
        inter.selectEntered.AddListener((args) =>
        {
            if (pv.Owner.IsMasterClient) { }
                //SceneManager.LoadScene("3_GameScene");
        });
    }

    protected override void SelectFunc()
    {
        base.SelectFunc();
        print("sel함수 호출 : "+PhotonNetwork.CurrentRoom.Players[pv.Owner.ActorNumber].IsMasterClient);
    }

    private void Update()
    {
        print(PhotonNetwork.CurrentRoom.Players[pv.Owner.ActorNumber].IsMasterClient);
    }


}
