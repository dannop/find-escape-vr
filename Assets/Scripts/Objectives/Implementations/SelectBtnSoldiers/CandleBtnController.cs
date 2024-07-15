using Photon.Pun;
using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CandleBtnController : MonoBehaviour
{

    public bool IsSelected => xrInteractable.isSelected;
    public Action OnSelected;
    public Action OnDeselected;
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
        myView.RPC("DoOnSelectEnter", RpcTarget.AllBuffered);
    }

    void XRInteractable_OnSelectExit(SelectExitEventArgs args)
    {
        myView.RPC("DoOnSelectExit", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void DoOnSelectEnter()
    {
        animator.SetBool("Selected", true);
        OnSelected?.Invoke();
    }

    [PunRPC]
    void DoOnSelectExit()
    {
        animator.SetBool("Selected", false);
        OnDeselected?.Invoke();
    }

    

}
