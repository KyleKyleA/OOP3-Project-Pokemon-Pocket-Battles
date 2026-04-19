// Author: Alex Tan
// File: PokemonImageDisplay.cs
// Date-Written: Alex Tan
// Description: Quick UI script to load Images
using UnityEngine;
using UnityEngine.UI;

public class PokemonImageDisplay : MonoBehaviour
{
    public TurnSystem turnSystem;
    public Image playerImage;
    public Image opponentImage;

    void Start()
    {
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        if (turnSystem.playerOneActivePokemon != null)
            playerImage.sprite = turnSystem.playerOneActivePokemon.pokemonSprite;

        if (turnSystem.playerTwoActivePokemon != null)
            opponentImage.sprite = turnSystem.playerTwoActivePokemon.pokemonSprite;
    }

    /// Refreshes when active pokemon changes(might be useful later)
    /// FindObjectOfType<PokemonDisplay>()?.UpdateDisplay();
}