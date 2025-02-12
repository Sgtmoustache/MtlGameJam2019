﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    //Mouvement
    public float speed = 1.5f;

    //RayCastLook
    public GameObject foward;
    public GameObject hands;
    public GameObject feets;

    //CamLook
    Vector2 mouseLook;
    Vector2 smoothVector;
    public float sensitivity = 1.0f;
    public float smoothing = 2.0f;


    GameObject Pmenu;
    GameObject view;

    private AudioSource WalkAudioSource;
    private AudioSource otherAudioSource;
    public AudioClip pauseSound;
    public AudioClip walkingSound;
    private Vector3 lastPosition;

    private bool gateForward;
    private bool gateSideway;
    private bool gateRotation;
    private bool pause;
    private bool outofpause;

    public float interactionRange = 10f;


    // Start is called before the first frame update
    void Start()
    {
        Pmenu = GameObject.Find("PauseMenu");
        Resume();
        WalkAudioSource = GetComponents<AudioSource>()[0];
        otherAudioSource = GetComponents<AudioSource>()[1];
        outofpause = false;
        pause = true;

        //CamLook
        view = transform.GetChild(0).gameObject;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        pause = true;
        gateForward = true;
        gateSideway = true;
        gateRotation = true;
        Pmenu.SetActive(false);
        outofpause = true;
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if(pauseSound != null)
                otherAudioSource.PlayOneShot(pauseSound);
            if(pause)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                pause = false;
                gateForward = false;
                gateSideway = false;
                gateRotation = false;
                Pmenu.SetActive(true);
                Time.timeScale = 0f;
            }  
        }
        
        //Mouvement
        float m_forward = Input.GetAxis("Vertical") * speed;
        float m_sideway = Input.GetAxis("Horizontal") * speed;
        m_forward *= Time.deltaTime;
        m_sideway *= Time.deltaTime;
        if (!gateSideway) m_sideway = 0;
        if (!gateForward) m_forward = 0;

        GetComponent<Rigidbody>().velocity = Vector3.Normalize(transform.forward) * m_forward + Vector3.Normalize(transform.right) * m_sideway + new Vector3(0,GetComponent<Rigidbody>().velocity.y,0);

        //transform.Translate(m_sideway, 0, m_forward);
        if (transform.position == lastPosition)
        {
            WalkAudioSource.Stop();
        }
        else if (!WalkAudioSource.isPlaying)
        {
            WalkAudioSource.Play();
        }

        //CamLook
        if (gateRotation)
        {
            //get mouse mouvement
            var mouse = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            mouse = Vector2.Scale(mouse, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
            smoothVector.x = Mathf.Lerp(smoothVector.x, mouse.x, 1f / smoothing);
            smoothVector.y = Mathf.Lerp(smoothVector.y, mouse.y, 1f / smoothing);
            mouseLook += smoothVector;

            //maximum angle Y
            if (mouseLook.y < -70)
                mouseLook.y = -70;
            if (mouseLook.y > 40)
                mouseLook.y = 40;

        //rotation of camera
            
            view.transform.localRotation = Quaternion.AngleAxis(Mathf.Clamp(-mouseLook.y, -30, 80), Vector3.right);
            transform.localRotation = Quaternion.AngleAxis(mouseLook.x, transform.up);
        }
        
        if(!outofpause)
        checkForObject();
        lastPosition = transform.position;
        outofpause = false;
    }

    public void StopForward(bool b)
    {
        gateForward = b;
    }
    public void StopSideWay(bool b)
    {
        gateSideway = b;
    }
    public void StopRotation(bool b)
    {
        gateRotation = b;
    }

    private void checkForObject()
    {

        int count = 0;
        RaycastHit hit;

        Ray rayFeet = new Ray(feets.transform.position, transform.forward);

        if (Physics.Raycast(rayFeet, out hit, interactionRange))
        {
            Debug.DrawLine(transform.position, hit.point, Color.red);
            
            foreach (Interactable interactable in hit.collider.gameObject.GetComponents<Interactable>())
            {
                if (Interactable.currentActive == TypeOfAction.NOTHING) {
                    GetComponentInChildren<Animator>().SetBool("Interact", true);
                }

                if (interactable is Climbable && Input.GetKey("space") && (Interactable.currentActive == TypeOfAction.NOTHING || Interactable.currentActive == TypeOfAction.CLIMABLE))
                {
                    Debug.Log("try to climb");
                    interactable.Interact(gameObject, true);
                }

                if (interactable is Pushable && (Interactable.currentActive == TypeOfAction.NOTHING || Interactable.currentActive == TypeOfAction.PUSHABLE))
                {
                    Debug.Log("try to push");
                    interactable.Interact(gameObject, Input.GetMouseButton(0));
                }
                count++;
            }
        }
       

        Ray rayFoward = new Ray(foward.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(rayFoward, out hit, interactionRange))
        {
            Debug.DrawLine(foward.transform.position, hit.point, Color.blue);
            foreach (Interactable interactable in hit.collider.gameObject.GetComponents<Interactable>())
            {

                if (Interactable.currentActive == TypeOfAction.NOTHING)
                {
                    GetComponentInChildren<Animator>().SetBool("Interact", true);
                }
                if ((interactable is Triggerable || interactable is Collectable || interactable is Pickable) && Input.GetMouseButtonDown(0))
                {
                    Debug.Log("try something");
                    interactable.Interact(gameObject, Input.GetMouseButton(0));
                }
                count++;
            }
        }
        
           if(count == 0) 
                GetComponentInChildren<Animator>().SetBool("Interact", false);
            
        
    }
}
