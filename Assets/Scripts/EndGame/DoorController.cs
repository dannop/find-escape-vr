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

    private void Update()
    {

        if (!hasEndedGame && AllPlayersInRange())
        {
            hasEndedGame = true;
            onEndGame?.Invoke();
        }

        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, distanceToDoor);
    }

    private bool AllPlayersInRange()
    {

        var players = FindObjectsOfType<PlayerNetworkSetup>();
        foreach (var player in players)
        {
            if (!(Vector3.Distance(transform.position, player.transform.position) < distanceToDoor))
            {
                return false;
            }
        }

        return true;
    }

}
