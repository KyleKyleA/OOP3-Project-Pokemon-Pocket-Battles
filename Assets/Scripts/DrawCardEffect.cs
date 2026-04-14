using UnityEngine;

// Author: Alex Tan
// File: DrawCardEffect.cs
// Date-Written: April 13th 2026
// Description: Effect that draws a number of cards for the current player.
[CreateAssetMenu(menuName = "Effects/Draw Card")]
public class DrawCardEffect : AbilityEffect
{
    public int cardsToDraw;

    public override void Execute(PokemonCard user, PokemonCard target, TurnSystem turnSystem)
    {
        if (turnSystem == null) { Debug.LogWarning("DrawCardEffect: No TurnSystem!"); return; }

        for (int i = 0; i < cardsToDraw; i++)
            turnSystem.drawCard(turnSystem.currentPlayer);

        Debug.Log("Drew " + cardsToDraw + " card(s)!");
    }
}
