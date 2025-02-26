using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildable : MonoBehaviour
{
    public Material BaseMaterial, HighlightMaterial;
    public GameObject BuildMenu;

    public void OnMouseOver()
    {
        gameObject.GetComponent<MeshRenderer>().material = HighlightMaterial;
    }

    public void OnMouseExit()
    {
        gameObject.GetComponent<MeshRenderer>().material = BaseMaterial;
    }

    public void OnMouseDown()
    {
        Instantiate(BuildMenu, transform);
    }
}
