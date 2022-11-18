using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject m_pauseScreen;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    public void Pause()
    {
        m_pauseScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        m_pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }
}
