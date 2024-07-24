using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent (typeof(XRSimpleInteractable))]
public class XRInteractableUtils : MonoBehaviour
{

    XRSimpleInteractable interactable;

    private void Awake()
    {
        interactable = GetComponent<XRSimpleInteractable>();
    }

    public void Select()
    {
        interactable.selectEntered.Invoke(null);
    }

    public void Deselect()
    {
        interactable.selectExited.Invoke(null);
    }

}
