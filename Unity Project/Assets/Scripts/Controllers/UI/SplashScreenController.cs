using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SplashScreenController : MonoBehaviour
{
    public AudioClip audio;
    private AudioSource audiosource;
    public RawImage img;

    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Timer()
    {
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            // set color with i as alpha
            img.color = new Color(1, 1, 1, i);
            yield return null;
        }
        yield return new WaitForSeconds(3);
        audiosource.Stop();
        audiosource.PlayOneShot(audio);
        // loop over 1 second backwards
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            // set color with i as alpha
            img.color = new Color(1, 1, 1, i);
            yield return null;
        }
        yield return new WaitForSeconds(2);
        LoadMenu();
    }

    private void LoadMenu()
    {
        UIController sceneswitcher = new UIController();
        sceneswitcher.changeScene("Main Menu");
    }
}
