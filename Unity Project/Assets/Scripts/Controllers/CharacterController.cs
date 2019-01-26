using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    //Mouvement
    public float speed = 1.5f;

    //RayCastLook
    public GameObject hands;


    //CamLook
    Vector2 mouseLook;
    Vector2 smoothVector;
    public float sensitivity = 1.0f;
    public float smoothing = 2.0f;


    GameObject view;
    GameObject character;

    public float interactionRange = 10f;
    private bool isLeftClicking = false;

    // Start is called before the first frame update
    void Start()
    {
        //CamLook
        Cursor.lockState = CursorLockMode.Locked;
        character = this.transform.GetChild(0).gameObject;
        view = character.transform.GetChild(0).gameObject;
    }

    void Update()
    {
        //Mouvement
        float m_forward = Input.GetAxis("Vertical") * speed;
        float m_sideway = Input.GetAxis("Horizontal") * speed;
        m_forward *= Time.deltaTime;
        m_sideway *= Time.deltaTime;

        transform.Translate(m_sideway, 0, m_forward);

        //CamLook
        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;

        if (Cursor.lockState != CursorLockMode.None)
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
            transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);

            isLeftClicking = Input.GetMouseButton(0);
        }

        checkForObject();
    }
    private void checkForObject()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward,Color.red);

        Ray ray = new Ray(hands.transform.position, hands.transform.forward);

        if (Physics.Raycast(ray, out hit, interactionRange))
        {
            Debug.Log(hit.collider.gameObject.name);
            Debug.DrawLine(hands.transform.position, hit.point, Color.red);

            if (hit.collider.gameObject.GetComponent<Interactable>() != null)
            {
                hit.collider.gameObject.GetComponent<Interactable>().Interact(gameObject, isLeftClicking);
            }
        }
    }
}
