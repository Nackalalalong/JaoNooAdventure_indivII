using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    public Image fadeImage;
    public GameObject backgroundMusicPlayer;
    // Start is called before the first frame update
    void Start()
    {
        backgroundMusicPlayer.GetComponent<AudioSource>().Pause();
        FadeIn();
    }

    void FadeIn(){
        fadeImage.canvasRenderer.SetAlpha(0.0f);
        fadeImage.CrossFadeAlpha(0.8f, 2f, false);
    }

}
