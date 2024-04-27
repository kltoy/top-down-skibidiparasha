using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammmmNAvernoePause : MonoBehaviour
{
    public GameObject pausePanel;
    public bool isActive;

    void Start()
    {
        
    }
    public void SwitchPause()
    {
        if (shop.isActive == true)
        {
            return;
        }
        if (isActive == true)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1.0f;

            isActive = false;
        }
        else
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
            isActive = true;
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SwitchPause();
        }
    }
}
