using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelButton : MonoBehaviour
{
    public void onClick()
    {
        Destroy(GameObject.Find("BuildMenu"));
    }
}
