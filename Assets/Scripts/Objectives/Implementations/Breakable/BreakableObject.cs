using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{

    public event Action<BreakableObject> OnBreak;

    [SerializeField, Range(0.2f, 10f)] float minimumVelocityToBreak = 1f;

    bool isBroken = false;
    Vector3 initialPosition;
    Quaternion initialRotation;

    Rigidbody body;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
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
        if (isBroken)
        {
            return;
        }

        isBroken = true;
        OnBreak?.Invoke(this);
    }

    public void Restore()
    {
        isBroken = false;
        transform.SetPositionAndRotation(initialPosition, initialRotation);
    }

}
