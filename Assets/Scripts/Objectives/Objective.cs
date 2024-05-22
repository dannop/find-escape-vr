using System;
using UnityEngine;

public class Objective : MonoBehaviour
{

    public bool IsCompleted { get => isCompleted; set => isCompleted = value; }
    public event Action OnCompleteObjective;

    protected bool isCompleted;

    public virtual void CompleteObjective()
    {
        isCompleted = true;
        OnCompleteObjective?.Invoke();
    }
}