using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MyAudioSource : MonoBehaviour
{
    private AudioSource audioSource;

    void Awake()
    {
        // Получаем компонент AudioSource, который уже должен быть присоединен к этому объекту
        audioSource = GetComponent<AudioSource>();
    }

    // Метод для воспроизведения звука
    public void PlaySound(AudioClip clip, float volume = 1f, bool loop = false)
    {
        // Проверяем, есть ли компонент AudioSource
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing!");
            return;
        }

        // Настройка параметров звука
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.loop = loop;

        // Воспроизводим звук
        audioSource.Play();
    }

    // Метод для остановки воспроизведения звука
    public void StopSound()
    {
        // Проверяем, есть ли компонент AudioSource
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing!");
            return;
        }

        // Останавливаем воспроизведение звука
        audioSource.Stop();
    }

    // Метод для проверки, играет ли звук в данный момент
    public bool IsPlaying()
    {
        // Проверяем, есть ли компонент AudioSource
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing!");
            return false;
        }

        // Возвращаем true, если звук играет, и false в противном случае
        return audioSource.isPlaying;
    }
}