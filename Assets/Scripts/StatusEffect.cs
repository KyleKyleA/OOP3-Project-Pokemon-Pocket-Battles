using Assets.Scripts;
using UnityEngine;

// Author: Alex Tan
// File: StatusEffect.cs
// Date-Written: April 13th 2026
// Description: Effect that applies a status condition to the target pokemon.
// Create via Assets > Create > Effects > Apply Status in the Unity Editor.
[CreateAssetMenu(menuName = "Effects/Apply Status")]
public class StatusEffect : AbilityEffect
{
    public enum Status { None, Poisoned, Paralyzed, Asleep, Burned }
    public Status statusToApply;

    public override void Execute(PokemonCard user, PokemonCard target, TurnSystem turnSystem)
    {
        if (target == null) { Debug.LogWarning("StatusEffect: No target!"); return; }
        target.currentStatus = statusToApply;
        Debug.Log(target.cardName + " is now " + statusToApply + "!");
    }
}
