using Photon.Pun;
using System;
using UnityEngine;

public class BreakableObject : MonoBehaviourPunCallbacks
{

    public bool CanBreak {  get => canBreak; set => canBreak = value; }
    public event Action<BreakableObject> OnBreak;
    public event Action<BreakableObject> OnRestore;

    [SerializeField, Range(0.01f, 1f)] float minimumVelocityToBreak = 1f;
    [SerializeField] GameObject brokenPrefab;
    [SerializeField] bool canBreak;

    [SerializeField] AudioClip brokenSound;

    GameObject instantiatedBrokenPrefab;

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
            if (CanBreak)
            {
                Break();
            }
        }

    }

    [ContextMenu("Break object!")]
    public void Break()
    {
        myView.RPC("DoBreak", RpcTarget.AllBuffered);
    }

    public void Restore()
    {
        //myView.RPC("DoRestore", RpcTarget.AllBuffered);
        DoRestore();
    }

    void DoRestore()
    {
        isBroken = false;
        meshRenderer.enabled = true;
        body.isKinematic = false;
        body.velocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;
        transform.SetPositionAndRotation(initialPosition, initialRotation);
        instantiatedBrokenPrefab.SetActive(false);
        OnRestore?.Invoke(this);
    }

    [PunRPC]
    void DoBreak()
    {
        if (isBroken)
        {
            return;
        }

        if(instantiatedBrokenPrefab == null)
        {
            instantiatedBrokenPrefab = Instantiate(brokenPrefab);
        }
        instantiatedBrokenPrefab.SetActive(true);
        instantiatedBrokenPrefab.transform.SetPositionAndRotation(transform.position, transform.rotation);

        if(brokenSound != null)
        {
            AudioManager.Instance.PlaySound(brokenSound, true);
        }

        isBroken = true;
        meshRenderer.enabled = false;
        body.isKinematic = true;
        OnBreak?.Invoke(this);
        Debug.Log("Broke object!");
    }
}
