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
            //������Ʈ�� PhotonView���� Ownership Transfer�� Takeover�� �����ϸ� ������(��Ʈ�ѷ� ����)�� ������ ������ �� �ֵ��� �Ѵ�
            //TransferOwnership(Player) -> ���� PhotonView�� �������� Player�� �ٲٴ� �Լ�
            SceneManager.LoadScene("3_GameScene");
        });
    }

}
