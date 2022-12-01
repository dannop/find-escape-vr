using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class VirtualWorldManager : MonoBehaviourPunCallbacks
{
    private string lastMapType;

    public static VirtualWorldManager Instance;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }

    public void GoToNewRoom()
    {
        ExitGames.Client.Photon.Hashtable expectedCustomProperties = new ExitGames.Client.Photon.Hashtable() { { MultiplayerVRConstants.MAP_TYPE_KEY, lastMapType } };
        PhotonNetwork.JoinRandomRoom(expectedCustomProperties, 0);
    }

    public void VerifyNewRoom()
    {
        if (PhotonNetwork.CurrentRoom == null)
        {
            GoToNewRoom();
        }
        else
        {
            PhotonNetwork.LeaveRoom();
        }
    }

    public void OnEnterHall()
    {
        lastMapType = MultiplayerVRConstants.MAP_TYPE_VALUE_HALL;
        VerifyNewRoom();
    }

    public void OnEnterThrone()
    {
        lastMapType = MultiplayerVRConstants.MAP_TYPE_VALUE_THRONE;
        VerifyNewRoom();
    }

    public void LeaveRoomAndLoadHomeScene()
    {
        lastMapType = MultiplayerVRConstants.MAP_TYPE_VALUE_HOME;
        PhotonNetwork.LeaveRoom();
    }

    #region Photon Callback Methods

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " joined to: " + "Player count: " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Aqui: " + lastMapType);
        if (lastMapType == MultiplayerVRConstants.MAP_TYPE_VALUE_HOME)
        {
            PhotonNetwork.Disconnect();
        }
        else
        {
            //GoToNewRoom();
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to servers again.");
        GoToNewRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Ali: " + lastMapType);
        if (lastMapType == MultiplayerVRConstants.MAP_TYPE_VALUE_SCHOOL)
        {
            PhotonNetwork.LoadLevel("World_School");
        }
        else if (lastMapType == MultiplayerVRConstants.MAP_TYPE_VALUE_OUTDOOR)
        {
            PhotonNetwork.LoadLevel("World_Outdoor");
        }
        else if (lastMapType == MultiplayerVRConstants.MAP_TYPE_VALUE_HALL)
        {
            PhotonNetwork.LoadLevel("HallScene");
        }
        else if (lastMapType == MultiplayerVRConstants.MAP_TYPE_VALUE_THRONE)
        {
            PhotonNetwork.LoadLevel("ThroneScene");
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Connect Failed. " + message);
        string randomRoomName = "Room_" + lastMapType + Random.Range(0, 10000);
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 20;

        string[] roomPropsInLobby = { MultiplayerVRConstants.MAP_TYPE_KEY };

        ExitGames.Client.Photon.Hashtable customRoomProperties = new ExitGames.Client.Photon.Hashtable() { { MultiplayerVRConstants.MAP_TYPE_KEY, lastMapType } };

        roomOptions.CustomRoomPropertiesForLobby = roomPropsInLobby;
        roomOptions.CustomRoomProperties = customRoomProperties;

        PhotonNetwork.CreateRoom(randomRoomName, roomOptions);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("A room is created: " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        PhotonNetwork.LoadLevel("HomeScene");
    }

    #endregion
}
