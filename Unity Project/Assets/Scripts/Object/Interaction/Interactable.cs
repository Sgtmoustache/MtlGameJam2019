using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract void Interact(GameObject player, bool input);
    public abstract void OnStart();

    public void Start()
    {
        OnStart();
    }
    public void Update()
    {

    }
}
