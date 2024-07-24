using UnityEngine;

public class ClipHolder : MonoBehaviour
{
    [SerializeField] AudioClip clip;

    public void PlayClip()
    {
        AudioManager.Instance.PlaySound(clip);
    }

}
