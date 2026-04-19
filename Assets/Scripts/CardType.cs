// Author: Alex Tan
// File: CardType.cs
// Date-Written: April 13th 2026
// Description: Represents a card's elemental type (e.g. Fire, Water, Grass).
// Matches the Types table in the ERD.
[System.Serializable]
public class CardType
{
    public int typeID;
    public string typeName; // e.g. "Fire", "Water", "Grass", "Electric"
}

// Matches the TypeEffectiveness table in the ERD.
// Stores the damage modifier when one type attacks another.
[System.Serializable]
public class TypeEffectiveness
{
    public int attackingTypeID;
    public int defendingTypeID;
    public float modifier; // e.g. +20 for super effect attacks
}
