using System;
using System.Collections.Generic;

// Author: Karim
// Description: CardData class to represent the structure of card information loaded from JSON. This includes both the basic card info and battle stats.
[Serializable]
public class CardData
{
    // Stats from the JSON file
    public string set;
    public int number;      
    public string rarity;
    public string name;
    public string image;
    public string[] packs;

    // Battle Stats
    public int hp;
    public int maxHp; 
    public Skill attack1;
    public Skill attack2; 
}

[Serializable]
public class CardListWrapper
{
    public List<CardData> cards;
}