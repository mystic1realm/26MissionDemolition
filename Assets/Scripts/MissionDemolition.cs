using UnityEngine;

public class MissionDemolition : MonoBehaviour
{
    public GameOverUI gameOverUI;

    public void LevelWon()
    {
        gameOverUI.ShowGameOver();
    }
}
