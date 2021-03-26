using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{


    



    [SerializeField]
    private GameObject pausePanel;

    

    void Awake()
    {
        pausePanel.SetActive(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);

    } 

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.LoadLevel("Main Menu");
    }




}
