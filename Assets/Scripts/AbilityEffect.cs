using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
// Author: Alex Tan
// File: AbilityEffect.cs
// Date-Written: April 13th 2026
// Description: Class building block for all unique Pokemon abilities and trainer cards.

namespace Assets.Scripts
{
    // AbilityEffect.cs
    public abstract class AbilityEffect : ScriptableObject
    {
        public string effectName;
        public string description;

        // Activate Pokemon Effect/Trainer Card
        public abstract void Execute(PokemonCard user, PokemonCard target, TurnSystem turnSystem);
    }
}
