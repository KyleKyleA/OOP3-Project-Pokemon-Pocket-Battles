using UnityEngine;
using System.Collections.Generic;
using System.Linq;

// Author: Kyle Angeles, Alex Tan, Karim El Moussadeq
// description: this file is for the turn system of the game. it handles the flow of the game, 
//including player turns, drawing cards, playing Pokemon, attacking, and ending turns. 
//It also interacts with the AI for the opponent's turn and updates the UI accordingly.

public class TurnSystem : MonoBehaviour
{
    public enum Player { PlayerOne, PlayerTwo }
    public enum GameState { PlayerOneTurn, PlayerTwoTurn, Attack, GameOver }

    // game state variables
    [Header("Game State")]
    public GameState currentState;
    public Player currentPlayer;

    // Battle variables
    [Header("Battle Setup")]
    public PokemonCard playerOneActivePokemon;
    public PokemonCard playerTwoActivePokemon;
    public AI aiController;
    public ScoreSystem scoreSystem;

    // hand lists 
    [Header("Hand Lists")]
    public List<Card> playerOneHand = new List<Card>();
    public List<Card> playerTwoHand = new List<Card>();
    
    public int playerOneHandCount = 0;
    public int playerTwoHandCount = 0;
    public int startingHandSize = 5;
    public int maxHandSize = 10;

    // UI variables
    [Header("UI Visuals")]
    public GameObject pokemonCardPrefab; // This prefab should have the Card script attached
    public Transform playerOneHandArea;
    public Transform playerTwoHandArea;
    public Transform playerOneBenchArea;
    public PokemonImageDisplayUI imageDisplay;
    public PokemonStatsDisplayUI statsDisplay;

    // Deck variables
    [Header("Settings")]
    public bool drawAtStartOfTurn = true;
    public CardType defaultEnergyType;

    private bool hasAttachedEnergy = false;
    private bool isFirstTurn = true;

    // Start is called before the first frame update
    void Start()
    {
        if (scoreSystem == null) scoreSystem = ScoreSystem.instance;
        currentPlayer = Player.PlayerOne;
        currentState = GameState.PlayerOneTurn;

        DrawStartingHand(Player.PlayerOne);
        DrawStartingHand(Player.PlayerTwo);
        BeginTurn();
    }

    // Method to draw the starting hand for each player at the beginning of the game
    public void DrawStartingHand(Player player) { for (int i = 0; i < startingHandSize; i++) drawCard(player); }

    // Method to draw a card for the specified player, with checks for hand size limits and null card data
    public void drawCard(Player player)
    {
        if ((player == Player.PlayerOne && playerOneHandCount >= maxHandSize) || 
            (player == Player.PlayerTwo && playerTwoHandCount >= maxHandSize)) return;

        CardData drawnData = DeckManager.Instance?.DrawCard();
        if (drawnData == null) return;

        if (player == Player.PlayerOne) playerOneHandCount++; else playerTwoHandCount++;

        if (pokemonCardPrefab != null)
        {
            Transform hand = (player == Player.PlayerOne) ? playerOneHandArea : playerTwoHandArea;
            GameObject visual = Instantiate(pokemonCardPrefab, hand);
            
            // Get the generic Card component
            Card script = visual.GetComponent<Card>();
            if (script != null)
            {
                script.cardName = drawnData.name;
                
                // Add to the generic list Alex asked for
                if (player == Player.PlayerOne) playerOneHand.Add(script); 
                else playerTwoHand.Add(script);

                // If it happens to be a Pokemon, set those specific stats
                PokemonCard pkmnScript = script as PokemonCard;
                if (pkmnScript != null)
                {
                    pkmnScript.hp = drawnData.hp;
                    pkmnScript.maxHp = drawnData.hp;
                }
            }
        }
    }

    public void playPokemon(GameObject cardObj)
    {
        // Identify the card types
        Card baseCard = cardObj.GetComponent<Card>();
        PokemonCard pkmnCard = cardObj.GetComponent<PokemonCard>();

        // If it's a Pokemon, play it to the appropriate area
        if (pkmnCard != null)
        {
            if (playerOneActivePokemon == null) playerOneActivePokemon = pkmnCard;
            else cardObj.transform.SetParent(playerOneBenchArea);
        }

        // Remove from hand data regardless of type
        if (currentPlayer == Player.PlayerOne) { playerOneHandCount--; playerOneHand.Remove(baseCard); }
        else { playerTwoHandCount--; playerTwoHand.Remove(baseCard); }
        
        RefreshUI();
    }

    // This method handles the attack action when a player chooses to attack with their active Pokemon.
    public void Attack(int skillOrder)
    {
        
        PokemonCard attacker = GetActivePokemon(currentPlayer);
        PokemonCard defender = GetOpponentPokemon(currentPlayer);
        if (attacker != null && defender != null)
        {
            Skill skill = attacker.skills.Find(s => s.skillOrder == skillOrder);
            if (skill != null) attacker.UseSkill(skillOrder, defender);
        }
        RefreshUI();
        if (defender != null && defender.HasFainted()) HandleKnockout(currentPlayer, defender); else EndTurn();
    }

    // This method handles the attachment of energy cards to the active Pokemon.
    public void EndTurn()
    {
        // Reset energy attachment status and switch the current player
        isFirstTurn = false;
        currentPlayer = (currentPlayer == Player.PlayerOne) ? Player.PlayerTwo : Player.PlayerOne;
        BeginTurn();
    }

    // This method initiates the beginning of a player's turn. 
    public void BeginTurn()
    {
        hasAttachedEnergy = false;
        if (!isFirstTurn && drawAtStartOfTurn) drawCard(currentPlayer);
        if (currentPlayer == Player.PlayerTwo) aiController.ExecuteAITurn();
        RefreshUI();
    }
    
    public void AttachEnergy() { RefreshUI(); }
    public void UseAbility() { }
    private void HandleKnockout(Player p, PokemonCard f) { scoreSystem?.AddPoint(p); EndTurn(); }
    public PokemonCard GetActivePokemon(Player p) => (p == Player.PlayerOne) ? playerOneActivePokemon : playerTwoActivePokemon;
    public PokemonCard GetOpponentPokemon(Player p) => (p == Player.PlayerOne) ? playerTwoActivePokemon : playerOneActivePokemon;
    private void RefreshUI() { statsDisplay?.Refresh(); imageDisplay?.Refresh(); }
}