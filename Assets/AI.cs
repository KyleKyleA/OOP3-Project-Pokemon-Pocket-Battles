using UnityEngine;

public class AI : MonoBehaviour
{
    public TurnSystem turnSystem;

    public void ExecuteAITurn()
    {
        Debug.Log("AI is thinking...");

        // draw a card
        turnSystem.drawCard(TurnSystem.Player.PlayerTwo);

        // decide to attack 
        if (Random.value > 0.5f)
        {
            Debug.Log("AI chooses to Attack.");
            turnSystem.Attack();
        }

        // end turn after 2 seconds
        Invoke("EndTurn", 2.0f);
    }

    void EndTurn()
    {
        turnSystem.EndTurn();
    }
}