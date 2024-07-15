﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(BreakableObject))]
public class BreakGroupObjective : Objective
{

    [SerializeField] float restoreTime = 1.5f;

    [SerializeField]
    List<BreakableObject> objectsToBreak = new List<BreakableObject>();

    int numberOfObjects = 0;
    int brokenObjects = 0;

    public UnityEvent OnBreakAll;

    PhotonView myView;

    private void Awake()
    {
        myView = PhotonView.Get(this);
    }

    private void Start()
    {
        numberOfObjects = objectsToBreak.Count;
        objectsToBreak.ForEach(obj => obj.OnBreak += Breakable_OnBreak);
    }

    void Breakable_OnBreak(BreakableObject breakable)
    {

        brokenObjects++;

        if(brokenObjects == numberOfObjects)
        {
            myView.RPC("DoCompleteObjective", RpcTarget.All);
        }

        StartCoroutine(RestoreRoutine(breakable));
    }

    [PunRPC]
    void DoCompleteObjective()
    {
        if (this.isCompleted)
        {
            return;
        }

        this.CompleteObjective();
        OnBreakAll?.Invoke();
    }

    IEnumerator RestoreRoutine(BreakableObject breakable)
    {
        yield return new WaitForSeconds(restoreTime);

        if (!this.isCompleted)
        {
            breakable.Restore();
        }

    }

}
