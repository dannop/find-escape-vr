using System;
using System.Collections.Generic;
using UnityEngine;

public class CrystalPotManager : MonoBehaviour
{

    [SerializeField] int amountOfCrystalsToOpenDoor = 4;
    [SerializeField] DoorController doorController;

    public Action OnCrystalThreasholdReached;
    int amountOfAlocatedCrystals = 0;



    public bool TryAllocateCristalOnThisPot(CrystalController cristal, CrystalPotController pot)
    {

        if((pot.allocatedCrystal == null) && (cristal.allocationPot == null))
        {
            pot.allocatedCrystal = cristal;
            cristal.allocationPot = pot;

            amountOfAlocatedCrystals++;
            if(amountOfAlocatedCrystals >= amountOfCrystalsToOpenDoor)
            {
                Debug.Log("TODO - OpenDoor");
                doorController.CanEndGame = true;
                OnCrystalThreasholdReached?.Invoke();
            }

            return true;
        }

        return false;
    }

}

