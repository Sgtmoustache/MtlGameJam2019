using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public void changeScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }

    public void changeScene(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }

    public void exit(int exitCode = 0)
    {
        Debug.Log("Exited the application with exit code " + exitCode);
        Application.Quit(exitCode);
    } 
}
