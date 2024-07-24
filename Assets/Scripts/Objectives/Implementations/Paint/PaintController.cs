using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[SelectionBase]
public class PaintController : MonoBehaviourPunCallbacks
{
  

    public PaintManager PaintManager { get; set; }
    public ColorState CurrentState => currentState;
    public bool CanChangeState { get; set; } = true;
    public UnityEvent OnColorChange;

    [SerializeField] MeshRenderer myRenderer;
    [SerializeField] List<Material> colorMaterials;

    [SerializeField] ColorState currentState = ColorState.blue;


    PhotonView myView;

    private void Awake()
    {
        myRenderer ??= GetComponent<MeshRenderer>();
        myView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        SetColor(currentState);
    }


    [ContextMenu("Set color")]
    public void SetColor(ColorState color)
    {
        myView.RPC("DoSetColor", RpcTarget.AllBuffered, color);
    }

    public void SetColor(int color)
    {
        SetColor((ColorState) color);
    }

    public void SetNextColor()
    {
        SetColor(((int)currentState + 1) % 3);
    }

    [PunRPC]
    private void DoSetColor(ColorState color)
    {
        if (!CanChangeState)
            return;

        switch (color)
        {
            case ColorState.blue:
                myRenderer.material = colorMaterials[0];
                break;

            case ColorState.green:
                myRenderer.material = colorMaterials[1];
                break;

            case ColorState.yellow:
                myRenderer.material = colorMaterials[2];
                break;

            case ColorState.white:
                myRenderer.material = colorMaterials[3];
                break;
        }

        currentState = color;
        OnColorChange?.Invoke();
    }
}

