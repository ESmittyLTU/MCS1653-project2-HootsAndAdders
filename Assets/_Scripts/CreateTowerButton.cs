using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTowerButton : MonoBehaviour
{
    public GameObject TowerPrefab;

    public void OnClick()
    {
        Transform tile = GameObject.Find("BuildMenu").transform.parent;
        Instantiate(TowerPrefab, tile.position, tile.rotation, null);
        //Destroy menu
    }
}
