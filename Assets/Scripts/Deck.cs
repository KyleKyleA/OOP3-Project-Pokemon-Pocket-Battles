using System.Collections.Generic;
using UnityEngine;

// Author: Kyle Angeles
// File: Deck.cs
// Date-Written: 2026/4/9
// Description: Represents a player's deck. Matches the Decks table in the ERD.
// DeckCard is the junction class matching the DeckCards table.
public class Deck : MonoBehaviour
{
    public int deckID;
    public string deckName;
    public List<DeckCard> deckCards = new List<DeckCard>();

    public const int MaxDeckSize = 20; // Pocket TCG uses 20-card decks

    /// <summary>
    /// Returns the total number of cards across all DeckCard entries
    /// </summary>
    public int TotalCardCount()
    {
        int total = 0;
        foreach (var dc in deckCards) total += dc.quantity;
        return total;
    }

    /// <summary>
    /// Returns true if the deck is valid (exactly MaxDeckSize cards)
    /// </summary>
    public bool IsValid() => TotalCardCount() == MaxDeckSize;
}

// Matches the DeckCards junction table in the ERD.
[System.Serializable]
public class DeckCard
{
    public int deckID;
    public int cardID;
    public int quantity; // Max 2 copies of any card in Pocket TCG
}
