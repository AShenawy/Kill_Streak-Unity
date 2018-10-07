using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToLevel : MonoBehaviour 
{
    [SerializeField] string levelToLoad;

    public void GoToScene()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
