using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject instructionPanel;

    public void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void RestartGame()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
        Time.timeScale = 1;
    }

    public void BackToHome()
    {
        SceneManager.LoadScene("HomeScene");
    }

    public void OKButton()
    {
        Time.timeScale = 1;
        instructionPanel.SetActive(false);
    }

}
