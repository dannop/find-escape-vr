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
            if(value == true)
            {
                onEndGameAllowed.Invoke();
            }
        }
    }


    [SerializeField] float distanceToDoor = 2f;

    [SerializeField] UnityEvent onEndGame;
    [SerializeField] UnityEvent onEndGameAllowed;

    bool canEndGame = false;
    bool hasEndedGame = false;

    int amountOfPressedBtns = 0;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, distanceToDoor);
    }

    public void OnBtnPressed()
    {
        amountOfPressedBtns++;

        if(amountOfPressedBtns >= 2)
        {
            hasEndedGame = true;
            onEndGame?.Invoke();
        }
    }

    public void OnBtnUnpressed()
    {
        amountOfPressedBtns--;
    }

    private bool AllPlayersInRange()
    {

        var players = FindObjectsOfType<PlayerNetworkSetup>();
        
        foreach (var player in players)
        {

            var targetTransform = player.transform.GetChild(2);
            if (!(Vector3.Distance(targetTransform.position, player.transform.position) < distanceToDoor))
            {
                return false;
            }
        }

        return true;
    }

}
