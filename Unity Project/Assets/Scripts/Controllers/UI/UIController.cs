using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject fader;
    public float fadeSpeed;
    public void changeScene(string scenename)
    {
        //SceneManager.LoadScene(scenename);
        StartCoroutine(Fade(scenename, fader));
    }

    public void changeScene(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }

    private IEnumerator Fade(string sceneName, GameObject fadeOut)
    {
        while (true)
        {
            fader.transform.Translate(Vector3.right * fadeSpeed);
        }    }

    public void exit(int exitCode = 0)
    {
        Debug.Log("Exited the application with exit code " + exitCode);
        Application.Quit(exitCode);
    } 
}
