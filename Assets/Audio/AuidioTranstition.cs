using System.Collections;
using UnityEngine;

public class AuidioTranstition : MonoBehaviour
{
    public AudioSource as1, as2;

    float defaultVolume = 0.2f;
    float transitionTime = 1.25f;
    bool Iss1IsPlaying = true;

    public void ChangeClip()
    {
        AudioSource nowPlaying = as1;
        AudioSource target = as2;
        if (Iss1IsPlaying == false)
        {
            nowPlaying = as2;
            target = as1;
        }
        Iss1IsPlaying = !Iss1IsPlaying;

        StopAllCoroutines();
        StartCoroutine(MixSources(nowPlaying,target));
    }

    private IEnumerator MixSources(AudioSource nowPalying, AudioSource target)
    {
        float percentage = 0;
        while (nowPalying.volume > 0)
        { 
            nowPalying.volume = Mathf.Lerp(defaultVolume,0,percentage);
            percentage += Time.deltaTime / transitionTime;
            yield return null;
        }

        nowPalying.Pause();
        if (target.isPlaying == false) target.Play();
        target.UnPause();
        percentage = 0;
        while( target.volume < defaultVolume)
        {
            target.volume = Mathf.Lerp(0,defaultVolume, percentage);
            percentage += Time.deltaTime / transitionTime;
            yield return null;
        }
    }
}
