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

    public AudioSource PlaySound(AudioClip clip)
    {
        var source = InstantiateSource();
        source.clip = clip;
        source.Play();
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
