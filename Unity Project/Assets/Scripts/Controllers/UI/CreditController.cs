using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditController : MonoBehaviour
{
    public float speed = 1;
    public AudioClip music;

    private int finalPosition = 1900;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed);

        if (transform.position.y >= finalPosition)
        {
            UIController controller = new UIController();
            controller.changeScene("Main Menu");
        }
    }
}
