using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject fader;
    public float fadeSpeed;

    private bool isFading = false;

    private void Update()
    {
        if(isFading)
            fader.transform.Translate(Vector3.right * fadeSpeed);
    }
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
        isFading = true;
        yield return new WaitUntil(() => fader.transform.position.x >= 80);
        SceneManager.LoadScene(sceneName);
    }

    public void exit(int exitCode = 0)
    {
        Debug.Log("Exited the application with exit code " + exitCode);
        Application.Quit(exitCode);
    } 
}
