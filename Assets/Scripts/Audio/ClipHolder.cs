using UnityEngine;

public class ClipHolder : MonoBehaviour
{

    [SerializeField] bool playOnWake = false;
    [SerializeField] AudioClip clip;

    AudioSource mysource;

    private void Awake()
    {
        if (playOnWake)
        {
            if(mysource != null)
            {
                mysource.Stop();
            }

            PlayClip();
        }
    }

    public void PlayClip()
    {
        mysource = AudioManager.Instance.PlaySound(clip);
    }

}
