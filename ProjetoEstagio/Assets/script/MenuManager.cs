using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private string menuSceneName;
    [SerializeField] private GameObject panelMainMenu;
    [SerializeField] private GameObject panelOptions;

    public void Play()
    {
        SceneManager.LoadScene(menuSceneName);
    }

    public void OpenOptions()
    {
        panelMainMenu.SetActive(false);
        panelOptions.SetActive(true);
    }

    public void CloseOptions()
    {
        panelOptions.SetActive(false);
        panelMainMenu.SetActive(true);
    }
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}