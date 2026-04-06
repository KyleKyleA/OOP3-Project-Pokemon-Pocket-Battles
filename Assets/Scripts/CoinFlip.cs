using DG.Tweening;
using UnityEngine;
// Author: Kyle Angeles
// Date-Written 2026/4/6
// File: CoinFlip.cs
// Description: Coin flip is responsbile for determining players turn at the beginning of the game
// This only occurs at the start of the game.

public class CoinFlip : MonoBehaviour
{
    
    // Variable Declaration setting coin flip to boolean
    private bool flipped = false;

    // Variable Declaration setting the coin flip to see if it actually flipped
    private bool flippedCoin = false;

    /// <summary>
    /// Update coin flip when a key is clicked 
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !flippedCoin)
            Flip();
      
    }

    /// <summary>
    /// This function is responsbile for flipping the coin at the start of the game
    /// when the coin is clicked assuming it returns a heads or a tails rotating 180 degree
    /// </summary>
    private void Flip()
    {
        bool hasFlipped = true;
        flipped = !flipped;

        transform.DORotate(new(0, flipped ? 0f : 180f, 0), 0.25f).OnComplete(() =>
        {
            bool isHeads = flipped;
            Debug.Log(isHeads ? "Heads - Player 1 goes first!" : "Tails - Player 2 goes first!");
        });
        
    }



}
