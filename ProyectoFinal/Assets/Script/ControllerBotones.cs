using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerBotones : MonoBehaviour
{
  
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Pausar()
    {
        Time.timeScale = 0f;
    }
    public void Reanudar()
    {
        Time.timeScale = 1f;
    }
    public void Exit()
    {
        SceneManager.LoadScene("SampleScene");
    }
    //menu
    public void Play()
    {
        SceneManager.LoadScene("Scene1");
    }
    public void FinalizarJuego()
    {
        Application.Quit();
    }
}
