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
            //오브젝트의 PhotonView에서 Ownership Transfer를 Takeover로 설정하면 소유권(컨트롤러 포함)을 강제로 가져올 수 있도록 한다
            //TransferOwnership(Player) -> 현재 PhotonView의 소유권을 Player로 바꾸는 함수
            SceneManager.LoadScene("3_GameScene");
        });
    }

}
