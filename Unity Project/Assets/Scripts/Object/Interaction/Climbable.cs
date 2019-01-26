using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbable : Interactable
{
    GameObject player;
    Rigidbody r_player;
    bool move_up;
    bool move_side;
    float height;
    Vector2 travel;
    float distance;

    

    public override void Interact(GameObject character, bool input)
    {
        player = character;
        height = GetComponent<Collider>().bounds.size.y + transform.position.y - player.transform.position.y + 1;
        if(this.gameObject.tag.Equals("Cube"))
            height = GetComponent<Collider>().bounds.size.y / 2 + transform.position.y - player.transform.position.y + 1;

        Debug.Log("hauteur  :" + height);

        if (input && height <= 1.1 && height > 0.30)
        {
            Climb();
        }

    }

    public override void OnStart()
    {
        move_up = false;
    }

    public void Climb()
    {
        travel = new Vector2();
        r_player = player.GetComponent<Rigidbody>();
        r_player.useGravity = false;
        distance = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(player.transform.position.x, 0, player.transform.position.z));
        
        player.SendMessage("StopForward", false);
        player.SendMessage("StopSideWay", false);
        player.SendMessage("StopRotation", false);

        player.GetComponent<Collider>().isTrigger = true;

        move_up = true;
        move_side = true;
    }

    void Update()
    {
        if (move_up)
        {
            player.transform.Translate(0, 0.02f, 0);
            travel.y += 0.02f;
            if (travel.y >= height)
            {
                move_up = false;
            }
        }
        if (move_side)
        {
            player.transform.Translate(0, 0, 0.02f);
            travel.x += 0.04f;
            if(travel.x >= distance)
            {
                r_player.useGravity = true;
                move_side = false;
                player.SendMessage("StopForward", true);
                player.SendMessage("StopSideWay", true);
                player.SendMessage("StopRotation", true);

                player.GetComponent<Collider>().isTrigger = false;
            }
        }
    }
}
