using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class RoomManager : MonoBehaviourPunCallbacks
{
    private string mapType;

    public TextMeshProUGUI OccupancyRateText_ForSchool;
    public TextMeshProUGUI OccupancyRateText_ForOutdoor;

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    public void JoinHall()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        if (!PhotonNetwork.IsConnectedAndReady)
        {
            Debug.Log("Room manager Start 2");
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            Debug.Log("Room manager Start 3");
            PhotonNetwork.JoinLobby();
        }
    }

    public void GoToNewRoom()
    {
        ExitGames.Client.Photon.Hashtable expectedCustomProperties = new ExitGames.Client.Photon.Hashtable() { { MultiplayerVRConstants.MAP_TYPE_KEY, mapType } };
        PhotonNetwork.JoinRandomRoom(expectedCustomProperties, 0);
    }

    public void OnEnterHall()
    {
        mapType = MultiplayerVRConstants.MAP_TYPE_VALUE_HALL;
        GoToNewRoom();
    }
    
    public void OnEnterButtonClickedOutdoor()
    {
        mapType = MultiplayerVRConstants.MAP_TYPE_VALUE_OUTDOOR;
        GoToNewRoom();
    }
    
    public void OnEnterButtonClickedSchool()
    {
        mapType = MultiplayerVRConstants.MAP_TYPE_VALUE_SCHOOL;
        GoToNewRoom();
    }

    #region Photon Callback Methods 

    public override void OnJoinedLobby()
    {
        OnEnterHall();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Connect Failed. " + message);
        CreateAndJoinRoom();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to servers again.");
        PhotonNetwork.JoinLobby();
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("A room is created: " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("The Local player: " + PhotonNetwork.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name + " Player count " + PhotonNetwork.CurrentRoom.PlayerCount);

        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(MultiplayerVRConstants.MAP_TYPE_KEY))
        {
            object mapType;
            if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(MultiplayerVRConstants.MAP_TYPE_KEY, out mapType))
            {
                Debug.Log("Joined room with the map: " + (string)mapType);
                if ((string)mapType == MultiplayerVRConstants.MAP_TYPE_VALUE_SCHOOL)
                {
                    PhotonNetwork.LoadLevel("World_School");
                }
                else if ((string)mapType == MultiplayerVRConstants.MAP_TYPE_VALUE_OUTDOOR)
                {
                    PhotonNetwork.LoadLevel("World_Outdoor");
                }
                else if((string)mapType == MultiplayerVRConstants.MAP_TYPE_VALUE_HALL)
                {
                    PhotonNetwork.LoadLevel("HallScene");
                }
            }
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("A new player joined in room");
    }
    #endregion

    #region Private Methods
    private void CreateAndJoinRoom()
    {
        string randomRoomName = "Room_" + mapType + Random.Range(0, 10000);
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 20;

        string[] roomPropsInLobby = { MultiplayerVRConstants.MAP_TYPE_KEY };

        ExitGames.Client.Photon.Hashtable customRoomProperties = new ExitGames.Client.Photon.Hashtable() { { MultiplayerVRConstants.MAP_TYPE_KEY, mapType } };

        roomOptions.CustomRoomPropertiesForLobby = roomPropsInLobby;
        roomOptions.CustomRoomProperties = customRoomProperties;

        PhotonNetwork.CreateRoom(randomRoomName, roomOptions);
    }
    #endregion
}
