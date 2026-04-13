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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sum = 0;
        cardId = new int[100];
        quantity = new int[100];
        alreadyCreated = new bool[100];
        saveDeck = new int[numberOfCardsInJson];
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// This allows the user to create decks and adding in cards
    /// </summary>
    public void CreateDeck()
    {
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
    /// Function to add deck button
    /// this would allow the user to add/creat decks
    /// </summary>
    public void addDeck()
    {
        mouseOverDeck = true;
    }

    public void Exit()
    {
        mouseOverDeck = false;
    }

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


    // Drop or deleting a card
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


    /// <summary>
    /// Function 
    /// </summary>
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

    public void SetSelectedDeck(DeckSearch.pokemonCard card)
    {
        selectedDeck = card;
    }

    public DeckSearch.pokemonCard GetSelectedDeck()
    {
        return selectedDeck;
    }
}