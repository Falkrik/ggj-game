using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Prefab")]
    public GameObject audioPlayerPrefab;
    [Space]
    public float fadeInSpeed = 0.3f;
    public float fadeOutSpeed = 0.3f;

    [Header("Lists")]
    private readonly List<AudioSource> musicList = new List<AudioSource>();
    private readonly List<AudioSource> sfxList = new List<AudioSource>();
    [Space]
    private int musicPlayingIndex = -1;
    private int musicFadeoutIndex = -1;
    private int musicFadeinIndex = -1;

    [Header("Saved Settings")]
    [HideInInspector] public float master = 1;
    [HideInInspector] public float music = 0.5f;
    [HideInInspector] public float sfx = 0.5f;

    public static AudioManager audioManager;
    
    void Awake()
    {
        if (AudioManager.audioManager != null)
        {
            Destroy(this.gameObject);
            return;
        }

        audioManager = this;
        DontDestroyOnLoad(this.gameObject);

        for(int i = 0; i < 20; i++)
            sfxList.Add(Instantiate(audioPlayerPrefab, new Vector2(0, 0), Quaternion.identity, this.transform).GetComponent<AudioSource>());

        for(int i = 0; i < 2; i++)
        {
            musicList.Add(Instantiate(audioPlayerPrefab, new Vector2(0, 0), Quaternion.identity, this.transform).GetComponent<AudioSource>());
            musicList[i].loop = true;
        }

        UpdateAllVolume();
    }

    
    void Update()
    {
        if (musicFadeoutIndex != -1)
        {
            musicList[musicFadeoutIndex].volume = Mathf.MoveTowards(musicList[musicFadeoutIndex].volume, 0, fadeOutSpeed * Time.deltaTime);

            if (musicList[musicFadeoutIndex].volume == 0)
            {
                musicList[musicFadeoutIndex].Stop();
                musicFadeoutIndex = -1;
            }
        }

        if (musicFadeinIndex != -1)
        {
            musicList[musicFadeinIndex].volume = Mathf.MoveTowards(musicList[musicFadeinIndex].volume, master * music, fadeInSpeed * Time.deltaTime);

            if (musicList[musicFadeinIndex].volume == master * music)
            {
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

        sfxList.Add(Instantiate(audioPlayerPrefab, new Vector2(0, 0), Quaternion.identity, this.transform).GetComponent<AudioSource>());
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

    private void UpdateAllVolume()
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

    public void UpdateVolume(SliderType type, float volume)
    {
        switch (type)
        {
            case SliderType.MASTER:
                master = volume;
                break;

            case SliderType.MUSIC:
                music = volume;
                break;

            case SliderType.SFX:
                sfx = volume;
                break;
        }

        UpdateAllVolume();
    }
}
