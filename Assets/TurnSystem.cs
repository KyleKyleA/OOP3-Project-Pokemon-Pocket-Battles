using UnityEngine;

// Author: Kyle Angeles, Alex Tan, karim el moussadeq

public class TurnSystem : MonoBehaviour
{
    // this will manage the flow of the game which includes the turn order, player actions and game state transitions
    public enum Player { PlayerOne, PlayerTwo }
    public enum GameState { PlayerOneTurn, PlayerTwoTurn, Attack, GameOver }

    public GameState currentState;
    public Player currentPlayer;

    public int playerOneHandCount = 0;
    public int playerTwoHandCount = 0;

    public int startingHandSize = 5;
    public int maxHandSize = 30;

    private bool hasAttachedEnergy = false;
    private bool hasPlayedSupporterCard = false;
    private bool hasEvolvedPokemon = false;

    public bool CanAttachEnergy() => !hasAttachedEnergy;
    public bool CanPlayTrainer() => !hasPlayedSupporterCard;

    private bool isFirstTurn = true;

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

    public void drawCard(Player player)
    {
        // Pulls a real card from the DeckManager 
        CardData drawnCard = DeckManager.Instance.DrawCard();

        if (drawnCard == null)
        {
            Debug.Log("No cards left in deck!");
            return;
        }

        if (player == Player.PlayerOne)
        {
            if (playerOneHandCount >= maxHandSize)
            {
                Debug.Log("Player One hand is full.");
                return;
            }
            playerOneHandCount++;
            Debug.Log("Player One drew: " + drawnCard.name + " | Hand size: " + playerOneHandCount);
        }
        else if (player == Player.PlayerTwo)
        {
            if (playerTwoHandCount >= maxHandSize)
            {
                Debug.Log("Player Two hand is full.");
                return;
            }
            playerTwoHandCount++;
            Debug.Log("Player Two drew: " + drawnCard.name + " | Hand size: " + playerTwoHandCount);
        }
    }

    public void playPokemon() { }

    public void AttachEnergy() { }

    public void UseAbility() { }

    public void Attack()
    {
        Debug.Log(currentPlayer + " attacks!");
    }

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