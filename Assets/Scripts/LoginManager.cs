using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LoginManager : MonoBehaviourPunCallbacks
{
    public void ConnectAnonymously()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void ConnectToPhotonServer()
    {
        PhotonNetwork.LoadLevel("HomeScene");
    }

    public override void OnConnected()
    {
        Debug.Log("OnConnected called.");
        
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster called.");
        PhotonNetwork.LoadLevel("HomeScene");
    }
}
