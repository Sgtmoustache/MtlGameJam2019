using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbable : Interactable
{

    private GameObject player;
    private Rigidbody r_player;

    private bool first_pass;
    private bool move_up;
    private bool move_side;
    private bool done_up;
    private bool done_side;

    private float height;
    private float distance;
    public float soundDelay;

    private Vector2 travel;
    public AudioClip climbingSound;
    private AudioSource audioSource;

    public override void Interact(GameObject character, bool input)
    {
        if (first_pass)
        {
            player = character;
            height = GetComponent<Collider>().bounds.size.y + transform.position.y - player.transform.position.y + 1;
            if (this.gameObject.tag.Equals("Cube"))
                height = GetComponent<Collider>().bounds.size.y / 2 + transform.position.y - player.transform.position.y + 1 + 0.1f;

            Debug.Log("hauteur  :" + height);

            if (height <= 1.1 && height > 0.30)
            {
                
                StartCoroutine(Climb());
            }
            first_pass = false;
        }
    }


    public override void OnStart()
    {
        move_up = false;
        first_pass = true;
        audioSource = gameObject.AddComponent<AudioSource>();
        done_up = false;
        done_side = false;
    }

    IEnumerator Climb()
    {
        currentActive = TypeOfAction.CLIMABLE;
        player.GetComponentInChildren<Animator>().SetBool("Climb", true);
        player.SendMessage("StopForward", false);
        player.SendMessage("StopSideWay", false);
        player.SendMessage("StopRotation", false);

        yield return new WaitForSeconds(soundDelay);
        if (climbingSound != null && !audioSource.isPlaying)
            audioSource.PlayOneShot(climbingSound);
        yield return new WaitForSeconds(soundDelay-1);

        RaycastHit hit;
        Ray rayFeet = new Ray(player.transform.GetChild(3).transform.position, player.transform.forward);
        Physics.Raycast(rayFeet, out hit, 1);

        r_player = player.GetComponent<Rigidbody>();
        r_player.useGravity = false;
        distance = Vector3.Distance(new Vector3(hit.point.x, 0, hit.point.z), new Vector3(player.transform.position.x, 0, player.transform.position.z));
        Debug.Log("Distance : " + distance);

        move_up = true;
        move_side = true;
    }

    public void finish()
    {
        r_player.useGravity = true;
        move_side = false;
        player.SendMessage("StopForward", true);
        player.SendMessage("StopSideWay", true);
        player.SendMessage("StopRotation", true);
        first_pass = true;
        done_up = false;
        done_side = false;
        travel = new Vector2();
        currentActive = TypeOfAction.NOTHING;
    }

    void Update()
    {
        Debug.Log("travel : " + travel);
        if (move_up)
        {
            player.transform.Translate(0, 0.05f, 0);
            travel.y += 0.04f;
            if (travel.y >= height)
            {
                move_up = false;
                done_up = true;
            }
        }
        if (move_side)
        {
            player.transform.Translate(0, 0, 0.03f);
            travel.x += 0.02f;
            if(travel.x >= distance)
            {
                move_side = false;
                done_side = true;
            }
        }
        if (done_up && done_side)
            finish();
    }
}
