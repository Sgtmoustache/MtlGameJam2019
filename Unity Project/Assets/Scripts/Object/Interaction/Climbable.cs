using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbable : Interactable
{

    public override void Interact(GameObject player, bool input)
    {
        
        if (input && transform.lossyScale.y/2 + transform.position.y - player.transform.position.y <= 0)
        {
            Climb(player);    
        }
        

    }

    public override void OnStart()
    {

    }

    public void Climb(GameObject player)
    {
            Rigidbody r_player = player.GetComponent<Rigidbody>();
            r_player.useGravity = false;

            Debug.Log("climb");
            Vector3 newPosition = player.transform.position;
            newPosition.y += transform.lossyScale.y / 2 + transform.position.y;
            Debug.Log(player.transform.position);
            Debug.Log(newPosition);
            player.transform.position = Vector3.MoveTowards(player.transform.position, newPosition, Time.deltaTime * 100);

            /*
            float m_forward = Input.GetAxis("Vertical") * speed;
            float m_sideway = Input.GetAxis("Horizontal") * speed;
            m_forward *= Time.deltaTime;
            m_sideway *= Time.deltaTime;
            transform.Translate(m_sideway, 0, m_forward);*/

            player.transform.Translate(0, 0, 1);
            r_player.useGravity = true;
    }
}
