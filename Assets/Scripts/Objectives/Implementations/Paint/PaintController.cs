using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class PaintController : MonoBehaviourPunCallbacks
{

    public enum ColorState
    {
        blue,
        green,
        yellow
    }

    public PaintManager PaintManager { get; set; }
    public ColorState CurrentState => currentState;
    public bool CanChangeState { get; set; } = true;
    public event Action OnColorChange;

    [SerializeField] MeshRenderer myRenderer;
    [SerializeField] List<Material> colorMaterials;

    [SerializeField] ColorState currentState = ColorState.blue;

    private void Awake()
    {
        myRenderer ??= GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        SetColor(currentState);
    }

    [ContextMenu("Set color")]
    public void SetColor(ColorState color)
    {
        if (!CanChangeState)
            return;

        DoSetColor(color);
    }

    private void DoSetColor(ColorState color)
    {
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
        }

        currentState = color;
        OnColorChange?.Invoke();
    }
}


