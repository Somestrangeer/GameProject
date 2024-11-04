using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{
    //private VideoPlayer videoPlayer;

    public static void StartThisShit(VideoPlayer videoPlayer)
    {
        //videoPlayer = GetComponent<VideoPlayer>();

        // ���������, ��� ����� ����� ���������������� � ������ ���������� ���������������
        //videoPlayer.isLooping = false;

        // ������������� �� ������� ���������� ��������������� �����
        videoPlayer.loopPointReached += OnVideoFinished;

        // ��������� ��������������� �����
        videoPlayer.Play();
    }

    // �����, ������� ����������, ����� ����� �����������
    private static void OnVideoFinished(VideoPlayer vp)
    {
        // ��������� ����� �����
        SceneManager.LoadScene(1); // �������� "�������������" �� ��� ����� �����
    }
}
