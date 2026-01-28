using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;

    [Header("UI Settings")]
    public GameObject gameOverCanvas;
    public float defaultDelay = 2f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void TriggerGameOver()
    {
        Invoke(nameof(ShowCanvas), defaultDelay);
    }

    private void ShowCanvas()
    {
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}