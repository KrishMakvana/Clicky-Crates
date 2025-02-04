using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Reflection;
public class MainMenu : MonoBehaviour
{
    public Text credits;
    public GameObject Title;
    public void PlayGame()
    {
        SceneManager.LoadScene("Prototype 5");
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
    public void Back()
    {
        SceneManager.LoadScene("MainMenu");        
    }
    public void Instruction()
    {
        SceneManager.LoadScene("Instruction");
    }
}
