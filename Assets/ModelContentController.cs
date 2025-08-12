using UnityEngine;
using Vuforia;

public class ModelContentController : MonoBehaviour
{
    public string videoFileName;
    public AudioClip audioClip;

    private VideoUIPlayer videoUIPlayer;
    private AudioDescription audioDescription;

    void Start()
    {
        videoUIPlayer = FindObjectOfType<VideoUIPlayer>();
        audioDescription = FindObjectOfType<AudioDescription>();

        var observer = GetComponent<ObserverBehaviour>();
        if (observer)
        {
            observer.OnTargetStatusChanged += OnTargetStatusChanged;
        }
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        if (status.Status == Status.TRACKED || status.Status == Status.EXTENDED_TRACKED)
        {
          
            videoUIPlayer.videoFileName = videoFileName;

            if (audioDescription != null && audioClip != null)
            {
                audioDescription.audioClip = audioClip;
            }


            videoUIPlayer.videoButton.SetActive(true);
        }
        else
        {
            
            videoUIPlayer.videoButton.SetActive(false);
        }
    }
}
