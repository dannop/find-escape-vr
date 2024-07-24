using UnityEngine;
using UnityEngine.Events;

public class DoorController : MonoBehaviour
{

    public bool CanEndGame
    {
        get => canEndGame;
        set
        {
            canEndGame = value;
            onEndGameAllowed.Invoke();
        }
    }

    [SerializeField] UnityEvent onEndGame;
    [SerializeField] UnityEvent onEndGameAllowed;

    int amountOfPlayersInside = 0;
    bool canEndGame = false;

    private void OnTriggerEnter(Collider other)
    {

        amountOfPlayersInside++;
        if (amountOfPlayersInside == 2 && CanEndGame)
        {
            onEndGame.Invoke();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        amountOfPlayersInside--;
    }

}
