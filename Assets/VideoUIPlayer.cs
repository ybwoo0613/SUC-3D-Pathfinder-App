using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoUIPlayer : MonoBehaviour
{
    public GameObject videoButton;
    public GameObject videoPanel;
    public RawImage rawImage;
    public VideoPlayer videoPlayer;
    public Button closeButton;
    public string videoFileName;

    private bool isPlaying = false; // 新增：追踪播放状态

    void Start()
    {
        videoPanel.SetActive(false);
        closeButton.onClick.AddListener(CloseVideo);
    }

    public void PlayVideo()
    {
        if (isPlaying)
        {
            CloseVideo();
            return;
        }

        if (!videoFileName.EndsWith(".mp4"))
        {
            videoFileName += ".mp4";
        }

        string path = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
        Debug.Log("Video URL: " + path);
        videoPlayer.url = path;

        videoPanel.SetActive(true);
        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += OnVideoPrepared;

        isPlaying = true;
    }

    void OnVideoPrepared(VideoPlayer vp)
    {
        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();
    }

    public void CloseVideo()
    {
        videoPlayer.Stop();
        videoPanel.SetActive(false);
        isPlaying = false; 
    }
}
