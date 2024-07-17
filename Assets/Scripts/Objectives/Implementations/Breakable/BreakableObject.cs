using Photon.Pun;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BreakableObject : MonoBehaviourPunCallbacks
{

    public event Action<BreakableObject> OnBreak;

    [SerializeField, Range(0.2f, 10f)] float minimumVelocityToBreak = 1f;

    bool isBroken = false;
    Vector3 initialPosition;
    Quaternion initialRotation;

    PhotonView myView;
    Rigidbody body;
    MeshRenderer meshRenderer;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        myView = GetComponent<PhotonView>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (body.isKinematic || isBroken)
        {
            return;
        }

        if(body.velocity.magnitude > minimumVelocityToBreak)
        {
            Break();
        }

    }

    [ContextMenu("Break object!")]
    public void Break()
    {
        myView.RPC("DoBreak", RpcTarget.AllBuffered);
    }

    public void Restore()
    {
        myView.RPC("DoRestore", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void DoRestore()
    {
        isBroken = false;
        meshRenderer.enabled = true;
        transform.SetPositionAndRotation(initialPosition, initialRotation);
    }

    [PunRPC]
    void DoBreak()
    {
        if (isBroken)
        {
            return;
        }

        isBroken = true;
        meshRenderer.enabled = false;
        OnBreak?.Invoke(this);
        Debug.Log("Broke object!");
    }
}
