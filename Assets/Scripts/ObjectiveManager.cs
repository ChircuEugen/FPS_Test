using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ObjectiveManager : MonoBehaviour
{
    private int enemyCount = 0;
    private GameObject[] enemies;
    private PlayerShooter playerShooter;

    public GameObject victoryScreen;
    public GameObject gameOverScreen;

    private CanvasGroup canvasGroup;
    private float fadeInDuration = 1f;

    public Text progress;
    private int victories;
    private int failures;

    private void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        playerShooter = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShooter>();
        enemyCount = enemies.Length;

        victories = PlayerPrefs.GetInt("Victories", 0);
        failures = PlayerPrefs.GetInt("Failures", 0);

        progress.text = "Failures: " + failures + "\n Victories: " + victories;
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
            playerShooter.SaveAmmoData();
            victories++;
            progress.text = "Failures: " + failures + "\n Victories: " + victories;
            PlayerPrefs.SetInt("Victories", victories);

            StartCoroutine(FadeInEffect());
        }
    }

    public void PlayerFailed()
    {
        Cursor.lockState = CursorLockMode.None;
        gameOverScreen.SetActive(true);
        canvasGroup = gameOverScreen.GetComponent<CanvasGroup>();
        failures++;
        progress.text = "Failures: " + failures + "\n Victories: " + victories;
        PlayerPrefs.SetInt("Failures", failures);

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
