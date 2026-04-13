using Assets.Scripts;
using UnityEngine;

// Author: Alex Tan
// File: HealEffect.cs
// Date-Written: April 13th 2026
// Description: Effect that heals the pokemon by a fixed amount, capped at maxHp.
// Create via Assets > Create > Effects > Heal in the Unity Editor.
[CreateAssetMenu(menuName = "Effects/Heal")]
public class HealEffect : AbilityEffect
{
    public int healAmount;

    public override void Execute(PokemonCard user, PokemonCard target, TurnSystem turnSystem)
    {
        if (user == null) { Debug.LogWarning("HealEffect: No user!"); return; }
        user.hp = Mathf.Min(user.hp + healAmount, user.maxHp);
        Debug.Log(user.cardName + " healed " + healAmount + " HP. Current HP: " + user.hp);
    }
}
