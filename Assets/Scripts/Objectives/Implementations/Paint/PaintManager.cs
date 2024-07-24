using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static PaintController;

public class PaintManager : MonoBehaviourPunCallbacks
{

    [SerializeField] List<PaintController> InsidePaints = new List<PaintController>();
    [SerializeField] List<PaintController> OutsidePaints = new List<PaintController>();

    [SerializeField] GameObject objectToActive;

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

    void Paint_OnColorChange()
    {
        if (ArePaintColorsMatching())
        {
            DoCompletedObjectiveRoutine();
        }
    }

    void DoRandomizeOutsidePaintings()
    {
        var usedColors = new HashSet<PaintController.ColorState>();

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

    void DoCompletedObjectiveRoutine()
    {

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


