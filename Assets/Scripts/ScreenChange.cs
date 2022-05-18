using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenChange : MonoBehaviour
{
    public Canvas canvas = null;

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Project()
    {
        SceneManager.LoadScene("Forest");
    }

    public void GameInfo()
    {
        SceneManager.LoadScene("GameInfo");
    }

    public void Exit()
    {
        Application.Quit();
    }
    public void GameInv1()
    {
        Time.timeScale = 0;
        canvas = GetComponentInParent<Canvas>();
        canvas.enabled = false;
        canvas = GameObject.FindGameObjectWithTag("Inv1").GetComponent<Canvas>();
        canvas.enabled = true;
    }
    public void GameInv2()
    {
        Time.timeScale = 1;
        canvas = GetComponentInParent<Canvas>();
        canvas.enabled = false;
        canvas = GameObject.FindGameObjectWithTag("Inv2").GetComponent<Canvas>();
        canvas.enabled = true;
    }
}