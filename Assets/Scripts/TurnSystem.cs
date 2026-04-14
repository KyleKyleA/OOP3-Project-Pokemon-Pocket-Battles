using UnityEngine;

// Author: Kyle Angeles, Alex Tan
// File: TurnSystem.cs
// Date-Written: 2026/4/9
// Description: This script is responsible for handling player and AI turns during the game 
// Features include the end turn button to handle the players and AI turns.
public class TurnSystem : MonoBehaviour
{
    public enum Player { PlayerOne, PlayerTwo }

    public enum GameState { PlayerOneTurn, PlayerTwoTurn, Attack, GameOver }

    public GameState currentState;
    public Player currentPlayer;

    public int playerOneHandCount = 0;
    public int playerTwoHandCount = 0;

    // Player's at hand can only have 5 cards but a maxium of 30 cards within a deck
    public int startingHandSize = 5;
    public int maxHandSize = 30;

    // Player's once per turn actions
    private bool hasAttachedEnergy = false;
    private bool hasPlayedSupporterCard = false;
    private bool hasEvolvedPokemon = false;

    // UI Flags
    public bool CanAttachEnergy() => !hasAttachedEnergy;
    public bool CanPlayTrainer() => !hasPlayedSupporterCard;

    //Is this the first turn?
    private bool isFirstTurn = true;

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
        if (player == Player.PlayerOne)
        {
            if (playerOneHandCount >= maxHandSize)
            {
                Debug.Log("Player One hand is full and cannot draw another card");
                return;
            }

            playerOneHandCount++;
            Debug.Log("Player One drew a card. Hand size: " + playerOneHandCount);
        }
        else if (player == Player.PlayerTwo)
        {
            if (playerTwoHandCount >= maxHandSize)
            {
                Debug.Log("Player Two hand is full and cannot draw another card");
                return;
            }

            playerTwoHandCount++;
            Debug.Log("Player Two drew a card. Hand size: " + playerTwoHandCount);
        }
    }

    /// <summary>
    /// Play the pokemon card when players turn if they have a basic card
    /// </summary>
    public void playPokemon()
    {

    }

    /// <summary>
    /// Each Turn a player must attach an energy to one of their bench or played pokemon card
    /// </summary>
    public void AttachEnergy()
    {

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
    public void Attack()
    {

    }

    /// <summary>
    /// Once the player ends their turn it becomes either player ones or player two's turn vice versa throughout the game
    /// </summary>
    public void EndTurn()
    {
        hasAttachedEnergy = false;
        hasPlayedSupporterCard = false;
        hasEvolvedPokemon = false;

        if (isFirstTurn)
        {
            isFirstTurn = false;
        }

        currentPlayer = (currentPlayer == Player.PlayerOne) ? Player.PlayerTwo : Player.PlayerOne;
        currentState = (currentPlayer == Player.PlayerOne) ? GameState.PlayerOneTurn : GameState.PlayerTwoTurn;

        Debug.Log(currentPlayer + "'s turn");
    }
}