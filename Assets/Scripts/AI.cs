using UnityEngine;
using System.Collections.Generic;

// Author: Karim
// description: this file contains is reponsible for the AI's decision making during its turn.
public class AI : MonoBehaviour
{
    public TurnSystem turnSystem;
    public float turnDelaySeconds = 0.75f;

    public void ExecuteAITurn()
    {
        // if game is over or turn system is not set, do nothing
        if (turnSystem == null || turnSystem.currentState == TurnSystem.GameState.GameOver) return;

        Debug.Log("AI is processing its turn...");

        // Play a Pokemon from hand if the Active slot is empty
        if (turnSystem.playerTwoActivePokemon == null && turnSystem.playerTwoHand.Count > 0)
        {
            // Search the hand for a card that is actually a Pokemon 
            Card pokemonToPlay = turnSystem.playerTwoHand.Find(c => c is PokemonCard);
            
            if (pokemonToPlay != null)
            {
                turnSystem.playPokemon(pokemonToPlay.gameObject);
                Debug.Log($"AI played {pokemonToPlay.cardName} to the Active slot.");
            }
            else
            {
                Debug.Log("AI has no Pokemon in hand to play.");
            }
        }

        // Attach Energy and use Abilities
        turnSystem.AttachEnergy();
        turnSystem.UseAbility();

        // Attack Logic
        PokemonCard aiActive = turnSystem.playerTwoActivePokemon;
        if (aiActive != null)
        {
            Skill bestSkill = GetBestUsableSkill(aiActive);
            if (bestSkill != null)
            {
                Debug.Log($"AI attacking with {bestSkill.skillName}");
                // We pass the skillorder to the attack method
                turnSystem.Attack(bestSkill.skillOrder);
                return; // Attack internally calls EndTurn, so we exit here.
            }
        }

        // If no attack was possible, end the turn after a short delay
        Invoke(nameof(EndTurn), turnDelaySeconds);
    }

    /// <summary>
    /// Searches the Pokemon's skill list for the highest damage skill.
    /// </summary>
    public Skill GetBestUsableSkill(PokemonCard pokemon)
    {
        if (pokemon == null || pokemon.skills == null || pokemon.skills.Count == 0) return null;

        Skill best = null;
        int maxDamage = -1;

        foreach (Skill s in pokemon.skills)
        {
            // this is to pick the skill based on the highest damage
            if (s.damage > maxDamage)
            {
                maxDamage = s.damage;
                best = s;
            }
        }

        return best;
    }

    // this is to end the AI's turn after it made its move
    void EndTurn()
    {
        if (turnSystem != null)
        {
            turnSystem.EndTurn();
        }
    }
}