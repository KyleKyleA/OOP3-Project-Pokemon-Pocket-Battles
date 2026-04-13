using Assets.Scripts;
using UnityEngine;

// Author: Alex Tan
// File: DamageEffect.cs
// Date-Written: April 13th 2026
// Description: Effect that deals a fixed amount of damage to the target.
// Create via Assets > Create > Effects > Deal Damage in the Unity Editor.
[CreateAssetMenu(menuName = "Effects/Deal Damage")]
public class DealDamageEffect : AbilityEffect
{
    public int damageAmount;

    public override void Execute(PokemonCard user, PokemonCard target, TurnSystem turnSystem)
    {
        if (target == null) { Debug.LogWarning("DealDamageEffect: No target!"); return; }
        target.TakeDamage(damageAmount);
        Debug.Log(user.cardName + " dealt " + damageAmount + " damage to " + target.cardName);
    }
}
