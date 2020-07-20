using UnityEngine;
using UnityEngine.SceneManagement;

public class BackScreen : MonoBehaviour
{
    public int titleIndex = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            GoBack();
    }

    public void GoBack()
    {
        SceneManager.LoadScene(this.titleIndex);
    }

}
