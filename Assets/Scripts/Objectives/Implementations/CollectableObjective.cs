using Photon.Pun;
using System;

public class CollectableObjective: Objective
{

    public event Action OnCollected;
    PhotonView m_photonView;

    private void Awake()
    {
        m_photonView = GetComponent<PhotonView>();
    }

    //TODO - Logic to call this method
    public override void CompleteObjective()
    {
        m_photonView.RPC("DoCompleteObjective", RpcTarget.All);
    }

    void DoCompleteObjective()
    {

        if (!isCompleted)
        {
            isCompleted = true;
            OnCollected?.Invoke();
        }

    }


}
