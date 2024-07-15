using UnityEngine;

public class RotatingSoldiers : MonoBehaviour
{

    public int CurrentPlayerIndexInNetwork => 0;

    [SerializeField] OnInteractRotate soldierOneRotation;
    [SerializeField] OnInteractRotate soldierTwoRotation;

    [SerializeField] float correctRotation = 0f;
    [SerializeField] float aproximationThreashhold = 3f;

    OnInteractRotate usedRotation;

    private void Awake()
    {
        if(soldierOneRotation == null || soldierTwoRotation == null) 
        {
            Debug.LogError("[RotatingSoldiers] Soldiers reference missing!");
            this.enabled = false;
            return;
        }
    }

    private void Start()
    {
        
        if(CurrentPlayerIndexInNetwork == 0)
        {
            soldierTwoRotation.enabled = false;
            usedRotation = soldierOneRotation;
        }
        else if(CurrentPlayerIndexInNetwork == 1)
        {
            soldierOneRotation.enabled = false;
            usedRotation = soldierTwoRotation;
        }

        usedRotation.onRotate += OnInteractRotate_OnRotate;

    }

    void OnInteractRotate_OnRotate()
    {

        if (AreApproximately(usedRotation.transform.rotation.eulerAngles.y, correctRotation))
        {
            usedRotation.enabled = false;
        }

    }

    bool AreApproximately(float num1, float num2, float threashhold = 1f)
    {
        return Mathf.Abs(num1 - num2) < threashhold;
    }

}
