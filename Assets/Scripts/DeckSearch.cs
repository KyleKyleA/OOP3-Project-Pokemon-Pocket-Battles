using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

// Author: Kyle Angeles / Karim
// Description: Handles the UI Toolkit search bar and deck selection in the Main Menu.
public class DeckSearch : MonoBehaviour
{

    public TextField searchField;
    public VisualElement deckListContainer;

    private List<pokemonCard> cardList;
    private pokemonCard selectedCard = null;
    private Button confirmBtn;

    [Serializable]
    // this class represents the structure of the card data we will load from JSON for the search UI. It includes basic info like name, type, HP, and an image byte array for potential future use in displaying card images in the UI.
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
    // Wrapper class for JSON deserialization of the card list. This allows us to load a list of pokemonCard objects from a JSON file
    public class CardListWrapper { public List<pokemonCard> cards; }

    void Start()
    {
        LoadCardsFromJson();
    }

    void LoadCardsFromJson()
    {
        // loading the JSON file from the resources folder
        TextAsset jsonFile = Resources.Load<TextAsset>("PokemonCards");

        if (jsonFile == null)
        {
            Debug.LogError("Failed to fetch JSON file from Resources! Check the file name.");
            return;
        }

        // deserializing the JSON data into a list of pokemonCard objects
        CardListWrapper data = JsonUtility.FromJson<CardListWrapper>(jsonFile.text);
        cardList = data.cards;
        
        Debug.Log($"Successfully loaded {cardList.Count} cards for the search UI.");

        var root = GetComponent<UIDocument>().rootVisualElement;

        // setting up the confirm button to be disabled until a deck is selected, and adding a click event to load the main menu scene when clicked
        confirmBtn = root.Q<Button>("confirm-button");
        confirmBtn.SetEnabled(false);
        confirmBtn.clicked += () => {
            if (selectedCard != null) SceneManager.LoadScene("MainMenu");
        };

        // setting up the search field to filter the deck list as the user types, and populating the initial deck list with all cards
        searchField = root.Q<TextField>("search-field");
        searchField.RegisterValueChangedCallback(evt => filterDecks(evt.newValue));

        deckListContainer = root.Q<VisualElement>("deck-list-container");

        PopulateDeckList(cardList);
    }

    // This method populates the deck list UI with buttons for each card. Each button displays the card's name, type, and HP, and has a click event that selects the card and updates the DeckManager singleton with the selected deck.
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

    // This method handles the selection of a deck when a card button is clicked.
    void SelectDeck(pokemonCard card, Button btn)
    {
        selectedCard = card;

        // Visual feedback for selection
        deckListContainer.Query<Button>(className: "deck-card").ForEach(b =>
            b.RemoveFromClassList("deck-card--selected"));
        btn.AddToClassList("deck-card--selected");

        // Sync with DeckManager singleton
        DeckManager.Instance.SetSelectedDeck(card);
        confirmBtn.SetEnabled(true);

        Debug.Log("Deck selected: " + card.pokemonName);

        // Add the selection to the active deck list
        AddCardToActiveDeck(card.pokemonName);
    }

    // this will filter the deck list based on the user's search 
    void filterDecks(string query)
    {
        var filtered = string.IsNullOrEmpty(query)
            ? cardList
            : cardList.FindAll(c =>
                c.pokemonName.ToLower().Contains(query.ToLower()) ||
                c.pokemonType.ToLower().Contains(query.ToLower()));

        PopulateDeckList(filtered);
    }

    // this will add the selected card to the active deck list in the DeckManager singleton
    public void AddCardToActiveDeck(string clickedCardName)
    {
        CardData cardStats = CardLoader.Instance.GetCardByName(clickedCardName);

        if (cardStats != null)
        {
            if (DeckManager.Instance.activeDeck.Count < DeckManager.Instance.maxDeckSize)
            {
                DeckManager.Instance.activeDeck.Add(cardStats);
                Debug.Log($"Added {cardStats.name} to Active Deck. Count: {DeckManager.Instance.activeDeck.Count}/30");
            }
            else
            {
                Debug.Log("Deck is full (30/30)!");
            }
        }
    }
}