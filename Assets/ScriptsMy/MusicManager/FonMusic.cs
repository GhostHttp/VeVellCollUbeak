using System.Globalization;
using System.Collections;
using UnityEngine;

public class FonMusicManager : MonoBehaviour
{
    public AudioSource[] FonMusicEffect;
    private int _numberPlayMusic;

    public void Start()
    {
        FonMusicEffect[0].Play();
        _numberPlayMusic = 0;
    }

    public void MusicAdd(int numberMusic)
    {
        numberMusic -= 1;
        if (FonMusicEffect[numberMusic] = FonMusicEffect[_numberPlayMusic])
        {
            Debug.Log("Эта музыка уже включена!");
        }
        else
        {
            MusicAddAgresive(numberMusic);
        }
    }

    public void MusicAddAgresive(int numberMusic)
    {
        if(numberMusic+1 == _numberPlayMusic || numberMusic-1 == _numberPlayMusic)
        {
            FonMusicEffect[numberMusic].Play();

            _numberPlayMusic = numberMusic;
        }
        else
        {
            if(numberMusic == 0 && _numberPlayMusic == 2)
            {
                TimeCancelMusic(numberMusic, numberMusic - _numberPlayMusic);
            }
            else
            {
                Debug.Log("Ошибка в поясе превращения!");
            }
        }
    }

    public void TimeCancelMusic(int numberMusic, int numberR)
    {
        FonMusicEffect[numberMusic + numberR].Play();
        while (FonMusicEffect[numberMusic + numberR].isPlaying)
        {

        }
        FonMusicEffect[numberMusic].Play();
    }
}
