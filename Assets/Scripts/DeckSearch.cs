using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
// Author: Kyle Angeles
// File: DeckSearch.cs
// Date-Written: 2026/4/9 
// Description: Deck search is reponsbile for searching player's deck or selection before starting the game
// Theirs a search bar or some sort of UI element that allows players to filter, sort by or search decks on the page 1
// of decks. This is only used at the start screen of the game.
// Declaring a method for PokemonCard
public class DeckSearch : MonoBehaviour
{

    // Variable Declaration setting the search field

    public TextField searchField;
    public VisualElement deckListContainer;
   
    private List<pokemonCard> cardList;
    private pokemonCard selectedCard = null;
    private Button confirmBtn;


    [Serializable]
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
        public List<pokemonCard> cards;
    }

    void Start()
    {
        LoadCardsFromJson();
    }

    // Method to Loading cards from the json file into the decks search.
    void LoadCardsFromJson()
    {
        // This would load the file from our json file 
        TextAsset jsonFile = Resources.Load<TextAsset>("PokemonCards");

        if (jsonFile == null)
        {
            Debug.LogError($"Loaded {cardList.Count} cards from json");
            return;
               
        }

        CardListWrapper data = JsonUtility.FromJson<CardListWrapper>(jsonFile.text);
        cardList = data.cards;
        Debug.Log("Failed to fetch json file");


        var root = GetComponent<UIDocument>().rootVisualElement;


        // Confirmed Selected deck before the game
        confirmBtn = root.Q<Button>("confirm-button");
        confirmBtn.SetEnabled(false);
        confirmBtn.clicked += () => {

            if (selectedCard == null) return;
            SceneManager.LoadScene("MainMenu");
        };


        searchField = root.Q<TextField>("search-field");
        searchField.RegisterValueChangedCallback(evt => filterDecks(evt.newValue));


        deckListContainer = root.Q<VisualElement>("deck-list-container");

        PopulateDeckList(cardList);
       
    }

    // Populate decjs
       void PopulateDeckList(List<pokemonCard> cards)
    {
        deckListContainer.Clear();

        foreach (var card in cards)
        {
            var btn = new Button();
            btn.AddToClassList("deck-card");

            btn.Add(new Label(card.pokemonName));
            btn.Add(new Label(card.pokemonType));
            btn.Add(new Label("HP: " + card.hp));

            var capturedCard = card;
            var capturedBtn = btn;
            btn.clicked += () => SelectDeck(capturedCard, capturedBtn);

            deckListContainer.Add(btn);

        }
    }

    /// <summary>
    /// This function incorporates selection of the decks and allows players to click on that 
    /// selected deck and then it will be highlighted and player can start the game with that 
    /// deck that they have selected. This will be only used in the start screen of the game before
    /// they start the game
    /// </summary>
    /// <param name="deck"></param>
    void SelectDeck(pokemonCard card, Button btn)
    {

        selectedCard = card;

        // Remove highlight from all cards
        deckListContainer.Query<Button>(className: "deck-card").ForEach(b =>
        b.RemoveFromClassList("deck-card--selected"));

        // Highlight the selected deck
        btn.AddToClassList("deck-card--selected");

       
        // Save to DeckManager so it survives the scene change
        DeckManager.Instance.SetSelectedDeck(card);

        confirmBtn.SetEnabled(true);

        Debug.Log("Deck selected: " + card.pokemonName);


    }


    // Search bar - Filter
    void filterDecks(string query)
    {
        var filtered = string.IsNullOrEmpty(query)
            ? cardList
            : cardList.FindAll(c =>
                c.pokemonName.ToLower().Contains(query.ToLower()) ||
                c.pokemonType.ToLower().Contains(query.ToLower()));

        PopulateDeckList(filtered);
    }
}

