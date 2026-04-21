using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

// Author: Kyle Angeles, D'Andre, Karim, Alex
// Date-Written 2026/4/8
// File: DeckManager.cs
// Description: Deck Manager is responsible for managing player's deck's in the decks screen
// This includes adding cards to the deck, deleting decks, saving decks
// Responsible for selecting decks on the second page of the deck screen and loading them into the game when the 
// player clicks start

// We need JSON Connectivity for this script to save and load decks from the game into the player's screen
public class DeckManager : MonoBehaviour
{
    public static DeckManager Instance;
    private DeckSearch.pokemonCard selectedDeck;

    // This is the player's active deck that will be used in the game. It will be populated with the cards that the player has selected in the deck screen and will be used to draw cards from during the game.
    public List<CardData> activeDeck = new List<CardData>();
    public int maxDeckSize = 30;

    // Deck Settings
    public int[] cardId;
    public int[] quantity;
    public bool[] alreadyCreated;
    public int[] saveDeck;

    // State of Decks
    public bool mouseOverDeck;
    public int dragged;
    public GameObject coll;
    public GameObject prefab;

    // Different cards in each deck
    public int numberOfDifferentCards;

    public int numberOfDifferentDecks;

    // Number of decks and update lastAdded deck
    public int sum;

    public int numberOfCardsInJson = 30;

    public int lastAdded;

    // This will be responsible for creating a new deck and saving it to the player's screen.
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        sum = 0;
        cardId = new int[100];
        quantity = new int[100];
        alreadyCreated = new bool[100];
        saveDeck = new int[numberOfCardsInJson];
    }

    void Update()
    {

    }

    
    public void CreateDeck()
    {
        // This function will be responsible for creating a new deck and saving it to the player's screen. 
        for (int i = 0; i < quantity.Length; i++)
        {
            sum += quantity[i];
        }

        if (sum == 30)
        {
            for (int i = 0; i < numberOfCardsInJson; i++)
            {
                PlayerPrefs.SetInt("deck" + i, cardId[i]);
            }
            PlayerPrefs.Save();
            Debug.Log("Deck Saved");
        }

        sum = 0;
        numberOfDifferentCards = 0;

        for (int i = 0; i < numberOfCardsInJson; i++)
        {
            saveDeck[i] = PlayerPrefs.GetInt("deck" + i, 0);
        }
    }

    /// <summary>
    /// This function will be responsible for selecting a deck from the player's screen and saving it to the player's screen
    /// <summmary>
    public void addDeck()
    {
        mouseOverDeck = true;
    }

    // This is for exiting the deck screen and going back to the main menu
    public void Exit()
    {
        mouseOverDeck = false;
    }

    // this will be for populating the deck list 
    public void Card1()
    {
        dragged = 0;
    }


    public void Card2()
    {
        dragged = 1;
    }

    public void Card3()
    {
        dragged = 2;
    }


    // this is for dropping the card into the deck and saving it to the player's screen 
    public void drop()
    {
        Collection collection = coll.GetComponent<Collection>();
        if (mouseOverDeck == true && coll.GetComponent<Collection>().HowManyCards[dragged] > 0)
        {
            cardId[dragged]++;

            if (cardId[dragged] > 4)
            {
                cardId[dragged] = 4;
            }

            if (cardId[dragged] < 0)
            {
                cardId[dragged] = 0;
            }

            coll.GetComponent<Collection>().HowManyCards[dragged]--;

            CalculateDrop();
        }
    }

    // calculates the drop of the card into the deck and saves it to the player's screen
    public void CalculateDrop()
    {
        lastAdded = 0;
        int i = dragged;

        if (cardId[i] > 0 && alreadyCreated[i] == false)
        {
            lastAdded = i;
            Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
            alreadyCreated[i] = true;

            quantity[i] = 1;

        }
        else if (cardId[i] > 0 && alreadyCreated[i] == true)
        {
            quantity[i]++;
        }
    }

    // selecting a deck from the player's screen and saving it to the player's screen 
    public void SetSelectedDeck(DeckSearch.pokemonCard card)
    {
        selectedDeck = card;
    }

    public DeckSearch.pokemonCard GetSelectedDeck()
    {
        return selectedDeck;
    }

    /// <summary>
    /// Pulls the top card from the player's active deck and removes it from the list.
    /// </summary>
    public CardData DrawCard()
    {
        // Check if there are actually cards left in the deck
        if (activeDeck.Count > 0)
        {
            // Get the card at the top of the deck
            CardData drawnCard = activeDeck[0];
            
            // Remove it from the deck so we don't draw the exact same card again
            activeDeck.RemoveAt(0); 
            
            return drawnCard;
        }
        
        // If the deck is empty, return nothing
        return null; 
    }
}