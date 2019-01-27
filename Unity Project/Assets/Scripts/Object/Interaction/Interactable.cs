using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public static TypeOfAction currentActive = TypeOfAction.NOTHING;
    public abstract void Interact(GameObject player, bool input);
    public abstract void OnStart();

    public void Start()
    {
        OnStart();
    }
}

public enum TypeOfAction
{
    CLIMABLE = 0,
    COLLECTABLE = 1,
    PICKABLE = 2,
    PUSHABLE =3 ,
    TRIGGERABLE = 4,
    NOTHING = -1
}

