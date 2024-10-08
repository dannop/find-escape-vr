using Photon.Pun;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class CandleBtnController : MonoBehaviour
{

    public bool IsSelected => xrInteractable.isSelected;
    public UnityEvent OnSelected;
    public UnityEvent OnDeselected;
    public XRSimpleInteractable Interactable => xrInteractable;

    Animator animator;
    XRSimpleInteractable xrInteractable;
    PhotonView myView;

    private void Awake()
    {
        xrInteractable = GetComponent<XRSimpleInteractable>();
        animator = GetComponent<Animator>();
        myView = GetComponent<PhotonView>();
    }

    private void OnEnable()
    {
        xrInteractable.selectEntered.AddListener(XRInteractable_OnSelectEnter);
        xrInteractable.selectExited.AddListener(XRInteractable_OnSelectExit);
    }

    private void OnDisable()
    {
        xrInteractable.selectEntered.RemoveListener(XRInteractable_OnSelectEnter);
        xrInteractable.selectExited.RemoveListener(XRInteractable_OnSelectExit);
    }

    void XRInteractable_OnSelectEnter(SelectEnterEventArgs args)
    {
        OnSelectEnter();
    }

    void XRInteractable_OnSelectExit(SelectExitEventArgs args)
    {
        OnSelectExit();
    }

    [PunRPC]
    void DoOnSelectEnter()
    {
        animator?.SetBool("Selected", true);
        OnSelected?.Invoke();
    }

    [PunRPC]
    void DoOnSelectExit()
    {
        animator?.SetBool("Selected", false);
        OnDeselected?.Invoke();
    }

    [ContextMenu("Enter select!")]
    void OnSelectEnter()
    {
        myView.RPC("DoOnSelectEnter", RpcTarget.AllBuffered);
    }

    [ContextMenu("Exit select!")]
    void OnSelectExit()
    {
        myView.RPC("DoOnSelectExit", RpcTarget.AllBuffered);
    }

    

}
