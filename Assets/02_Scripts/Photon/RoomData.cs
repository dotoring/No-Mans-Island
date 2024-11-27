using Photon.Realtime;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomData : MonoBehaviour
{
    [SerializeField] private TMP_Text _roomTxt;

    private RoomInfo roomInfo;
    public RoomInfo RoomInfo
    {
        get { return roomInfo; }
        set
        {
            roomInfo = value;
            _roomTxt.text = $"{roomInfo.Name} : {roomInfo.PlayerCount} / {roomInfo.MaxPlayers}";
            GetComponent<Button>().onClick.AddListener(() =>
            {
                PhotonNetwork.JoinRoom(roomInfo.Name);
            });
        }
    }
    private void Awake()
    {
        _roomTxt = GetComponentInChildren<TMP_Text>();
    }
}
