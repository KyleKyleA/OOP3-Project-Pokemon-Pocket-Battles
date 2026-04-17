using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Kyle Angeles, D'Andre, Karim, Alex
public class DeckManager : MonoBehaviour
{
    public static DeckManager Instance;

    // gameplay variables
    public List<CardData> activeDeck = new List<CardData>();
    public int maxDeckSize = 30;

    // UI variables
    public bool mouseOverDeck;
    public int dragged;
    public GameObject coll;
    public GameObject prefab;
    public int lastAdded;

    // to hold the selected deck for the deck search UI
    private DeckSearch.pokemonCard selectedDeck;

    // singleton to ensure only one DeckManager exists 
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

    // gameplay logic for shuffling and drawing cards
    public void ShuffleDeck()
    {
        for (int i = 0; i < activeDeck.Count; i++)
        {
            CardData temp = activeDeck[i];
            int randomIndex = Random.Range(i, activeDeck.Count);
            activeDeck[i] = activeDeck[randomIndex];
            activeDeck[randomIndex] = temp;
        }
        Debug.Log("Gameplay Deck Shuffled.");
    }

    // this will be called by the TurnSystem when a player draws a card
    public CardData DrawCard()
    {
        if (activeDeck.Count > 0)
        {
            CardData drawnCard = activeDeck[0];
            activeDeck.RemoveAt(0);
            return drawnCard;
        }
        Debug.Log("Deck Out! Player loses.");
        return null;
    }

    // UI
    public void addDeck() { mouseOverDeck = true; }
    public void Exit() { mouseOverDeck = false; }
    public void Card1() { dragged = 0; }
    public void Card2() { dragged = 1; }
    public void Card3() { dragged = 2; }

    // this will be called when the card is dropped onto the deck area
    public void drop(string droppedCardName)
    {
        if (mouseOverDeck && activeDeck.Count < maxDeckSize)
        {
            CardData cardFromDatabase = CardLoader.Instance.GetCardByName(droppedCardName);

            if (cardFromDatabase != null)
            {
                activeDeck.Add(cardFromDatabase);
                CalculateDrop();
            }
        }
    }

    // this is where we will implement the animation for dropping a card onto the deck area
    public void CalculateDrop()
    {
        Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    
    // deck search
    
    public void SetSelectedDeck(DeckSearch.pokemonCard card)
    {
        selectedDeck = card;
    }

    public DeckSearch.pokemonCard GetSelectedDeck()
    {
        return selectedDeck;
    }
}