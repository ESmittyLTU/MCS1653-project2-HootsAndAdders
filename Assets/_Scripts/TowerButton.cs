using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerButton : MonoBehaviour
{
    public GameObject TowerPrefab;

    public void OnClick()
    {
        Buildable b = GetComponentInParent<Buildable>();

        //Gets transform of buildable tile
        Transform tile = b.transform;
        Instantiate(TowerPrefab, tile.position, Quaternion.identity);
        
        //Hides menu
        b.HideMenu();

        //Dehilights tile then destroys buildable script once tower is built
        b.Unhighlight();
        Destroy(b);
    }
}
