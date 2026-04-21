using UnityEngine;
using System.IO;
using System.Collections.Generic;

// author: Karim
// description: CardLoader is responsible for loading card data from a JSON file ù

public class CardLoader : MonoBehaviour
{
    // Singleton instance to allow easy access to the card database from other scripts
    public static CardLoader Instance;
    public List<CardData> database = new List<CardData>();

    [System.Serializable]
    public class CardListWrapper { public List<CardData> cards; }

    // Ensures that there is only one instance of CardLoader and loads the card data from the JSON file when the game starts.
    void Awake()
    {
        if (Instance == null) Instance = this;
        LoadJson();
    }

    // loads the card data from a JSON file
    void LoadJson()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "cards.json");
        if (File.Exists(path))
        {
            string jsonText = File.ReadAllText(path);
            string wrappedJson = "{\"cards\":" + jsonText + "}";
            CardListWrapper wrapper = JsonUtility.FromJson<CardListWrapper>(wrappedJson);
            database = wrapper.cards;
            Debug.Log($"Database Automated: {database.Count} cards loaded.");
        }
    }

    // gets a card from the database by its name 
    public CardData GetCardByName(string name)
    {
        CardData baseCard = database.Find(c => c.name.ToLower() == name.ToLower());
        if (baseCard != null) { AssignManualStats(baseCard); return baseCard; }
        return null;
    }

    // assigning stats to cards manually since we are not loading them from the JSON
    private void AssignManualStats(CardData card)
    {
        switch (card.name)
        {
            case "Pikachu":
                card.hp = 60;
                card.maxHp = 60; // Added for UI Sync
                card.attack1 = new Skill { skillName = "Gnaw", damage = 30, energyCost = 1, skillOrder = 1, description = "Self-damage 10." };
                break;
            case "Charizard ex":
                card.hp = 180;
                card.maxHp = 180;
                card.attack1 = new Skill { skillName = "Slash", damage = 60, energyCost = 3, skillOrder = 1, description = "None" };
                card.attack2 = new Skill { skillName = "Crimson Storm", damage = 200, energyCost = 4, skillOrder = 2, description = "None" };
                break;
            case "Bulbasaur":
                card.hp = 70;
                card.maxHp = 70;
                card.attack1 = new Skill { skillName = "Vine Whip", damage = 40, energyCost = 2, skillOrder = 1, description = "None" };
                break;
            case "Charmander":
                card.hp = 70;
                card.maxHp = 70;
                card.attack1 = new Skill { skillName = "Flame Tail", damage = 30, energyCost = 2, skillOrder = 1, description = "None" };
                break;
            case "Squirtle":
                card.hp = 70;
                card.maxHp = 70;
                card.attack1 = new Skill { skillName = "Tail Whap", damage = 40, energyCost = 2, skillOrder = 1, description = "None" };
                break;
            case "Jigglypuff":
                card.hp = 120;
                card.maxHp = 120;
                card.attack1 = new Skill { skillName = "Thunderbolt", damage = 150, energyCost = 3, skillOrder = 1, description = "None" };
                break;
            case "Gyarados ex":
                card.hp = 140;
                card.maxHp = 140;
                card.attack1 = new Skill { skillName = "Collapse", damage = 100, energyCost = 4, skillOrder = 1, description = "None" };
                break;
            case "Eevee":
                card.hp = 50;
                card.maxHp = 50;
                card.attack1 = new Skill { skillName = "Stampede", damage = 10, energyCost = 1, skillOrder = 1, description = "None" };
                break;
            case "Mewtwo ex":
                card.hp = 150;
                card.maxHp = 150;
                card.attack1 = new Skill { skillName = "Psychic Sphere", damage = 50, energyCost = 2, skillOrder = 1, description = "None" };
                card.attack2 = new Skill { skillName = "Psydrive", damage = 150, energyCost = 4, skillOrder = 2, description = "None" };
                break;
            case "Mewtwo":
                card.hp = 130;
                card.maxHp = 130;
                card.attack1 = new Skill { skillName = "Power Blast", damage = 120, energyCost = 4, skillOrder = 1, description = "None" };
                break;
            case "Pidgey":
                card.hp = 60;
                card.maxHp = 60;
                card.attack1 = new Skill { skillName = "Peck", damage = 30, energyCost = 2, skillOrder = 1, description = "None" };
                break;
            case "Rattata":
                card.hp = 70;
                card.maxHp = 70;
                card.attack1 = new Skill { skillName = "Shatter", damage = 10, energyCost = 1, skillOrder = 1, description = "None" };
                break;
            case "Hitmonchan":
                card.hp = 80;
                card.maxHp = 80;
                card.attack1 = new Skill { skillName = "Jab", damage = 30, energyCost = 1, skillOrder = 1, description = "None" };
                break;
            case "Scyther":
                card.hp = 70;
                card.maxHp = 70;
                card.attack1 = new Skill { skillName = "U-turn", damage = 10, energyCost = 1, skillOrder = 1, description = "None" };
                break;
            case "Electabuzz":
                card.hp = 70;
                card.maxHp = 70;
                card.attack1 = new Skill { skillName = "Head Bolt", damage = 30, energyCost = 1, skillOrder = 1, description = "None" };
                break;
            case "Magmar":
                card.hp = 80;
                card.maxHp = 80;
                card.attack1 = new Skill { skillName = "Fire Blast", damage = 80, energyCost = 2, skillOrder = 1, description = "None" };
                break;
            case "Lapras":
                card.hp = 100;
                card.maxHp = 100;
                card.attack1 = new Skill { skillName = "Raging Freeze", damage = 60, energyCost = 3, skillOrder = 1, description = "None" };
                break;
            case "Articuno ex":
                card.hp = 140;
                card.maxHp = 140;
                card.attack1 = new Skill { skillName = "Ice Wing", damage = 40, energyCost = 2, skillOrder = 1, description = "None" };
                card.attack2 = new Skill { skillName = "Blizzard ", damage = 80, energyCost = 3, skillOrder = 2, description = "None" };
                break;
            case "Zapdos ex":
                card.hp = 130;
                card.maxHp = 130;
                card.attack1 = new Skill { skillName = "Peck", damage = 20, energyCost = 1, skillOrder = 1, description = "None" };
                card.attack2 = new Skill { skillName = "Thundering Hurricane", damage = 50, energyCost = 3, skillOrder = 2, description = "None" };
                break;
            case "Moltres":
                card.hp = 100;
                card.maxHp = 100;
                card.attack1 = new Skill { skillName = "Sky Attack", damage = 130, energyCost = 3, skillOrder = 1, description = "None" };
                break;
        }
    }
}