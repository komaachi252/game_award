using UnityEngine.Audio;
using System;
using UnityEngine;

public class Audio_Manager : MonoBehaviour
{
    public Sound[] sounds;

    public static Audio_Manager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.pitch = s.pitch;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
     
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log(name + "はありません");
            return;
        }
        s.source.Play();
        Debug.Log(name + "は再生中");
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log(name + "はありません");
            return;
        }
        s.source.Stop();

    }

    public void Set_Volume(bool bgm, float volume)
    {
        //  loopフラグでBGMを判定
        foreach(var sound in sounds)
        {
            if (sound.source.loop == bgm)
            {
                sound.source.volume = volume;
            }
        }
    }
}
