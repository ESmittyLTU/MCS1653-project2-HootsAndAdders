using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildable : MonoBehaviour
{
    public Material BaseMaterial, HighlightMaterial;
    public GameObject BuildMenu;

    private GameObject Menu;
    private bool MenuShowing = false;

    //If the menu isnt already up, make one
    public void ShowMenu()
    {
        if (!MenuShowing)
        {
            Menu = Instantiate(BuildMenu, transform);
            MenuShowing = true;
        }
    }

    //If menu is up, destroy it and update our bool
    public void HideMenu()
    {
        if (Menu != null)
        {
            Destroy(Menu);
        }
        MenuShowing = false;
    }

    public void Unhighlight()
    {
        gameObject.GetComponent<MeshRenderer>().material = BaseMaterial;
    }

    //Highlights/dehilights on mouse over
    public void OnMouseOver()
    {
        gameObject.GetComponent<MeshRenderer>().material = HighlightMaterial;
    }

    public void OnMouseExit()
    {
        Unhighlight();
    }

    //Calls the fx to make menu on click
    public void OnMouseDown()
    {
        ShowMenu();
    }
}
