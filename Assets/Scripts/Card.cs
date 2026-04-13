using UnityEngine;

// Author: Alex Tan
// File: Card.cs
// Date-Written: April 13th 2026
// Description: Abstract base class for all cards in the game.
public abstract class Card : MonoBehaviour
{
    public int cardID;
    public int imageID;
    public int typeID;
    public string cardName;
    public int hp;
    public int retreatCost;
    public string cardText;
    public int evolvesFromCardID;

    public enum CardCategory { Pokemon, Trainer }
    public CardCategory cardCategory;
}
