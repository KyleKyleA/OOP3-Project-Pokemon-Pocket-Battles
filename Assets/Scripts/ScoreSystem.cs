using UnityEngine;
using Unity.UI;
using Unity.VectorGraphics;
using UnityEngine.SceneManagement;
// Author: Kyle Angeles
// File: ScoreSystem.cs
// Date-Written: 2026/4/16
// Description: This class is responsible for determing player and ai's score 
// which the player and AI go head to head till 3 points determining the winner.
public class ScoreSystem : MonoBehaviour
{
    public static ScoreSystem instance;
    // Constant
    // Win score for this game goes up to 3 points.
    private const int win_score = 3;
    // Variable Declaration
    // Scores for players are set to 0
    private int playerOneScored = 0;
    private int playerTwoScored = 0;



    /// <summary>
    /// Function Responsible for incrementing player one's score if they take defeated one card
    /// from player two's 
    /// </summary>
    /// <param name="playerOneScored"></param>
    public void AddPlayerOnePoint(int playerOneScored) 
    {

        playerOneScored++;
        Debug.Log($"Player one has defeat player two's card. ");
        Debug.Log($"Player one has earned a point: {playerOneScored}");
    }

    /// <summary>
    /// Function helps with incrementing player two's score if they defeated one card
    /// </summary>
    /// <param name="playerTwoScored"></param>
    public void AddPlayerTwoPoint(int playerTwoScored)
    {
        playerTwoScored++;
        Debug.Log($"Player two has defeated player's two's card. ");
        Debug.Log("Player Two has scored! a point. ");
    }


    // Check win condition after the game has been settled
    public void winCondition()
    {

        if (playerOneScored >= win_score)
        {
            Debug.Log("PLayer one has won the game");
        }
        else if (playerTwoScored >= win_score)
        {
            Debug.Log("Player two has won the game");

            //TODO: Win screen
            // This will load the win screen
            SceneManager.LoadScene("");
        }

        
    }
    

    // 
    public void resetScore()
    {
        playerOneScored = 0;
        playerTwoScored = 0;
        Debug.Log("Scores have been resetted");
    }

   
}

