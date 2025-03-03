using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelButton : MonoBehaviour
{
    public void onClick()
    {
        //Checks ANY parent of object for Buildable class and sets it to class buildable "b"
        //Calls HideMenu from buildable class
        Buildable b = GetComponentInParent<Buildable>();
        b.HideMenu();
    }
}
