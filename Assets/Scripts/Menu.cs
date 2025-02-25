using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    void Start()
    {
        Time.timeScale = 1;        
    }


    public void Jugar(string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena);
    }

   public void Salir()
    {
        Application.Quit();
        Debug.Log("Salir del juego");
    }
}
