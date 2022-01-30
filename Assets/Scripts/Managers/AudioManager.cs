using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Prefab")]
    public GameObject audioPlayer;

    [Header("Lists")]
    private readonly List<AudioSource> musicList = new List<AudioSource>();
    private readonly List<AudioSource> sfxList = new List<AudioSource>();
    [Space]
    private int musicPlayingIndex = -1;
    private int musicFadeoutIndex = -1;
    private int musicFadeinIndex = -1;


    public static AudioManager audioManager;
    
    void Start()
    {
        audioManager = this;
        DontDestroyOnLoad(this.gameObject);

        for(int i = 0; i < 20; i++)
            sfxList.Add(Instantiate(audioPlayer, new Vector2(0, 0), Quaternion.identity, this.transform).GetComponent<AudioSource>());

        for(int i = 0; i < 2; i++)
        {
            musicList.Add(Instantiate(audioPlayer, new Vector2(0, 0), Quaternion.identity, this.transform).GetComponent<AudioSource>());
            musicList[i].loop = true;
        }

        UpdateVolume(1, 0.5f, 0.5f);
    }

    
    void Update()
    {
        if (musicFadeoutIndex != -1)
        {
            musicList[musicFadeoutIndex].volume = Mathf.MoveTowards(musicList[musicFadeoutIndex].volume, 0, 0.1f * Time.deltaTime);

            if (musicList[musicFadeoutIndex].volume == 0)
            {
                musicList[musicFadeoutIndex].Stop();
                musicFadeoutIndex = -1;
            }
        }

        if (musicFadeinIndex != -1)
        {
            musicList[musicFadeinIndex].volume = Mathf.MoveTowards(musicList[musicFadeinIndex].volume, 0, 0.1f * Time.deltaTime);

            if (musicList[musicFadeinIndex].volume == 0)
            {
                musicList[musicFadeinIndex].Stop();
                musicFadeinIndex = -1;
            }
        }
    }

    public void PlaySFX(AudioClip sound)
    {
        for(int i = 0; i < sfxList.Count; i++)
        {
            if(!sfxList[i].isPlaying)
            {
                sfxList[i].clip = sound;
                sfxList[i].Play();
                return;
            }
        }

        sfxList.Add(Instantiate(audioPlayer, new Vector2(0, 0), Quaternion.identity, this.transform).GetComponent<AudioSource>());
        sfxList[sfxList.Count - 1].clip = sound;
        sfxList[sfxList.Count - 1].Play();
    }

    public void PlayMusic(AudioClip music)
    {
        if(musicPlayingIndex != -1)
        {
            if (musicList[musicPlayingIndex].clip != music)
            {
                musicFadeoutIndex = musicPlayingIndex;
            }
            else
                return;
        }

        musicPlayingIndex = (musicPlayingIndex + 1) % 2;
        musicList[musicPlayingIndex].clip = music;
        musicList[musicPlayingIndex].Play();
        musicFadeinIndex = musicPlayingIndex;
    }

    public void UpdateVolume(float master, float music, float sfx)
    {
        foreach (AudioSource source in sfxList)
        {
            source.volume = sfx * master;
        }

        foreach(AudioSource source in musicList)
        {
            source.volume = music * master;
        }
    }
}
