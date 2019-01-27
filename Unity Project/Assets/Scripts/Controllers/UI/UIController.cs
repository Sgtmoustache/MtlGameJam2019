using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject fader;
    public float fadeSpeed;
    public AudioClip buttonPress;

    private bool isFading = false;

    private void Update()
    {
        if(isFading)
            fader.transform.Translate(Vector3.right * fadeSpeed);
    }
    public void changeScene(string scenename)
    {
        if(fader != null)
            //SceneManager.LoadScene(scenename);
            StartCoroutine(Fade(scenename, fader));
        else
            SceneManager.LoadScene(scenename);
    }

    public void changeScene(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }

    private IEnumerator Fade(string sceneName, GameObject fadeOut)
    {
        isFading = true;
        if(buttonPress != null)
            GetComponent<AudioSource>().PlayOneShot(buttonPress);
        yield return new WaitUntil(() => fader.transform.position.x >= 400);
        SceneManager.LoadScene(sceneName);
    }

    public void exit(int exitCode = 0)
    {
        GetComponent<AudioSource>().PlayOneShot(buttonPress);
        Debug.Log("Exited the application with exit code " + exitCode);
        Application.Quit(exitCode);
    } 
}
