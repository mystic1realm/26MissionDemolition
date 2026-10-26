using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TMPro.TextMeshProUGUI levelText;

    private bool gameOver = false;

    void Start()
    {
        gameOverPanel.SetActive(false);
        UpdateLevelText();
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

    void UpdateLevelText()
    {
        int currentLevel = SceneManager
            .GetActiveScene().buildIndex;

        if (currentLevel >= 0 && currentLevel < 4)
        {
            levelText.text = "Level " + (currentLevel + 1);
        }
    }

    public void NextLevel()
    {
        int currentLevel =
            SceneManager.GetActiveScene().buildIndex;

        int totalLevels =
            SceneManager.sceneCountInBuildSettings;

        if (currentLevel < totalLevels - 1 &&
            currentLevel < 3)
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