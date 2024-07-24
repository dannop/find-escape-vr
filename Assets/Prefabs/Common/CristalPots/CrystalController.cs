using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class CrystalController : MonoBehaviour
{

    public CrystalPotController allocationPot;
    public UnityEvent OnAllocatedOnPot;

    public void Allocate()
    {
        var collider = GetComponent<BoxCollider>();
        Destroy(GetComponent<XRGrabInteractable>());
        Destroy(GetComponent<Rigidbody>());
        Destroy(GetComponent<NetworkedGrabing>());
        collider.enabled = false;
        OnAllocatedOnPot.Invoke();
    }

}

