using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTrigger : TriggerObject
{
    public float force = 10;
    public AudioClip ImpactSound;
    private AudioSource audioSource;

    public override void TriggerEffect(GameObject player = null)
    {
        if (currentCount < triggerCount || triggerCount <= 0)
        {
            if(player != null)
                StartCoroutine(PushAnimationManager(player));
            transform.parent.GetComponent<Rigidbody>().isKinematic = false;
            transform.parent.GetComponent<Rigidbody>().AddTorque(force * transform.forward, ForceMode.Impulse);
        }
    }
    IEnumerator PushAnimationManager(GameObject player)
    {
        if(ImpactSound != null)
            audioSource.PlayOneShot(ImpactSound);
        player.GetComponentInChildren<Animator>().SetBool("Push", true);
        yield return new WaitForSeconds(2);
        player.GetComponentInChildren<Animator>().SetBool("Push", false);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (transform.parent.GetComponent<Rigidbody>() == null)
        {
            transform.parent.gameObject.AddComponent<Rigidbody>();
            transform.parent.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }

        if (GetComponent<AudioSource>() == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
