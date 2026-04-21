using System.Diagnostics;
using TMPro;
using UnityEngine;

public class PokemonStatsDisplayUI : MonoBehaviour
{
    public TurnSystem turnSystem;

    [Header("Player UI")]
    public TextMeshProUGUI playerHpText;
    public TextMeshProUGUI playerEnergyText;

    [Header("Opponent UI")]
    public TextMeshProUGUI opponentHpText;
    public TextMeshProUGUI opponentEnergyText;

    private void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        UnityEngine.Debug.Log("Refresh called");
        if (turnSystem == null)
            return;

        PokemonCard player = turnSystem.playerOneActivePokemon;
        PokemonCard opponent = turnSystem.playerTwoActivePokemon;


        if (player != null)
        {
            if (playerHpText != null) playerHpText.text = $"HP: {player.hp}/{player.maxHp}";
            if (playerEnergyText != null) playerEnergyText.text = $"Energy: {player.attachedEnergy.Count}";
        }
        else
        {
            if (playerHpText != null) playerHpText.text = "HP: -";
            if (playerEnergyText != null) playerEnergyText.text = "Energy: -";
        }

        if (opponent != null)
        {
            if (opponentHpText != null) opponentHpText.text = $"HP: {opponent.hp}/{opponent.maxHp}";
            if (opponentEnergyText != null) opponentEnergyText.text = $"Energy: {opponent.attachedEnergy.Count}";
        }
        else
        {
            if (opponentHpText != null) opponentHpText.text = "HP: -";
            if (opponentEnergyText != null) opponentEnergyText.text = "Energy: -";
        }
    }
}