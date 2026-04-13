using UnityEngine;

// Author: Kyle Angeles, Alex Tan
// File: TurnSystem.cs
// Date-Written: 2026/4/9
// Description: This script is responsible for handling player and AI turns during the game 
// Features include the end turn button to handle the players and AI turns.
public class TurnSystem : MonoBehaviour 
{
    public enum Player { PlayerOne, PlayerTwo }

    public enum GameState { PlayerOneTurn, PlayerTwoTurn, Attack, GameOver };

    public GameState currentState;
    public Player currentPlayer;

    // Player's at hand can only have 5 cards but a maximum of 30 cards within a deck
    public int startingHandSize = 5;
    public int maxHandSize = 10;
    public int maxDeckSize = 30;

    // Player's once per turn actions
    private bool hasAttachedEnergy = false;
    private bool hasPlayedSupporterCard = false;
    private bool hasEvolvedPokemon = false;

    // The first turn of the game restricts some game actions
    private bool isFirstTurn = true;

    // UI button flags
    public bool CanAttachEnergy() => !hasAttachedEnergy;
    public bool CanPlaySupporter() => !hasPlayedSupporterCard;

    /// <summary>
    /// This would Display the current player's turn
    /// </summary>
    void Start()
    {
        currentPlayer = Player.PlayerOne;
        currentState = GameState.PlayerOneTurn;
        DrawStartingHand(Player.PlayerOne);
        DrawStartingHand(Player.PlayerTwo);
    }


    //
    public void DrawStartingHand(Player player)
    {
        for (int i = 0; i < startingHandSize; i++)
        {
            drawCard(player);
        }
    }

    /// <summary>
    /// drawing card method this would determine player draws a card and adds it to the players hand
    /// call this at the start of each turn 
    /// </summary>
    /// <param name="player"></param>
    public void drawCard(Player player)
    {
        if (currentPlayer == Player.PlayerOne)
        {
            if (currentPlayer == Player.PlayerOne)
            {
                if (playerHand.Count >= maxHandSize)
                {
                    Debug.Log("Player hand is full, and cannot draw another card");
                    return;
                }
            }
        }
    }


    /// <summary>
    /// Play the pokemon card when players turn if they have a basic card
    /// </summary>
    public void playPokemon()
    {

    }

    /// <summary>
    /// Each Turn a player can attach an energy to one of their bench or played pokemon card
    /// </summary>
    public void AttachEnergy()
    {
        if (hasAttachedEnergy)
        {
            Debug.Log("You can only attach one energy per turn!");
            return;
        }


    }

    /// <summary>
    /// Specific Pokemon Have ability they can use it but not at first turn if they go second yes they can
    /// </summary>
    public void UseAbility()
    {

    }


    /// <summary>
    /// Function is required to make the card actually play and attack the opposing card.
    /// </summary>
    public void useSkill()
    {

    }

    /// <summary>
    /// Once the player ends their turn it becomes either player ones or player two's turn vice versa throughout the game
    /// </summary>
    public void EndTurn()
    {
        if (isFirstTurn)
        {
            isFirstTurn = false;
        }

        // Reset once per turn flags
        hasAttachedEnergy = false;
        hasPlayedSupporterCard = false;
        hasEvolvedPokemon = false;
        
        currentPlayer = (currentPlayer == Player.PlayerOne) ? Player.PlayerTwo : Player.PlayerOne;
        currentState = (currentPlayer == Player.PlayerOne) ? GameState.PlayerOneTurn : GameState.PlayerTwoTurn;


    Debug.Log(currentPlayer + "'s turn");
    }
}
