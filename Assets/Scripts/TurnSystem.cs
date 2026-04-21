using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static TurnSystem;

// Author: Kyle Angeles, Alex Tan
// File: TurnSystem.cs
// Date-Written: 2026/4/9
// Description: Handles a turn-by-turn battle loop 
public class TurnSystem : MonoBehaviour
{
    public enum Player { PlayerOne, PlayerTwo }
    public enum GameState { PlayerOneTurn, PlayerTwoTurn, Attack, GameOver }


    [Header("Game State")]
    public GameState currentState;
    public Player currentPlayer;

    [Header("Battle Setup")]
    public PokemonCard playerOneActivePokemon;
    public PokemonCard playerTwoActivePokemon;
    public AI aiController;
    public ScoreSystem scoreSystem;

    [Header("Starting Decks")]
    public List<Card> playerOneStartingDeck = new List<Card>();
    public List<Card> playerTwoStartingDeck = new List<Card>();

    [Header("Runtime Zones")]
    public List<Card> playerOneDeck = new List<Card>();
    public List<Card> playerTwoDeck = new List<Card>();
    public List<Card> playerOneHand = new List<Card>();
    public List<Card> playerTwoHand = new List<Card>();
    public List<PokemonCard> playerOneBench = new List<PokemonCard>();
    public List<PokemonCard> playerTwoBench = new List<PokemonCard>();


    // Hand count and max size
    [Header("Hand / Deck Test Values")]
    public int playerOneHandCount = 0;
    public int playerTwoHandCount = 0;
    public int startingHandSize = 5;
    public int maxHandSize = 10;
    public int maxBenchSize = 3;

    // Testing purposes
    [Header("Testing Options")]
    public bool drawAtStartOfTurn = true;
    public CardType defaultEnergyType;

    // Once per turn action flags
    private bool hasAttachedEnergy = false;
    private bool hasPlayedSupporterCard = false;
    private bool hasEvolvedPokemon = false;
    private bool hasUsedAbility = false;
    private bool hasRetreatedPokemon = false;

    // Is this the first turn?
    private bool isFirstTurn = true;

    // UI Displays
    public PokemonImageDisplayUI imageDisplay;
    public PokemonStatsDisplayUI statsDisplay;
    public BenchDisplayUI benchDisplay;

    // UI Flags
    // Have I played an energy this turn?
    public bool CanAttachEnergy() => !hasAttachedEnergy && currentState != GameState.GameOver;
    // Have I played a trainer this turn?
    public bool CanPlayTrainer() => !hasPlayedSupporterCard && currentState != GameState.GameOver;
    // Can I play an active pokemon?
    public bool CanPlayActivePokemon(Player player)
    {
        PokemonCard active = player == Player.PlayerOne ? playerOneActivePokemon : playerTwoActivePokemon;
        return active == null;
    }
    // Can I bench a pokemon?
    public bool CanBenchPokemon(Player player)
    {
        List<PokemonCard> bench = player == Player.PlayerOne ? playerOneBench : playerTwoBench;
        return bench.Count < maxBenchSize;
    }

    /// <summary>
    /// Initialize game start
    /// </summary>
    void Start()
    {
        if (scoreSystem == null)
            scoreSystem = ScoreSystem.instance;

        currentPlayer = Player.PlayerOne;
        currentState = GameState.PlayerOneTurn;

        DrawStartingHand(Player.PlayerOne);
        DrawStartingHand(Player.PlayerTwo);

        InitializePokemonState(playerOneActivePokemon);
        InitializePokemonState(playerTwoActivePokemon);

        DebugBattleState();
        BeginTurn();

        //Temporary
        RefreshUI();
    }

    public void DrawStartingHand(Player player)
    {
        for (int i = 0; i < startingHandSize; i++)
            drawCard(player);
    }

