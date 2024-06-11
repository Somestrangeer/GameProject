using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


public class Footstep : MonoBehaviour
{
    public AudioClip[] sounds;
    private AudioSource audioSrc;
    private float stepRate = 0.45f; // Время между шагами в секундах
    private float nextStepTime = 0f; // Время следующего шага
    private static Footstep instance;
    

    public static Footstep Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Footstep>();
            }
            return instance;
        }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    public void PlaySound(float volume = 0.1f)
    {
        if (audioSrc == null || sounds.Length == 0 || Time.time < nextStepTime)
        {
            return;
        }

        // Убрана проверка audioSrc.isPlaying для непрерывного воспроизведения звуков
        AudioClip clip = sounds[Random.Range(0, sounds.Length)];
        audioSrc.pitch = Random.Range(1.1f, 1.2f); // Значения pitch теперь зафиксированы
        audioSrc.PlayOneShot(clip, volume); // Используем PlayOneShot для воспроизведения звука

        nextStepTime = Time.time + stepRate; // Обновляем время следующего шага
    }
}
//public class Footstep : MonoBehaviour
//{
//    public AudioClip[] sounds;
//    public SoundArrays[] randSound;

//    private AudioSource audioSrc;

//    private static Footstep instance;
//    public static Footstep Instance
//    {
//        get
//        {
//            if (instance == null)
//            {
//                instance = FindObjectOfType<Footstep>();
//            }
//            return instance;
//        }
//    }
//    void Awake()
//    {
//        if (instance != null && instance != this)
//        {
//            Destroy(this.gameObject);
//        }
//        else
//        {
//            instance = this;
//        }


//    }

//    void Start()
//    {
//        audioSrc = GetComponent<AudioSource>();
//    }

//    public void PlaySound(int i, float volume = 1f, bool random = false, bool destroyed = false, float p1 = 1.1f, float p2 = 1.2f)
//    {
//        if (audioSrc == null)
//        {
//            return;
//        }

//        if (!audioSrc.isPlaying) 
//        {
//            AudioClip clip = random ? randSound[i].soundArray[Random.Range(0, randSound[i].soundArray.Length)] : sounds[i];
//            audioSrc.pitch = Random.Range(p1, p2);

//            if (destroyed)
//                AudioSource.PlayClipAtPoint(clip, transform.position, volume);
//            else
//                audioSrc.PlayOneShot(clip, volume);
//        }
//    }

//    [System.Serializable]
//    public class SoundArrays
//    {
//        public AudioClip[] soundArray;
//    }
//}