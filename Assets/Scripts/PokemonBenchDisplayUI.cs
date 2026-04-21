using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BenchDisplayUI : MonoBehaviour
{
    public TurnSystem turnSystem;

    public Image[] playerBenchImages;
    public Image[] opponentBenchImages;

    public void Refresh()
    {
        RefreshBench(turnSystem.playerOneBench, playerBenchImages);
        RefreshBench(turnSystem.playerTwoBench, opponentBenchImages);
    }

    private void RefreshBench(List<PokemonCard> bench, Image[] images)
    {
        for (int i = 0; i < images.Length; i++)
        {
            if (i < bench.Count)
            {
                images[i].sprite = bench[i].pokemonSprite;
                images[i].enabled = true;
            }
            else
            {
                images[i].sprite = null;
                images[i].enabled = false;
            }
        }
    }
}