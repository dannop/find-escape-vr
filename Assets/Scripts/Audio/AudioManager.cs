using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] AudioSource sourcePrefab;

    public static AudioManager Instance
    {
        get
        {
            if(instance != null)
                return instance;

            instance = FindObjectOfType<AudioManager>();

            return instance;
        }
    }

    private static AudioManager instance;

    public float DefaultVolume = 0.3f;

    public AudioSource PlaySound(AudioClip clip, bool randomizePitch = false)
    {
        var source = InstantiateSource();
        source.clip = clip;
        source.Play();
        source.volume = DefaultVolume;
        
        if(randomizePitch)
        {
            source.pitch = Random.Range(0.9f, 1.3f);
        }
        StartCoroutine(SourceLifetimeRoutine(source));
       
        return source;
    }

    AudioSource InstantiateSource()
    {
        var newSource = Instantiate<AudioSource>(sourcePrefab);
        newSource.transform.position = Vector3.zero;
        return newSource;
    }

    IEnumerator SourceLifetimeRoutine(AudioSource source)
    {
        yield return new WaitForSeconds(source.clip.length);
        Destroy(source.gameObject);
    }

}
