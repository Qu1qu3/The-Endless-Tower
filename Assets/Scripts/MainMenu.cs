using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    public void playgame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
    }
    public void QuitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
        Application.Quit();
    }
    public void controlMusica (float sliderMusica)
    {
        audioMixer.SetFloat("VolumenMusica",Mathf.Log10(sliderMusica)*20);
    }
    public void returnToMenu()
    {
        SceneManager.LoadScene(0);
    }

}
