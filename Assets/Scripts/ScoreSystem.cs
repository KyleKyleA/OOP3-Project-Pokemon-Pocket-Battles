using Unity.UI;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
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
    private int playerOneScore = 0;
    private int playerTwoScore = 0;



    /// <summary>
    /// Function Responsible for incrementing attacking player's score if pokemon knocked out
    /// </summary>
    /// <param name="playerOneScore"></param>
    public void AddPoint(TurnSystem.Player player)
    {
        if (player == TurnSystem.Player.PlayerOne)
        {
            playerOneScore++;
            Debug.Log($"Player One scored. Total points: {playerOneScore}");
        }
        else
        {
            playerTwoScore++;
            Debug.Log($"Player Two scored. Total points: {playerTwoScore}");
        }
    }



    // Check which player won after the game has been settled
    public void winCondition()
    {

        if (playerOneScore >= win_score)
        {
            Debug.Log("PLayer one has won the game");
        }
        else if (playerTwoScore >= win_score)
        {
            Debug.Log("Player two has won the game");

            //TODO: Win screen
            // This will load the win screen
            SceneManager.LoadScene("");
        }

    
    }

    // Checks if there is a winner
    public bool HasWinner()
    {
        return playerOneScore >= win_score || playerTwoScore >= win_score;
    }

    // 
    public void resetScore()
    {
        playerOneScore = 0;
        playerTwoScore = 0;
        Debug.Log("Scores have been resetted");
    }

   
}

