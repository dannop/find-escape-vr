using UnityEngine;
using UnityEngine.Events;

public class DoorController : MonoBehaviour
{

    public bool CanEndGame = false;

    [SerializeField] UnityEvent onEndGame;

    private void OnTriggerEnter(Collider other)
    {

        if (CanEndGame)
        {
            onEndGame.Invoke();
        }

    }

}