    /// <summary>
    /// drawing card method this would determine player draws a card and adds it to the players hand
    /// call this at the start of each turn 
    /// </summary>
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
        else
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
    /// Tries to play selected basic pokemon card on active slot, then bench.
    /// </summary>
    public bool PlayPokemon(Player player, PokemonCard pokemon)
    {
        List<Card> hand = player == Player.PlayerOne ? playerOneHand : playerTwoHand;
        List<PokemonCard> bench = player == Player.PlayerOne ? playerOneBench : playerTwoBench;
        PokemonCard active = player == Player.PlayerOne ? playerOneActivePokemon : playerTwoActivePokemon;

        // No active Pokémon yet -> set as active
        if (CanPlayActivePokemon(player))
        {
            if (player == Player.PlayerOne)
                playerOneActivePokemon = pokemon;
            else
                playerTwoActivePokemon = pokemon;

            Debug.Log(player + " set active Pokémon: " + pokemon.cardName);
            RefreshUI();
            return true;
        }

        // Active already exists -> try to bench it
        if (!CanBenchPokemon(player))
        {
            Debug.LogWarning(player + "'s bench is full.");
            return false;
        }

        hand.Remove(pokemon);
        bench.Add(pokemon);
        Debug.Log(player + " benched " + pokemon.cardName);

        RefreshUI();
        benchDisplay?.Refresh();
        return true;
    }

    /// <summary>
    /// Each Turn a player must attach an energy to one of their bench or played pokemon card
    /// </summary>
    public void AttachEnergy()
    {
        if (currentState == GameState.GameOver)
            return;

        if (!CanAttachEnergy())
        {
            Debug.Log("Energy has already been attached this turn.");
            return;
        }

        PokemonCard active = GetActivePokemon(currentPlayer);
        if (active == null)
        {
            Debug.LogWarning("No active Pokemon found for " + currentPlayer);
            return;
        }

        active.attachedEnergy.Add(defaultEnergyType);
        hasAttachedEnergy = true;

        Debug.Log(currentPlayer + " attached an energy to " + active.cardName + ". Total attached: " + active.attachedEnergy.Count);
        RefreshUI();
    }
    // Active pokemon uses ability
    public void UseAbility()
    {
        if (currentState == GameState.GameOver)
            return;

        PokemonCard user = GetActivePokemon(currentPlayer);
        PokemonCard target = GetOpponentPokemon(currentPlayer);

        if (user == null)
        {
            Debug.LogWarning("No active Pokemon available to use an ability.");
            return;
        }

        if (isFirstTurn && currentPlayer == Player.PlayerOne)
        {
            Debug.Log("Player One cannot use an ability on the opening turn in this test setup.");
            return;
        }

        if (user.abilityEffects == null || user.abilityEffects.Count == 0)
        {
            Debug.Log(user.cardName + " has no ability effects configured.");
            return;
        }

        user.UseAbility(target, this);
        hasUsedAbility = true;
    }
    /// <summary>
    /// Active players turn can use active pokemon to Attack if cost is met.
    /// </summary>
    public void Attack(int skillOrder)
    {
        if (currentState == GameState.GameOver)
        {
            Debug.Log("Game is over. No more attacks can be made.");
            return;
        }

        PokemonCard attacker = GetActivePokemon(currentPlayer);
        PokemonCard defender = GetOpponentPokemon(currentPlayer);

        Skill skill = attacker.skills.Find(s => s.skillOrder == skillOrder);

        if (attacker == null || defender == null)
        {
            Debug.LogWarning("Attack failed because one side has no active Pokemon assigned.");
            return;
        }

        if (attacker.HasFainted())
        {
            Debug.LogWarning(attacker.cardName + " has fainted and cannot attack.");
            return;
        }

        // Check if skill is usable
        if (!attacker.CanUseSkill(skillOrder))
        {
            Debug.Log(attacker.cardName + " doesn't have enough energy to use " + skill.skillName);
            return;
        }

        currentState = GameState.Attack;

        // Use the selected skill
        attacker.UseSkill(skillOrder, defender);

        RefreshUI();
        benchDisplay?.Refresh();

        if (defender.HasFainted())
        {
            HandleKnockout(currentPlayer, defender);
            return;
        }

        // End turn after attack
        EndTurn();
        DebugBattleState();
    }
    /// <summary>
    /// Player ended their turn by choice or attacking.
    /// </summary>
    public void EndTurn()
    {
        if (currentState == GameState.GameOver)
            return;

        currentPlayer = currentPlayer == Player.PlayerOne ? Player.PlayerTwo : Player.PlayerOne;
        currentState = currentPlayer == Player.PlayerOne ? GameState.PlayerOneTurn : GameState.PlayerTwoTurn;

        if (isFirstTurn)
            isFirstTurn = false;

        Debug.Log(currentPlayer + "'s turn");
        BeginTurn();
    }

