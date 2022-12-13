using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    // Start is called before the first frame update
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
    public void Continue(FPSController player)
    {
        player.canShoot1 = true;
        player.canShoot2 = true;
    }
}
