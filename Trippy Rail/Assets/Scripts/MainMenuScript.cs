using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(1);  // Start game
    }

    public void Quit()
    {
        SceneManager.UnloadSceneAsync(0);
    }
}
