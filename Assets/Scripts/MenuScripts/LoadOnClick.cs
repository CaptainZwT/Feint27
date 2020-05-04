using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadOnClick : MonoBehaviour
{

    public GameObject loadingImage;
    public GameObject titleScreen;
    public GameObject mainMenu;
    public GameObject optionsMenu;

    public void LoadScene(string level)
    {
        if (level == "optionsMenu")
        {
            optionsMenu.SetActive(true);
            titleScreen.SetActive(false);
            mainMenu.SetActive(false);
        }
        else if (level == "mainMenu")
        {
            optionsMenu.SetActive(false);
            titleScreen.SetActive(true);
            mainMenu.SetActive(true);
        }
        else if (level == "startGame")
        {
            loadingImage.SetActive(true);
            SceneManager.LoadScene(1);
        }
        else
        {

        }
    }
}