using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MovementKiller : MonoBehaviour
{
    
    public void KillMovement()
    {
        foreach (var move in FindObjectsOfType<ContinuousMoveProviderBase>())
        {
            move.moveSpeed = 0;
        };
    }

}
