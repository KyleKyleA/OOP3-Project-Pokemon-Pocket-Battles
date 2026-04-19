using UnityEngine;

public class AI : MonoBehaviour
{
    public TurnSystem turnSystem;
    public bool useAbilityBeforeAttack = true;
    public float turnDelaySeconds = 0.75f;

    public void ExecuteAITurn()
    {
        if (turnSystem == null || turnSystem.currentState == TurnSystem.GameState.GameOver)
            return;

        Debug.Log("AI is thinking...");

        // 1. attach an energy
        // 2. try ability
        // 3. attack
        turnSystem.AttachEnergy();

        if (useAbilityBeforeAttack)
            turnSystem.UseAbility();

        PokemonCard aiPokemon = turnSystem.playerTwoActivePokemon;
        Skill chosenSkill = GetBestUsableSkill(aiPokemon);

        if (chosenSkill != null)
        {
            Debug.Log("AI chose skill: " + chosenSkill.skillName);
            turnSystem.Attack(chosenSkill.skillOrder);
            return;
        }

        Debug.Log("AI has no usable skills. Ending turn.");
        Invoke(nameof(EndTurn), turnDelaySeconds);
    }

    public Skill GetBestUsableSkill(PokemonCard pokemon)
    {
        if (pokemon == null || pokemon.skills == null)
            return null;

        Skill bestSkill = null;
        int highestDamage = -1;

        foreach (Skill skill in pokemon.skills)
        {
            if (skill.CanUse(pokemon) && skill.damage > highestDamage)
            {
                highestDamage = skill.damage;
                bestSkill = skill;
            }
        }

        return bestSkill;
    }

    void EndTurn()
    {
        turnSystem.EndTurn();
    }
}
