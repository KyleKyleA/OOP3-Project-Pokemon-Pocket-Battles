using UnityEngine;

// Author: Alex Tan
// File: AbilityEffect.cs
// Date-Written: Alex Tan
// Description: Abstract base ScriptableObject for all card effects.
// Subclass this to create reusable ability and trainer effects
// that can be configured in the Inspector without writing new scripts per card.
public abstract class AbilityEffect : ScriptableObject
{
    public string effectName;
    public string description;

    /// <summary>
    /// Processes the Pokemon's Ability or Supporter Card.
    /// </summary>
    public abstract void Execute(PokemonCard user, PokemonCard target, TurnSystem turnSystem);
}
