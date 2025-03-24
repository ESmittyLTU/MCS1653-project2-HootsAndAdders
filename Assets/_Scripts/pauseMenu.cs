using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseMenu : MonoBehaviour
{
    public GameObject self;
    public static bool paused;

    void Start()
    {
        self.SetActive(false);
        paused = false;
    }
    void Update()
    {
        if (!paused && Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("ESCAPED");
            self.SetActive(true);
            Time.timeScale = 0f;
            paused = true;
        }
        else if (paused && Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1f;
            self.SetActive(false);
            paused = false;
        }
    }
}
