using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameRules : MonoBehaviour
{
    public GameObject page1;
    public GameObject page2;


    void Start()
    {
        ShowPage(1);

    }

    public void ShowPage(int pageNumber)
    {
        if (page1 != null) page1.SetActive(pageNumber == 1);
        if (page2 != null) page2.SetActive(pageNumber == 2);
    }

    public void nextPage() => ShowPage(2);
    public void showPrevious() => ShowPage(1);


    public void exitButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
