using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseCondition : MonoBehaviour
{
    private GameObject[] m_flowers;
    private GameObject[] m_enemies;

    public GameObject m_winScreen;
    public GameObject m_lossScreen;

    private void Start()
    {
        Time.timeScale = 1;
    }

    private void Update()
    {
        m_flowers = GameObject.FindGameObjectsWithTag("Flower");
        m_enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (m_flowers.Length == 0)
        {
            Loss();
        }

        if (m_enemies.Length == 0)
        {
            Win();
        }
    }

    public void Win()
    {
        m_winScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void Loss()
    {
        m_lossScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
