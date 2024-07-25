using UnityEngine;

public class ClipHolder : MonoBehaviour
{

    [SerializeField] bool playOnWake = false;
    [SerializeField] bool randomizePitch = false;
    [SerializeField] bool loop = false;
    [SerializeField] bool useDefaultVolume = true;
    [SerializeField] float volume = 1.0f;
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
        if (useDefaultVolume)
        {
            mysource = AudioManager.Instance.PlaySound(clip, randomizePitch:randomizePitch);
        }
        else
        {
            mysource = AudioManager.Instance.PlaySound(clip, volume, randomizePitch);

        }

        if (loop)
        {
            mysource.loop = true;
        }
    }

}
