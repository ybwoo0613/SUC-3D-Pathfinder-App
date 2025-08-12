using UnityEngine;

public class AudioDescription : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;

    public void PlayAudio()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        else if (audioClip != null)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }


    public void StopAudio()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
