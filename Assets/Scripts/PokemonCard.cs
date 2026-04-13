using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

// Author: Kyle Angeles, Alex Tan
// File: PokemonCard.cs
// Date-Written: 2026/4/9
// Description: Represents a Pokemon card. Extends the base Card class.
// Supports abilities via AbilityEffect ScriptableObjects.
public class PokemonCard : Card
{
    [Header("Card Data")]
    public CardType type;
    public string imageUrl; // GitHub raw asset URL

    [Header("Stats")]
    public int maxHp;

    [Header("Evolution")]
    public PokemonCard evolvesFromCard; // Resolved at runtime from evolvesFromCardID

    [Header("Skills")]
    public List<Skill> skills = new List<Skill>(); // Max 2, ordered by Skill.skillOrder

    [Header("Ability")]
    public string abilityName;
    public string abilityDescription;
    public List<AbilityEffect> abilityEffects = new List<AbilityEffect>();
    public bool abilityUsedThisTurn = false;

    [Header("Attached Energy")]
    public List<CardType> attachedEnergy = new List<CardType>();

    [Header("Status")]
    public StatusEffect.Status currentStatus;

    /// <summary>
    /// Returns true if the pokemon has enough attached energy to use the skill at the given order (1 or 2)
    /// </summary>
    public bool CanUseSkill(int skillOrder)
    {
        Skill skill = skills.Find(s => s.skillOrder == skillOrder);
        if (skill == null) return false;
        return attachedEnergy.Count >= skill.energyCost;
    }

    /// <summary>
    /// Uses a skill on the target pokemon card
    /// </summary>
    public void UseSkill(int skillOrder, PokemonCard target)
    {
        Skill skill = skills.Find(s => s.skillOrder == skillOrder);
        if (skill == null) { Debug.Log("Skill not found!"); return; }
        if (!CanUseSkill(skillOrder)) { Debug.Log("Not enough energy to use " + skill.skillName); return; }

        Debug.Log(cardName + " used " + skill.skillName + "!");
        target.TakeDamage(skill.damage);
    }

    /// <summary>
    /// Triggers all ability effects on the target
    /// </summary>
    public void UseAbility(PokemonCard target, TurnSystem turnSystem)
    {
        if (abilityUsedThisTurn) { Debug.Log("Ability already used this turn!"); return; }

        foreach (var effect in abilityEffects)
            effect.Execute(this, target, turnSystem);

        abilityUsedThisTurn = true;
    }

    /// <summary>
    /// Applies damage to this pokemon and checks if it has fainted
    /// </summary>
    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            Debug.Log(cardName + " has fainted!");
        }
    }

    /// <summary>
    /// Returns true if this pokemon has fainted
    /// </summary>
    public bool HasFainted() => hp <= 0;
}
