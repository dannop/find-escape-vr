using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectivesManager : MonoBehaviour
{

    List<Objective> allSceneObjectives = new List<Objective>();
    

    private void Awake()
    {
        allSceneObjectives = FindObjectsOfType<Objective>(true).ToList();
        foreach (var objective in allSceneObjectives)
        {
            objective.OnCompleteObjective += Objective_OnCompleteObjective;
        }
    }

    void Objective_OnCompleteObjective()
    {
        Debug.Log("TODO - OnObjectiveComplete");
    }

}
