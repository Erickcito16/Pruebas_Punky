using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject objetoMenuPause;
    public bool pause = false;




    private void Update()
    {
       if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pause == false)
            {
                objetoMenuPause.SetActive(true);
                pause = true;
                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else if (pause == true)
            {
                Resume();
            }

            

        }

    }

    public void Resume()
    {
        objetoMenuPause.SetActive(false);
        pause = false;
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }  
    
    public void MenuPrincipal(string NombreMenu)
    {
        SceneManager.LoadScene("Menu");
    }
}