    public void BeginTurn()
    {
        if (currentState == GameState.GameOver)
            return;

        ResetPerTurnFlagsForCurrentPlayer();

        if (drawAtStartOfTurn)
        {
            bool shouldDraw = !isFirstTurn;
            if (shouldDraw)
                drawCard(currentPlayer);
        }

        DebugBattleState();

        // If it's AI's turn, let AI play.
        if (currentPlayer == Player.PlayerTwo)
            aiController.ExecuteAITurn();
    }

    public PokemonCard GetActivePokemon(Player player)
    {
        return player == Player.PlayerOne ? playerOneActivePokemon : playerTwoActivePokemon;
    }

    public PokemonCard GetOpponentPokemon(Player player)
    {
        return player == Player.PlayerOne ? playerTwoActivePokemon : playerOneActivePokemon;
    }

    private void InitializePokemonState(PokemonCard pokemon)
    {
        if (pokemon == null)
            return;

        pokemon.hp = pokemon.maxHp;
        pokemon.currentStatus = StatusEffect.Status.None;

        if (pokemon.attachedEnergy == null)
            pokemon.attachedEnergy = new System.Collections.Generic.List<CardType>();
        else
            pokemon.attachedEnergy.Clear();
    }
    /// <summary>
    /// Resets all once per turn action flags
    /// </summary>
    private void ResetPerTurnFlagsForCurrentPlayer()
    {
        hasAttachedEnergy = false;
        hasPlayedSupporterCard = false;
        hasEvolvedPokemon = false;
        hasUsedAbility = false;
        hasRetreatedPokemon = false;
    }

    private void HandleKnockout(Player attackingPlayer, PokemonCard faintedPokemon)
    {
        Debug.Log(faintedPokemon.cardName + " was knocked out by " + attackingPlayer);

        ScoreSystem scoring = scoreSystem != null ? scoreSystem : ScoreSystem.instance;
        if (scoring == null)
        {
            Debug.LogWarning("No ScoreSystem found. Ending test battle.");
            currentState = GameState.GameOver;
            return;
        }

        // Add point to attacking player
        scoring.AddPoint(attackingPlayer);

        // If a player has reached the winning amount, game ends
        if (scoring.HasWinner())
        {
            currentState = GameState.GameOver;
            scoring.winCondition();
            return;
        }

        // Until bench / replacement is implemented, end the test battle after a knockout.
        currentState = GameState.GameOver;
        Debug.Log("Point awarded through ScoreSystem. Battle ended because replacement Pokemon is not implemented yet.");
    }

    public void DebugBattleState()
    {
        string p1 = playerOneActivePokemon == null
            ? "None"
            : $"{playerOneActivePokemon.cardName} HP {playerOneActivePokemon.hp}/{playerOneActivePokemon.maxHp} Energy {playerOneActivePokemon.attachedEnergy.Count}";

        string p2 = playerTwoActivePokemon == null
            ? "None"
            : $"{playerTwoActivePokemon.cardName} HP {playerTwoActivePokemon.hp}/{playerTwoActivePokemon.maxHp} Energy {playerTwoActivePokemon.attachedEnergy.Count}";

        Debug.Log($"Battle State -> Current: {currentPlayer} | P1: {p1} | P2: {p2}");
    }

    private void RefreshUI()
    {
        statsDisplay?.Refresh();
        imageDisplay?.Refresh();
    }
}
