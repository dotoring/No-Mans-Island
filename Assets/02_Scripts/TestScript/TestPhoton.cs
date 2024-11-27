using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class TestPhoton : MonoBehaviourPunCallbacks
{
    //�̱��� ��ü
    private static TestPhoton instance;
    public static TestPhoton Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    private const string version = "1.0";
    [SerializeField] private string nickname = " ";

    [Header("Button")]
    [SerializeField] private Button randomRoomBtn;
    [SerializeField] private Button makeRoomBtn;
    [SerializeField] private Button exitBtn;

    [Header("Room List")]
    [SerializeField] private GameObject roomPrefab;
    [SerializeField] private Transform contentTr;

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        randomRoomBtn.interactable = false;
        makeRoomBtn.interactable = false;
        exitBtn.interactable = false;


        //���ӹ��� ����
        PhotonNetwork.GameVersion = version;
        //������ ����
        PhotonNetwork.NickName = nickname;

        //������ ���� �ε����� �� Ÿ �����鿡 �ڵ����� ���� �ε������ִ� �ɼ�
        PhotonNetwork.AutomaticallySyncScene = true;

        if (!PhotonNetwork.IsConnected)
        {
            //���漭���� ���� ��û
            PhotonNetwork.ConnectUsingSettings();
        }

    }//Awake

    private void Start()
    {
        randomRoomBtn.onClick.AddListener(() => PhotonNetwork.JoinRandomRoom());
        makeRoomBtn.onClick.AddListener(() => OnMakeRoomButtonClick());
        exitBtn.onClick.AddListener(() => Application.Quit());
    }

    private void OnMakeRoomButtonClick()
    {
        MakeRoom();
    }

    void MakeRoom()
    {
        var roomOption = new RoomOptions
        {
            MaxPlayers = 20,
            IsOpen = true,
            IsVisible = true,
        };
        //�� ����
        PhotonNetwork.CreateRoom(nickname + "'s Room", roomOption);
    }

    #region �����ݹ��Լ�
    //������ ������ Dictionary ����
    private Dictionary<string, GameObject> roomDict = new Dictionary<string, GameObject>();

    //������ ����Ǹ� ȣ��Ǵ� �ݹ�
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        //�κ� ������������ ȣ��Ǵ� �Լ�
        foreach (var room in roomList)
        {
            //print($"{room.Name} : {room.PlayerCount} / {room.MaxPlayers}");

            if (room.RemovedFromList == true)//������ ���� �ǹ� ( �÷��̾� ��Ȳ�� 0/0 �ΰ��)
            {
                //�����
                if (roomDict.TryGetValue(room.Name, out GameObject removedRoom))
                {
                    //������ �ν��Ͻ� ����
                    Destroy(removedRoom);
                    //Dictionary���� Ű�� ����
                    roomDict.Remove(room.Name);
                }
                continue;
            }
            //�븮��Ʈ���� �ִµ� dic�� ���� �� (�űԷ�)
            if (roomDict.ContainsKey(room.Name) == false)
            {
                var _room = Instantiate(roomPrefab, contentTr);
                _room.GetComponent<RoomData>().RoomInfo = room;
                roomDict.Add(room.Name, _room);
            }
            //�̹� �ִµ� ���ڰ� ������ ���
            if (roomDict.ContainsKey(room.Name))
            {
                if (roomDict[room.Name].GetComponent<RoomData>().RoomInfo.PlayerCount == room.PlayerCount)
                    continue;
                roomDict[room.Name].GetComponent<RoomData>().RoomInfo = room;
            }

        }//foreach room

    }//OnRoomListUpdate




    //���漭���� ���ӵǾ��� �� ȣ��Ǵ� �ݹ�(CallBack)
    public override void OnConnectedToMaster()
    {
        randomRoomBtn.interactable = true;
        makeRoomBtn.interactable = true;
        exitBtn.interactable = true;
        print("�������� �Ϸ�");
        //Lobby ���� ��û
        PhotonNetwork.JoinLobby();
    }

    //�κ� �������� �� ȣ��Ǵ� �ݹ�
    public override void OnJoinedLobby()
    {
        nickname = $"User_{UnityEngine.Random.Range(0, 1001):0000}";
        print("�κ� ���� �Ϸ�");
    }




    //���� ������ �������� �� ȣ��Ǵ� �ݹ�
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        print($"������ ���� : {returnCode} : {message}");

        //�� �ɼ� ����
        MakeRoom();
    }

    //�� ���� �Ϸ� �ݹ�
    public override void OnCreatedRoom()
    {
        print("�� ���� �Ϸ�");
    }

    //�� ���� �Ϸ� �ݹ�
    public override void OnJoinedRoom()
    {
        print($"�� ���� �Ϸ� : {PhotonNetwork.CurrentRoom.Name}");

        //PhotonNetwork.Instantiate("Tank", new Vector3(0, 5.0f, 0), Quaternion.identity, 0);//gropid �̰� �ٸ��� ���� �� ����

        if (PhotonNetwork.IsMasterClient)//PhotonNetwork.AutomaticallySyncScene = true;�� �������� ������ ������ ���� �ٲٸ� ��ΰ� �ٲ�
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Ex_TestRoom_CSY");
        }

    }



    #endregion
}