using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonsController : MonoBehaviour
{
    public void Play(){
        SceneManager.LoadScene("Story");
    }

    public void Exit(){
        Application.Quit();
    }
}
