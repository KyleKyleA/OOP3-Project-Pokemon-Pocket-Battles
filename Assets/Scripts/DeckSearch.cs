using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static DeckSearch;

// Author: Kyle Angeles
// File: DeckSearch.cs
// Date-Written: 2026/4/9 
// Description: Deck search is reponsbile for searching player's deck or selection before starting the game
// Theirs a search bar or some sort of UI element that allows players to filter, sort by or search decks on the page 1
// of decks. This is only used at the start screen of the game.
// Declaring a method for PokemonCard
public class pokemonCard
{
    public int cardId;
    public byte[] image;
    public string pokemonName;
    public int hp;
    public string pokemonType;
    public int cardCategoryID;

}

[Serializable]
public class CardListWrapper
{
    // List of decks that will be displayed on the screen and connected by json
    public List<pokemonCard> Cards;
}
public class DeckSearch : MonoBehaviour
{

    // Variable Declaration setting the search field

    public TextField searchField;
    public VisualElement deckListContainer;
    private List<pokemonCard> pokemonCard;

    void Start()
    {
        LoadCardsFromJson();
    }

    // Method to Loading cards from the json file into the decks search.
    void LoadCardsFromJson()
    {
        // This would load the file from our json file 
        TextAsset jsonFile = Resources.Load<TextAsset>("PokemonCards");

        if (jsonFile != null)
        {
            CardListWrapper data = JsonUtility.FromJson<CardListWrapper>(jsonFile.text);
            pokemonCard = data.Cards;

            Debug.Log($"Loaded {pokemonCard.Count} cards from JSON");
        }
        else
        {
            Debug.LogError("Failed to fetch json file");
        }
    }

    /// <summary>
    /// This function incorporates selection of the decks and allows players to click on that 
    /// selected deck and then it will be highlighted and player can start the game with that 
    /// deck that they have selected. This will be only used in the start screen of the game before
    /// they start the game
    /// </summary>
    /// <param name="deck"></param>
    void SelectDeck(pokemonCard deck)
    {
        Debug.Log("Selected Deck: " + deck);


    }
}

