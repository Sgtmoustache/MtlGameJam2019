using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject fader;
    public float fadeSpeed;
    public AudioClip fadeSound;
    public AudioClip buttonPress;

    private bool isFading = false;

    private void Update()
    {
        if(isFading)
            fader.transform.Translate(Vector3.right * fadeSpeed);
    }
    public void changeScene(string scenename)
    {
        if (fader != null)
        {
            IEnumerator fadeSound1 = AudioFadeOut.FadeOut(GetComponent<AudioSource>(), 0.5f);
            StartCoroutine(fadeSound1);
            StartCoroutine(Fade(scenename, fader));
            StopCoroutine(fadeSound1);
        }             
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

        GetComponent<AudioSource>().PlayOneShot(fadeSound,0.4f);
        
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
