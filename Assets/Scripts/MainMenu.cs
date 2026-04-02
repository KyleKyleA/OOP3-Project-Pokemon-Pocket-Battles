using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.TestTools;
// Author: 
// File:
// Date Written:
// Description:
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Declared a function in ordered for the play button to be play
    /// once the user clicks the button the user straight 
    /// heads to the gameboard where they can actually play the game
    /// </summary>
    public void playGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    /// <summary>
    /// Once a user clicks the game rule button
    /// it will show a list of rules which they should 
    /// follow in order to play fairly and have 
    /// a good user experience
    /// </summary>
    public void gameRules()
    {
        SceneManager.LoadSceneAsync(2);
    }


    /// <summary>
    /// This scene is used for decks
    /// where the user can create and manage their
    /// own decks 
    /// by searching, filtering, or sorting by
    /// </summary>
    public void decks()
    {
        SceneManager.LoadSceneAsync(3);
    }

    /// <summary>
    /// Function for exiting out of the application
    /// on the mainmenu 
    /// </summary>
    public void exitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }


// Exit button function for exiting out of the decks section
// of the game
public void exitDeckButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
