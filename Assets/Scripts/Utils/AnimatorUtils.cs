using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorUtils : MonoBehaviour
{

    public string targetParameter;

    Animator myAnimator;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    public void SetTargetParameter(string parameter)
    {
        targetParameter = parameter;
    }

    public void SetBool(bool value)
    {
        myAnimator.SetBool(targetParameter, value);
    }

    public void SetFloat(float value)
    {
        myAnimator.SetFloat(targetParameter, value);
    }

}
