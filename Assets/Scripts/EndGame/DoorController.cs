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
    [SerializeField] int playersNeededOnDoor = 2;

    [SerializeField] UnityEvent onEndGame;
    [SerializeField] UnityEvent onEndGameAllowed;

    int amountOfPlayersInside = 0;
    bool canEndGame = false;

    private void OnTriggerEnter(Collider other)
    {

        amountOfPlayersInside++;
        if (amountOfPlayersInside == playersNeededOnDoor && CanEndGame)
        {
            onEndGame.Invoke();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        amountOfPlayersInside--;
    }

}
