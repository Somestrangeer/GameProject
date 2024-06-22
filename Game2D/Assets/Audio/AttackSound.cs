using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSound : MonoBehaviour
{
    public AudioClip[] sounds;
    private AudioSource audioSrc;
    private float stepRate = 0.45f; // ����� ����� ������ � ��������
    private float nextStepTime = 0f; // ����� ���������� ����
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

        // ������ �������� audioSrc.isPlaying ��� ������������ ��������������� ������
        AudioClip clip = sounds[Random.Range(0, sounds.Length)];
        audioSrc.pitch = Random.Range(1.1f, 1.2f); // �������� pitch ������ �������������
        audioSrc.PlayOneShot(clip, volume); // ���������� PlayOneShot ��� ��������������� �����

        nextStepTime = Time.time + stepRate; // ��������� ����� ���������� ����
    }

    public static void PlaySoundUnit()
    {
        if (instance != null)
        {
            instance.PlaySoundAttack(); // ����� �������������� ������
        }
    }
}
