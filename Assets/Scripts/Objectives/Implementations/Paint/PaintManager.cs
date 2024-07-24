using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PaintManager : MonoBehaviourPunCallbacks
{

    [SerializeField] List<PaintController> InsidePaints = new List<PaintController>();
    [SerializeField] List<PaintController> OutsidePaints = new List<PaintController>();

    [SerializeField] GameObject objectToActive;

    [SerializeField] UnityEvent onFinishPuzzle;

    bool isComplete = false;
    PhotonView myView;

    private void Awake()
    {
        foreach (var paints in InsidePaints)
        {
            paints.PaintManager = this;
        }

        foreach (var paints in OutsidePaints)
        {
            paints.PaintManager = this;
        }

        myView = GetComponent<PhotonView>();
    }

    public override void OnEnable()
    {
        base.OnEnable();

        foreach (var paints in InsidePaints)
        {
            paints.OnColorChange += Paint_OnColorChange;
        }
    }

    public override void OnDisable()
    {
        base.OnDisable();

        foreach (var paints in InsidePaints)
        {
            paints.OnColorChange -= Paint_OnColorChange;
        }
    }

    private IEnumerator Start()
    {
        yield return null;
        RandomizeOutsidePaintings();
    }

    public void RandomizeOutsidePaintings()
    {
        DoRandomizeOutsidePaintings();
    }

    public void SetOutsidePaintingsWhite()
    {
        DoSetOutsidePaintingsWhite();
    }

    void Paint_OnColorChange()
    {
        if (ArePaintColorsMatching())
        {
            myView.RPC("DoCompletedObjectiveRoutine", RpcTarget.AllBuffered);
        }
    }

    void DoSetOutsidePaintingsWhite()
    {
        OutsidePaints.ForEach(paint => paint.SetColor(ColorState.white));
    }

    void DoRandomizeOutsidePaintings()
    {
        var usedColors = new HashSet<ColorState>();

        int i = 0;
        while(usedColors.Count < 3)
        {
            Debug.Log("In while: " + i);
            ColorState randomColor = (ColorState)UnityEngine.Random.Range(0, 3);
            if (!usedColors.Contains(randomColor)){
                OutsidePaints[i].SetColor(randomColor);
                usedColors.Add(randomColor);
                i++;
            }
        }

        if(ArePaintColorsMatching() && OutsidePaints[0].CanChangeState)
        {
            DoRandomizeOutsidePaintings();
        }
    }

    [PunRPC]
    void DoCompletedObjectiveRoutine()
    {

        if (isComplete)
            return;

        isComplete = true;
        InsidePaints.ForEach(paint => paint.CanChangeState = false);
        OutsidePaints.ForEach (paint => paint.CanChangeState = false);
        objectToActive.SetActive(true);
        Debug.Log("Completed paint objective");

    }

    bool ArePaintColorsMatching()
    {
        
        for(int i = 0; i < InsidePaints.Count; i++)
        {
            if (!(InsidePaints[i].CurrentState == OutsidePaints[i].CurrentState)){
                return false;
            }
        }

        return true;
    }

}


