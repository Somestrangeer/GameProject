using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSound : MonoBehaviour
{
    public AudioClip[] sounds;
    private AudioSource audioSrc;
    private float stepRate = 0.45f; // Время между шагами в секундах
    private float nextStepTime = 0f; // Время следующего шага
    private static AttackSound instance;


    public static AttackSound Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AttackSound>();
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

    public void PlaySoundAttack(float volume = 0.3f)
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

    public static void PlaySoundUnit()
    {
        if (instance != null)
        {
            instance.PlaySoundAttack(); // Вызов нестатического метода
        }
    }
}
