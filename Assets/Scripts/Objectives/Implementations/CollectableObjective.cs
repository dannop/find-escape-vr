using System;
using System.Runtime.CompilerServices;

public class CollectableObjective: Objective
{

    public event Action OnCollected;

    //TODO - Logic to call this method
    public override void CompleteObjective()
    {
        isCompleted = true;
        OnCollected?.Invoke();
    }


}
