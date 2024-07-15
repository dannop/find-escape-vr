using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressBtnsToComplete : Objective
{

    public int AmountOfBtns => btnsToPress.Count;
    public int AmountOfSelectedBtns => amountOfSelectedBtns;
    public bool AllBtnSelected => allBtnSelected;
    public UnityEvent OnComplete;


    [SerializeField]
    List<CandleBtnController> btnsToPress = new List<CandleBtnController>();

    bool allBtnSelected = false;
    int amountOfSelectedBtns = 0;
    PhotonView myView;

    private void Awake()
    {
        myView = GetComponent<PhotonView>();
    }

    private void OnEnable()
    {
        foreach (var item in btnsToPress)
        {
            item.OnSelected += Btn_OnSelectStart;
            item.OnDeselected += Btn_OnSelectExit;
        }
    }

    private void OnDisable()
    {
        foreach (var item in btnsToPress)
        {
            item.OnSelected -= Btn_OnSelectStart;
            item.OnDeselected -= Btn_OnSelectExit;
        }
    }

    void Btn_OnSelectStart()
    {

        amountOfSelectedBtns++;

        if(amountOfSelectedBtns == AmountOfBtns)
        {
            allBtnSelected = true;
            myView.RPC("DoAllBtnSelectedRoutine", RpcTarget.AllBuffered);
        }

    }

    void Btn_OnSelectExit()
    {
        amountOfSelectedBtns--;
    }

    [PunRPC]
    void DoAllBtnSelectedRoutine()
    {

        if (!this.isCompleted)
        {
            OnComplete?.Invoke();
            base.CompleteObjective();
        }

    }

}
