using System;
using System.Collections.Generic;

[Serializable]
public class CardData
{
    // stats from the JSON file
    public string set;
    public int number;      
    public string rarity;
    public string name;
    public string image;
    public string[] packs;
    // manual stats to be implemented
    public int hp;
    public Ability attack1;
    public Ability attack2;
}

// to read a flat json array
[Serializable]
public class CardListWrapper
{
    public List<CardData> cards;
}