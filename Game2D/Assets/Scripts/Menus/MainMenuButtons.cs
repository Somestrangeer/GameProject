using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MainMenuButtons : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    public void StartButton()
    {
        SaveData data = SaveSystem.LoadStatic();

        videoPlayer = GetComponent<VideoPlayer>();
        if (data != null)
        {
            if (data.sceneName.Length > 3)
            {
                SceneManager.LoadScene(data.sceneName);
            }
            else
            {
                GameObject h = GameObject.FindWithTag("DisableThis");
                h.SetActive(false);
                // ������������� �� ������� ���������� ��������������� �����
                videoPlayer.loopPointReached += OnVideoFinished;

                // ��������� ��������������� �����
                videoPlayer.Play();

                //SceneManager.LoadScene(1);
            }
        }
        else
        {
            GameObject h = GameObject.FindWithTag("DisableThis");
            h.SetActive(false);
            // ������������� �� ������� ���������� ��������������� �����
            videoPlayer.loopPointReached += OnVideoFinished;

            // ��������� ��������������� �����
            videoPlayer.Play();

           // SceneManager.LoadScene(1);
        }
    }

    public void OptionsButton()
    {
        SceneManager.LoadScene(2);
    }
    private static void OnVideoFinished(VideoPlayer vp)
    {
        // ��������� ����� �����
        SceneManager.LoadScene(1); // �������� "�������������" �� ��� ����� �����
    }

    public void ExitButton()
    {
        Application.Quit(); //�������� ������ ����� ���������� (�� ������� ���� ��� ���� ������� � ������)
    }
}
