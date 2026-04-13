using UnityEngine;

// Author: Alex Tan
// File: Card.cs
// Date-Written: Alex Tan
// Description: Abstract base class for all cards in the game.
public abstract class Card : MonoBehaviour
{
    public enum CardCategory { Pokemon, Trainer, Energy }

    public int cardID;
    public int imageID;
    public int typeID;
    public CardCategory cardCategory;
    public string cardName;
    public int hp;
    public int retreatCost;
    public string cardText;
    public int evolvesFromCardID;
}
