using System;
using System.Collections.Generic;
using UnityEngine;

public class CrystalPotManager : MonoBehaviour
{

    [SerializeField] int amountOfCrystalsToOpenDoor = 4;
    [SerializeField] DoorController doorController;

    public Action OnCrystalThreasholdReached;
    Dictionary<CrystalController, CrystalPotController> crystalToPotDic = new Dictionary<CrystalController, CrystalPotController>();
    int amountOfAlocatedCrystals = 0;



    public bool CanAllocateCristal(CrystalController cristal)
    {
        if (crystalToPotDic.ContainsKey(cristal))
        {
            return false;
        }

        return true;
    }

    public bool TryAllocateCristalOnThisPot(CrystalController cristal, CrystalPotController pot)
    {

        if(!crystalToPotDic.ContainsKey(cristal))
        {
            crystalToPotDic[cristal] = pot;
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

