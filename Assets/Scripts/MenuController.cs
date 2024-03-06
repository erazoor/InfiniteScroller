using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Debug.Log("Quitter le jeu !");
        Application.Quit();
    }
}
