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

        // Убедитесь, что видео будет воспроизводиться в режиме одиночного воспроизведения
        //videoPlayer.isLooping = false;

        // Подписываемся на событие завершения воспроизведения видео
        videoPlayer.loopPointReached += OnVideoFinished;

        // Запускаем воспроизведение видео
        videoPlayer.Play();
    }

    // Метод, который вызывается, когда видео закончилось
    private static void OnVideoFinished(VideoPlayer vp)
    {
        // Загружаем новую сцену
        SceneManager.LoadScene(1); // Замените "ИмяВашейСцены" на имя вашей сцены
    }
}
