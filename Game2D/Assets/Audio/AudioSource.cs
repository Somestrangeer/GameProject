using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MyAudioSource : MonoBehaviour
{
    private AudioSource audioSource;

    void Awake()
    {
        // �������� ��������� AudioSource, ������� ��� ������ ���� ����������� � ����� �������
        audioSource = GetComponent<AudioSource>();
    }

    // ����� ��� ��������������� �����
    public void PlaySound(AudioClip clip, float volume = 1f, bool loop = false)
    {
        // ���������, ���� �� ��������� AudioSource
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing!");
            return;
        }

        // ��������� ���������� �����
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.loop = loop;

        // ������������� ����
        audioSource.Play();
    }

    // ����� ��� ��������� ��������������� �����
    public void StopSound()
    {
        // ���������, ���� �� ��������� AudioSource
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing!");
            return;
        }

        // ������������� ��������������� �����
        audioSource.Stop();
    }

    // ����� ��� ��������, ������ �� ���� � ������ ������
    public bool IsPlaying()
    {
        // ���������, ���� �� ��������� AudioSource
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing!");
            return false;
        }

        // ���������� true, ���� ���� ������, � false � ��������� ������
        return audioSource.isPlaying;
    }
}