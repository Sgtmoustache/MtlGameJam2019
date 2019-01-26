using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterController characterController;
    void Start()
    {
        characterController = new CharacterController();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static GameObject getPlayer()
    {
        //TODO
        return new GameObject();
    }
}
