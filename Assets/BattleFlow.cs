using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleFlow : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject gameWinUI;

    public PlayerHealth playerHealth;
    public GameObject bgMusic;

    private bool isGameEnd = false;

    private void Start()
    {
        gameOverUI.SetActive(false);
        gameWinUI.SetActive(false);

        playerHealth.onDead += OnGameOver;
    }

    private void Update()
    {
        if (isGameEnd) return;

        if (EnemyHealth.LivingEnemyCount <= 0)
        {
            OnGameWin();
        }
    }

    private void OnGameWin()
    {
        isGameEnd = true;

        gameWinUI.SetActive(true);
        bgMusic.SetActive(false);

        playerHealth.gameObject.SetActive(false);
    }

    private void OnGameOver()
    {
        isGameEnd = true;

        gameOverUI.SetActive(true);
        bgMusic.SetActive(false);
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}