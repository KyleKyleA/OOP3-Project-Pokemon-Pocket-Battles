using UnityEngine;

// Author: Kyle Angeles
// File: Skill.cs
// Date-Written: 2026/4/9
// Description: Represents a skill/attack belonging to a Pokemon card.
// Matches the Skills table combined with CardSkills junction table in the ERD.
[System.Serializable]
public class Skill
{
    public int skillID;
    public string skillName;
    public int damage;
    public string description;
    public int energyCost;

    // From CardSkills junction table — defines whether this is the 1st or 2nd attack on the card
    public int skillOrder;
}
