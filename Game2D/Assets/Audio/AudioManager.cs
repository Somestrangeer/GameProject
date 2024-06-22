using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public static AudioManager instance;

	public Sound[] sounds;

    private bool isForestScene = false;
    private WaitForSeconds initialDelay = new WaitForSeconds(5f);
    private WaitForSeconds owlSoundInterval = new WaitForSeconds(15f);

    void Awake ()
	{
		if (instance != null)
		{
			Destroy(gameObject);
			return;
		} else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}

        if (SceneManager.GetActiveScene().name == "ForestScene")
        {
            isForestScene = true;
            StartCoroutine(PlayOwlSoundAtInterval());
        }
    }

    IEnumerator PlayOwlSoundAtInterval()
    {
        yield return initialDelay;
        while (isForestScene)
        {
            Play("owl");
            yield return owlSoundInterval;
        }
    }
    public void Play(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void Stop(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }
    public bool isPlaying(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + sound + " not found!");
            return false;
        }
        return s.source.isPlaying;
    }

    //   public void Play(string sound)
    //{
    //	Sound s = Array.Find(sounds, item => item.name == sound);
    //	s.source.Play();
    //}
    //public void Stop(string sound)
    //{
    //	Sound s = Array.Find(sounds, item => item.name == sound);
    //	s.source.Stop();
    //}

    // Добавленные методы для управления музыкой боя и обычной музыкой
    public void PlayBattleMusic()
    {
        Stop("Background");
        Play("battleMusic");
        Debug.Log("Playing Battle Music"); // Добавление сообщения в консоль, когда проигрывается музыка боя

    }

    public void PlayNormalMusic()
    {
        Stop("battleMusic");
        Play("Background");
        Debug.Log("Playing Normal Music"); // Добавление сообщения в консоль, когда проигрывается обычная музыка

    }
}
