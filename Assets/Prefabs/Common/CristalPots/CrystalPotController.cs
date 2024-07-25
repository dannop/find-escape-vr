using UnityEngine;

public class CrystalPotController : CollectableObjective
{

    public CrystalPotManager Manager;
    public CrystalController allocatedCrystal;

    [SerializeField] Transform positionToAllocate;

    private void OnTriggerEnter(Collider other)
    {

        if(other.TryGetComponent(out CrystalController crystal) && crystal.allocationPot == null)
        {
            
            if(Manager.TryAllocateCristalOnThisPot(crystal, this))
            {
                crystal.transform.position = positionToAllocate.transform.position;
                crystal.transform.rotation = Quaternion.Euler(Vector3.up);
                crystal.Allocate();
            };
        }

    }

}

