using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public GameObject gameOverPanel;

    private bool gameOver = false;

    void Start()
    {
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (gameOver)
        {
            return;
        }

        // Press G to show the Game Over screen
        if (Input.GetKeyDown(KeyCode.G))
        {
            ShowGameOver();
            return;
        }

        // Press N to advance to the next level
        if (Input.GetKeyDown(KeyCode.N))
        {
            NextLevel();
        }
    }

    public void NextLevel()
    {
        int currentLevel =
            SceneManager.GetActiveScene().buildIndex;

        int totalLevels =
            SceneManager.sceneCountInBuildSettings;

        if (currentLevel < totalLevels - 1)
        {
            SceneManager.LoadScene(currentLevel + 1);
        }
        else
        {
            ShowGameOver();
        }
    }

    public void ShowGameOver()
    {
        if (gameOver) return;

        gameOver = true;
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Level1");
    }
}