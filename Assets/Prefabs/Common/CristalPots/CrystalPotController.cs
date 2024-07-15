using System.Collections.Generic;
using UnityEngine;

public class CrystalPotController : MonoBehaviour
{

    public CrystalPotManager Manager { get; set; }

    [SerializeField] Transform positionToAllocate;

    private void OnTriggerEnter(Collider other)
    {
        CrystalController crystal = other.GetComponent<CrystalController>();

        
    }

}

public class CrystalController : MonoBehaviour
{

}

public class CrystalPotManager : MonoBehaviour
{

    Dictionary<CrystalController, CrystalPotController> crystalToPotDic = new Dictionary<CrystalController, CrystalPotController>();

    public bool CanAllocateCristal(CrystalController cristal)
    {
        if (crystalToPotDic.ContainsKey(cristal))
        {
            return false;
        }

        return true;
    }

    public void AllocateCristalOnThisPot(CrystalController cristal, CrystalPotController pot)
    {

    }

}

