using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public string playerName;

    public string gameVersion;

    public string roomName;
    [Range(1, 10)]
    public int countPlayerInRoom;

    public List<RoomInfo> roomList;

    private void Start()
    {
        roomList = new List<RoomInfo>();

        PhotonNetwork.AutomaticallySyncScene = true;

        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }   
    }

    public void OnGUI()
    {
        if (!PhotonNetwork.IsConnected || PhotonNetwork.InRoom)
            return;

        if (GUI.Button(new Rect(10, 10, 100, 30), "Update"))
            PhotonNetwork.JoinLobby(TypedLobby.Default);

        roomName = GUI.TextField(new Rect(120, 10, 100, 30), roomName);

        if (GUI.Button(new Rect(220, 10, 100, 30), "Create room"))
        {
            RoomOptions option = new RoomOptions();
            option.IsOpen = true;
            option.IsVisible = true;
            option.MaxPlayers = 16;
            
            PhotonNetwork.CreateRoom(roomName, option);
        }

        for (int i = 0; i < roomList.Count; i++)
        {
            GUI.Box(new Rect(10, 45 + (i * 30), 100, 30), roomList[i].Name);
            GUI.Box(new Rect(110, 45 + (i * 30), 100, 30), roomList[i].IsOpen.ToString());
            GUI.Box(new Rect(210, 45 + (i * 30), 100, 30), string.Format("{0}/{1}", roomList[i].PlayerCount, roomList[i].MaxPlayers));
            
            
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("connect to lobby");

        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogError(string.Format("DISCONNECTED ERROR: {0}", cause.ToString()));
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        this.roomList = roomList;
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("connect to room");
    }
}
