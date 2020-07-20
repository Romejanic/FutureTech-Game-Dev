using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionScreen : MonoBehaviour
{

    public void OpenScene(int buildIdx)
    {
        Debug.Log("Open Scene (" + buildIdx + ")");
        SceneManager.LoadScene(buildIdx);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

}