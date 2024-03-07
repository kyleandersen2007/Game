using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public UnityEvent videoFinished;
    public GameObject screen;
    public VideoPlayer videoPlayer;

    void Start()
    {
        if (videoPlayer == null)
            videoPlayer = screen.GetComponent<VideoPlayer>();

        videoPlayer.loopPointReached += EndReached;
    }


    public void EndReached(VideoPlayer vp)
    {
        videoFinished.Invoke();
        videoPlayer.enabled = false;
        screen.SetActive(false);
    }
}
