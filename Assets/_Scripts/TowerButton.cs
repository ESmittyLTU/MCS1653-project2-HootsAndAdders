using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour
{
    public GameObject TowerPrefab;
    public int cost = 0;
    public AudioClip buildSound;
    Button button;

    private void Start()
    {
        button = GetComponent<Button>();
    }

    public void OnClick()
    {
        if (GameManager.money >= cost)
        {
        GameManager.money -= cost;
        GameManager.moneyCounter.SetText($"{GameManager.money}");
        AudioSource.PlayClipAtPoint(buildSound, transform.position);

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

    private void Update()
    {
        if (GameManager.money < cost)
        {
            button.interactable = false;
        } else
        {
            button.interactable = true;
        }
    }
}
