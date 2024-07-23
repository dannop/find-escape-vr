using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BreakSelectedObject : MonoBehaviour
{

    [SerializeField] BreakableObject selectedObject;

    Button btn = null;

    private void Awake()
    {
        btn = GetComponent<Button>();    
    }
    

}
