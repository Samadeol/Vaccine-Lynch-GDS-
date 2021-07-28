using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    private void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void  LoadLevel()
    {
        SceneManager.LoadScene("Scene_A");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
