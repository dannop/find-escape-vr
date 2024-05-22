using System;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{

    public event Action OnBreak;

    [SerializeField, Range(0.2f, 10f)] float minimumVelocityToBreak = 1f;

    bool isBroken = false;

    Rigidbody body;

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

    void Break()
    {
        isBroken = true;
        OnBreak?.Invoke();
    }

}
