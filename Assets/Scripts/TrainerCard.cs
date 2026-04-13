using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

// Author: Alex Tan
// File: TrainerCard.cs
// Date-Written: April 13th 2026
// Description: Represents a Trainer card. Extends the base Card class.
// Trainer effects are driven by AbilityEffect ScriptableObjects —
// no new script needed per card, just configure effects in the Inspector.
public class TrainerCard : Card
{
    public enum TrainerType { Item, Supporter }
    public TrainerType trainerType;

    [Header("Effects")]
    public List<AbilityEffect> trainerEffects = new List<AbilityEffect>();

    /// <summary>
    /// Executes all configured effects for this trainer card.
    /// Override this in a subclass only if unique logic is needed beyond the effect list.
    /// </summary>
    public virtual void Activate(TurnSystem turnSystem)
    {
        foreach (var effect in trainerEffects)
            effect.Execute(null, null, turnSystem);

        Debug.Log(cardName + " activated!");
    }
}
