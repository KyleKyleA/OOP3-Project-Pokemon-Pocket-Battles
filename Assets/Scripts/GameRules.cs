using UnityEngine;


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
        page1.SetActive(pageNumber == 1);
        page2.SetActive(pageNumber == 2);
    }

    public void nextPage() => ShowPage(2);
    public void showPrevious() => ShowPage(1);
}
