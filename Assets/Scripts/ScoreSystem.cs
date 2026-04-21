using Unity.UI;
using Unity.VectorGraphics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;


// Author: Kyle Angeles
// File: ScoreSystem.cs
// Date-Written: 2026/4/16
// Description: This class is responsible for determing player and ai's score 
// which the player and AI go head to head till 3 points determining the winner.
public class ScoreSystem : MonoBehaviour
{

    // Update Score Board UI
    // 1-3 numbers images for player score
    public Image[] aiScoreImage; 
    public Image[] playerScoreImage;

    // Instance for score system
    public static ScoreSystem instance;

    // Constant
    // Win score for this game goes up to 3 points.
    private const int win_score = 3;
    // Variable Declaration
    // Scores for players are set to 0
    private int playerOneScore = 0;
    private int playerTwoScore = 0;


    public Color scoredColor = Color.red;
    public Color unscoredColor = Color.grey;
    



    /// <summary>
    /// This function shows 
    /// and works by going through a for loop starting at 0 then incrementing by 1 each time
    /// a player has knocked out a pokemon card then later it would increment their score by 1 till they
    /// hit 3
    /// </summary>
    void updateScoreUI()
    {
        for (int i = 0; i < aiScoreImage.Length; i++)
            aiScoreImage[i].color = i < playerTwoScore? scoredColor : unscoredColor;
        for (int i = 0; i < playerScoreImage.Length; i++)
            playerScoreImage[i].color = i < playerOneScore ? scoredColor : unscoredColor;
    }
    /// <summary>
    /// Function Responsible for incrementing attacking player's score if pokemon knocked out
    /// If Player one for instance has knocked an opponent increment their score by 1 till 3
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
        updateScoreUI(); 
    }



    // Check which player won after the game has been settled
    /// <summary>
    /// Once the condition has been setteled display a winning screen to 
    /// the usre that they have work the game.
    /// </summary>
    public void winCondition()
    {

        if (playerOneScore >= win_score)
        {
            Debug.Log("PLayer one has won the game");
            PlayerPrefs.SetString("Winner", "Player One");
            SceneManager.LoadSceneAsync(4);
        }
        else if (playerTwoScore >= win_score)
        {
            Debug.Log("Player two has won the game");
            PlayerPrefs.SetString("Winner", "Player Two");
            SceneManager.LoadSceneAsync(4);
        }

    
    }

    // Checks if there is a winner
    public bool HasWinner()
    {
        return playerOneScore >= win_score || playerTwoScore >= win_score;
    }

    /// <summary>
    /// Reset Scores
    /// </summary>
    public void resetScore()
    {
        playerOneScore = 0;
        playerTwoScore = 0;
        Debug.Log("Scores have been resetted");
    }

   
}

