using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class CardLoader : MonoBehaviour
{

    // to ensure only one CardLoader exists and to load the card data from the JSON file
    public static CardLoader Instance;
    public List<CardData> database = new List<CardData>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        LoadJson();
    }

    void LoadJson()
    {
        // loading the JSON file from the StreamingAssets folder and parsing it into the database list
        string path = Path.Combine(Application.streamingAssetsPath, "cards.json");
        if (File.Exists(path))
        {
            string jsonText = File.ReadAllText(path);
            string wrappedJson = "{\"cards\":" + jsonText + "}";
            CardListWrapper wrapper = JsonUtility.FromJson<CardListWrapper>(wrappedJson);

            database = wrapper.cards;
            Debug.Log($"Database Automated: {database.Count} cards loaded.");
        }
        else
        {
            Debug.LogError("Database file not found in StreamingAssets!");
        }
    }

    // retrieves a card from the database by name
    public CardData GetCardByName(string name)
    {
        CardData baseCard = database.Find(c => c.name.ToLower() == name.ToLower());

        if (baseCard != null)
        {
            AssignManualStats(baseCard);
            return baseCard;
        }
        return null;
    }

    
    // adding stats that aren't in the JSON file
    
    private void AssignManualStats(CardData card)
    {
        switch (card.name)
        {
            case "Pikachu":
                card.hp = 60;
                card.attack1 = new Ability
                {
                    skillName = "Gnaw",
                    damage = 30,
                    energyCost = 1,
                    effectDescription = "This Pokemon also does 10 damage to itself"
                };
                break;

            case "Charizard ex":
                card.hp = 180;
                card.attack1 = new Ability
                {
                    skillName = "Slash",
                    damage = 60,
                    energyCost = 3,
                    effectDescription = "None"
                };
                card.attack2 = new Ability
                {
                    skillName = "Crimson Storm",
                    damage = 200,
                    energyCost = 4,
                    effectDescription = "None"
                };
                break;

            case "Bulbasaur":
                card.hp = 70;
                card.attack1 = new Ability
                {
                    skillName = "Vine Whip",
                    damage = 40,
                    energyCost = 2,
                    effectDescription = "None"
                };
                break;
            
            case "Charmander":
                card.hp = 70;
                card.attack1 = new Ability
                {
                    skillName = "Flame Tail",
                    damage = 30,
                    energyCost = 2,
                    effectDescription = "None"
                };
                break;

            case "Squirtle":
                card.hp = 70;
                card.attack1 = new Ability
                {
                    skillName = "Tail Whap",
                    damage = 40,
                    energyCost = 2,
                    effectDescription = "None"
                };
                break;
            
            case "Jigglypuff":
                card.hp = 120;
                card.attack1 = new Ability
                {
                    skillName = "Thunderbolt",
                    damage = 150,
                    energyCost = 3,
                    effectDescription = "None"
                };
                break;

            case "Gyarados ex":
                card.hp = 140;
                card.attack1 = new Ability
                {
                    skillName = "Collapse",
                    damage = 100,
                    energyCost = 4,
                    effectDescription = "None"
                };
                break;

            case "Eevee":
                card.hp = 50;
                card.attack1 = new Ability
                {
                    skillName = "Stampede",
                    damage = 10,
                    energyCost = 1,
                    effectDescription = "None"
                };
                break;

            case "Mewtwo ex":
                card.hp = 150;
                card.attack1 = new Ability
                {
                    skillName = "Psychic Sphere",
                    damage = 50,
                    energyCost = 2,
                    effectDescription = "None"
                };
                    card.attack2 = new Ability
                    {
                        skillName = "Psydrive",
                        damage = 150,
                        energyCost = 4,
                        effectDescription = "None"
                    };
                break;

            case "Mewtwo":
                card.hp = 130;
                card.attack1 = new Ability
                {
                    skillName = "Power Blast",
                    damage = 120,
                    energyCost = 4,
                    effectDescription = "None"
                };
                break;

            case "Pidgey":
                card.hp = 60;
                card.attack1 = new Ability
                {
                    skillName = "Peck",
                    damage = 30,
                    energyCost = 2,
                    effectDescription = "None"
                };
                break;

            case "Rattata":
                card.hp = 70;
                card.attack1 = new Ability
                {
                    skillName = "Shatter",
                    damage = 10,
                    energyCost = 1,
                    effectDescription = "None"
                };
                break;

            case "Hitmonchan":
                card.hp = 80;
                card.attack1 = new Ability
                {
                    skillName = "Jab",
                    damage = 30,
                    energyCost = 1,
                    effectDescription = "None"
                };
                break;

            case "Scyther":
                card.hp = 70;
                card.attack1 = new Ability
                {
                    skillName = "U-turn",
                    damage = 10,
                    energyCost = 1,
                    effectDescription = "None"
                };
                break;

            case "Electabuzz":
                card.hp = 70;
                card.attack1 = new Ability
                {
                    skillName = "Head Bolt",
                    damage = 30,
                    energyCost = 1,
                    effectDescription = "None"
                };
                    break;

            case "Magmar":
                card.hp = 80;
                card.attack1 = new Ability
                {
                    skillName = "Fire Blast",
                    damage = 80,
                    energyCost = 2,
                    effectDescription = "None"
                };
                    break;

            case "Lapras":
                card.hp = 100;
                card.attack1 = new Ability
                {
                    skillName = "Raging Freeze",
                    damage = 60,
                    energyCost = 3,
                    effectDescription = "None"
                };
                        break;

            case "Articuno ex":
                card.hp = 140;
                card.attack1 = new Ability
                {
                    skillName = "Ice Wing",
                    damage = 40,
                    energyCost = 2,
                    effectDescription = "None"
                };
                card.attack2 = new Ability
                {
                     skillName = "Blizzard ",
                     damage = 80,
                     energyCost = 3,
                     effectDescription = "None"
                };
                break;

            case "Zapdos ex":
                card.hp = 130;
                card.attack1 = new Ability
                {
                    skillName = "Peck",
                    damage = 20,
                    energyCost = 1,
                    effectDescription = "None"
                };
                card.attack2 = new Ability
                {
                    skillName = "Thundering Hurricane",
                    damage = 50,
                    energyCost = 3,
                    effectDescription = "None"
                };
                break;

            case "Moltres":
                card.hp = 100;
                card.attack1 = new Ability
                {
                    skillName = "Sky Attack",
                    damage = 130,
                    energyCost = 3,
                    effectDescription = "None"
                };
                break;





        }
    }
}