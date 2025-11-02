using Oculus.Platform;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] music;
    public AudioSource AS;
    public bool Phase2 = true;
    public bool Phase1 = true;
    // Start is called before the first frame update
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
        if (Phase1)
        {
            playphase1();
        }
       
        
    }

    public void playphase1()
    {
        Phase1 = false;
        AS.clip = music[0];
        AS.Play();
    }

    public void playphase2()
    {
        if (!Phase1 && Phase2)
        {
            Phase2 = false;
            AS.clip = music[1];
            AS.Play();
        }
    }

    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }


}
