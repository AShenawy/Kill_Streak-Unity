using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour 
{
    [Header("Screens"),SerializeField] GameObject startScreen;
    [SerializeField] GameObject howToPlayScreen;
    [SerializeField] GameObject creditsScreen;

    [Header("Buttons"), SerializeField] GameObject quitButton;

    // Use this for initialization
    void Start () 
	{
        // set default state
        GoStartScreen();

        // display quit based on build
        DisplayQuitForBuild();
    }
	
	public void GoStartScreen ()
    {
        startScreen.SetActive(true);
        howToPlayScreen.SetActive(false);
        creditsScreen.SetActive(false);
    }

    public void GoHowTo()
    {
        startScreen.SetActive(false);
        howToPlayScreen.SetActive(true);
    }

    public void GoCredits ()
    {
        startScreen.SetActive(false);
        creditsScreen.SetActive(true);
    }

    void DisplayQuitForBuild ()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsPlayer:
            case RuntimePlatform.OSXPlayer:
            case RuntimePlatform.LinuxPlayer:
                quitButton.SetActive(true);
                break;

            default:
                quitButton.SetActive(false);
                break;
        }
    }
}
