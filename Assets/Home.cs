using UnityEngine;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour
{
    public void OnHomeButtonClicked()
    {
        SceneManager.LoadScene("Main menu"); // Replace "Home" with the actual home scene name
    }
}
