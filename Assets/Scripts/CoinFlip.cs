using DG.Tweening;
using UnityEngine;
using System;
// Author: Kyle Angeles
// Date-Written 2026/4/6
// File: CoinFlip.cs
// Description: Coin flip is responsbile for determining players turn at the beginning of the game
// This only occurs at the start of the game.
// Coin either hits heads or tails


public class CoinFlip : MonoBehaviour
{
    public bool flipped;
    public bool HasFlipped { get; private set; }
    public bool IsHeads => flipped;

    // Coinflip event
    public event Action<bool> OnFlipFinished;

    private void Flip()
    {
        flipped = UnityEngine.Random.value > 0.5f;
        HasFlipped = true;

        transform.DORotate(new Vector3(0, flipped ? 0f : 180f, 0), 0.25f)
            .OnComplete(() =>
            {
                bool isHeads = flipped;

                Debug.Log(isHeads
                    ? "Heads - Player 1 goes first!"
                    : "Tails - Player 2 goes first!");

                // This triggers the event
                OnFlipFinished?.Invoke(isHeads);
            });
    }
}
