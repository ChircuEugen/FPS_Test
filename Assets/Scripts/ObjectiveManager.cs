using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ObjectiveManager : MonoBehaviour
{
    private int enemyCount = 0;
    private GameObject[] enemies;

    public GameObject victoryScreen;
    public GameObject gameOverScreen;

    private CanvasGroup canvasGroup;
    private float fadeInDuration = 1f;

    private void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCount = enemies.Length;
    }

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDeath += PlayerFailed;
        EnemyHealth.OnEnemyDeath += DecreaseEnemyCounter;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDeath -= PlayerFailed;
        EnemyHealth.OnEnemyDeath -= DecreaseEnemyCounter;
    }

    public void DecreaseEnemyCounter()
    {
        enemyCount--;

        if(enemyCount == 0)
        {
            Cursor.lockState = CursorLockMode.None;
            victoryScreen.SetActive(true);
            canvasGroup = victoryScreen.GetComponent<CanvasGroup>();
            StartCoroutine(FadeInEffect());
        }
    }

    public void PlayerFailed()
    {
        Cursor.lockState = CursorLockMode.None;
        gameOverScreen.SetActive(true);
        canvasGroup = gameOverScreen.GetComponent<CanvasGroup>();
        StartCoroutine(FadeInEffect());
    }

    IEnumerator FadeInEffect()
    {
        float elapsed = 0f;
        while (elapsed < fadeInDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsed / fadeInDuration);
            yield return null;
        }
    }
}
