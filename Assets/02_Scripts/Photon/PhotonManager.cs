using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    //싱글톤 객체
    private static PhotonManager instance;
    public static PhotonManager Instance
    {
        get
        {
            if(instance == null)
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


        //게임버전 설정
        PhotonNetwork.GameVersion = version;
        //유저명 설정
        PhotonNetwork.NickName = nickname;

        //방장이 씬을 로딩했을 떄 타 유저들에 자동으로 씬을 로딩시켜주는 옵션
        PhotonNetwork.AutomaticallySyncScene = true;

        if (!PhotonNetwork.IsConnected)
        {
            //포톤서버에 접속 요청
            PhotonNetwork.ConnectUsingSettings();
        }

    }//Awake

    private void Start()
    {
        randomRoomBtn.onClick.AddListener(()=>PhotonNetwork.JoinRandomRoom());
        makeRoomBtn.onClick.AddListener(()=>OnMakeRoomButtonClick());
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
        //룸 생성
        PhotonNetwork.CreateRoom(nickname + "'s Room", roomOption);
    }

    #region 포톤콜백함수
        //룸목록을 저장할 Dictionary 선언
    private Dictionary<string, GameObject> roomDict = new Dictionary<string, GameObject>();

    //룸목록이 변경되면 호출되는 콜백
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        //로비에 입장했을때만 호출되는 함수
        foreach (var room in roomList)
        {
            //print($"{room.Name} : {room.PlayerCount} / {room.MaxPlayers}");

            if (room.RemovedFromList == true)//삭제된 룸을 의미 ( 플레이어 상황이 0/0 인경우)
            {
                //룸삭제
                if (roomDict.TryGetValue(room.Name, out GameObject removedRoom))
                {
                    //프리팹 인스턴스 삭제
                    Destroy(removedRoom);
                    //Dictionary에서 키를 삭제
                    roomDict.Remove(room.Name);
                }
                continue;
            }

            //새로 생성된 룸, 변경된 경우 로직 처리
            //처음 생성된 룸(Dictionary 검색했을 때 결과가 없을 경우)
            if (roomDict.ContainsKey(room.Name) == false)
            {
                //room prefab 생성
                var _room = Instantiate(roomPrefab, contentTr);
                roomDict.Add(room.Name, _room);
            }
        }//foreach room

    }//OnRoomListUpdate




    //포톤서버에 접속되었을 떄 호출되는 콜백(CallBack)
    public override void OnConnectedToMaster()
    {
        randomRoomBtn.interactable = true;
        makeRoomBtn.interactable = true;
        exitBtn.interactable = true;
        print("서버접속 완료");
        //Lobby 접속 요청
        PhotonNetwork.JoinLobby();
    }

    //로비에 입장했을 때 호출되는 콜백
    public override void OnJoinedLobby()
    {
        nickname = $"User_{UnityEngine.Random.Range(0, 1001):0000}";
        print("로비 입장 완료");
    }




    //랜덤 방입장 실패했을 때 호출되는 콜백
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        print($"방입장 실패 : {returnCode} : {message}");

        //룸 옵션 설정
        MakeRoom();
    }

    //룸 생성 완료 콜백
    public override void OnCreatedRoom()
    {
        print("룸 생성 완료");
    }

    //룸 입장 완료 콜백
    public override void OnJoinedRoom()
    {
        print($"방 입장 완료 : {PhotonNetwork.CurrentRoom.Name}");

        //PhotonNetwork.Instantiate("Tank", new Vector3(0, 5.0f, 0), Quaternion.identity, 0);//gropid 이게 다르면 만날 수 없음

        if (PhotonNetwork.IsMasterClient)//PhotonNetwork.AutomaticallySyncScene = true;를 설정헀기 떄문에 방장이 씬을 바꾸면 모두가 바뀜
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("SJHRoomTestScene");
        }

    }



    #endregion
}
