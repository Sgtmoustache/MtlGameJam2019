using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbable : Interactable
{
    GameObject player;
    Rigidbody r_player;
    bool move_up;
    bool move_side;
    int count;
    int max_count;
    float travel;
    float distance;

    

    public override void Interact(GameObject character, bool input)
    {
        player = character;
        if (input && transform.lossyScale.y / 2 + transform.position.y - player.transform.position.y <= 0)
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
        Debug.Log("climb");
        count = 0;
        travel = 0;
        r_player = player.GetComponent<Rigidbody>();
        r_player.useGravity = false;
        distance = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(player.transform.position.x, 0, player.transform.position.z));

        Debug.Log(transform.lossyScale.y / 2 + transform.position.y - player.transform.position.y);

        if (transform.lossyScale.y / 2 + transform.position.y - player.transform.position.y < -0.4)
            max_count = 25;
        else
            max_count = 50;

        player.SendMessage("StopForward", false);
        player.SendMessage("StopSideWay", false);
        player.SendMessage("StopRotation", false);

        move_up = true;
    }

    void Update()
    {
        if (move_up)
        {
            player.transform.Translate(0, 0.02f, 0);
            count++;
            if(count >= max_count)
            {
                move_up = false;
                move_side = true;
            }
        }
        if (move_side)
        {
            player.transform.Translate(0, 0, 0.04f);
            travel += 0.04f;
            if(travel >= distance)
            {
                r_player.useGravity = true;
                move_side = false;
                player.SendMessage("StopForward", true);
                player.SendMessage("StopSideWay", true);
                player.SendMessage("StopRotation", true);
            }
        }
    }
}
