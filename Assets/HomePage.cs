using UnityEngine;
using UnityEngine.SceneManagement;

public class HomePage : MonoBehaviour
{
    public void OnStartButtonClicked()
    {
        Debug.Log("Start Button Clicked");
        SceneManager.LoadScene("Imagetarget");
    }
}
