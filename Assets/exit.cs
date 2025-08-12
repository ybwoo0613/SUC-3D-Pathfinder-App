using UnityEngine;
using UnityEngine.SceneManagement;

public class exit : MonoBehaviour
{

    public void OnCloseButtonClicked()
    {
        Debug.Log("Exit Button Clicked");
        Application.Quit();

#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

}
