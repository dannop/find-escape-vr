using System;
using UnityEngine;

public class OnInteractRotate : MonoBehaviour, IInteractable
{

    public event Action onRotate;

    [SerializeField] float rotationAmount = 90f;
    [SerializeField] Transform objectToRotate;

    private void Awake()
    {
        if(objectToRotate == null) 
        {
            Debug.LogError("[OnInteractRotate] objectToRotate is null!");
            this.enabled = false;
            return;
        }
    }

    public void Interact()
    {

        objectToRotate.transform.Rotate(Vector3.up, rotationAmount, Space.Self);

    }

}
