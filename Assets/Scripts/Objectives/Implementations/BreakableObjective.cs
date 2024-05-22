using UnityEngine;

[RequireComponent (typeof(Objective), typeof(BreakableObject))]
public class BreakableObjective : MonoBehaviour
{

    Objective objective;
    BreakableObject breakable;

    private void Awake()
    {
        objective = GetComponent<Objective>();
        breakable = GetComponent<BreakableObject>();

        breakable.OnBreak += Breakable_OnBreak;
    }

    void Breakable_OnBreak()
    {
        objective.CompleteObjective();
    }

}
