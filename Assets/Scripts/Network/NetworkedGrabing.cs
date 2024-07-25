using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkedGrabing : MonoBehaviour, IPunOwnershipCallbacks
{
    public bool IsBeignHeld { get => isBeignHeld; set => isBeignHeld = value; }

    PhotonView m_photonView;
    Rigidbody rb;

    bool isBeignHeld = false;
    int oldLayer;


    private void Awake()
    {
        m_photonView = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        oldLayer = gameObject.layer;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsBeignHeld)
        {
            rb.isKinematic = true;
            gameObject.layer = 11;
        }
        else
        {
            rb.isKinematic = false;
            gameObject.layer = oldLayer;
        }
    }

    private void TransferOwnership()
    {
        m_photonView.RequestOwnership();
    }

    public void OnSelectedEntered()
    {
        m_photonView.RPC("StartNetworkGrabbing", RpcTarget.AllBuffered);
        
        if (m_photonView.Owner == PhotonNetwork.LocalPlayer)
        {
            return;
        }
        TransferOwnership();
    }

    public void OnSelectedExisted()
    {
        m_photonView.RPC("StopNetworkGrabbing", RpcTarget.AllBuffered);
    }

    public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
    {
        if (targetView != m_photonView)
        {
            return;
        }
        m_photonView.TransferOwnership(requestingPlayer);
    }

    public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
    {
        
    }

    public void OnOwnershipTransferFailed(PhotonView targetView, Player senderOfFailedRequest)
    {
        
    }

    [PunRPC]
    public void StartNetworkGrabbing()
    {
        IsBeignHeld = true;
    }

    [PunRPC]
    public void StopNetworkGrabbing()
    {
        IsBeignHeld = false;
    }
}
