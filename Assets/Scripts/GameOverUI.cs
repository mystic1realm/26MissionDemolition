using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject castle;
    public float checkInterval = 2f;

    private float nextCheckTime;
    private bool gameOver = false;

    void Start()
    {
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        // Temporary test: press G to show the Game Over screen
        if (Input.GetKeyDown(KeyCode.G))
        {
            ShowGameOver();
        }

        if (gameOver || castle == null)
        {
            return;
        }

        if (Time.time >= nextCheckTime)
        {
            nextCheckTime = Time.time + checkInterval;
            CheckCastle();
        }
    }

    void CheckCastle()
    {
        CastleBlock[] blocks =
            castle.GetComponentsInChildren<CastleBlock>();

        if (blocks.Length == 0)
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
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}