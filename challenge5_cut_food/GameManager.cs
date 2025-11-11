using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    private float spawnRate = 1f;
    public GameObject titlePanel;
    public GameObject endPanel;
    public TextMeshProUGUI scoreText;
    //public TextMeshProUGUI gameOverText;
    //public Button restartButton;
    public bool isGameActive;
    private int score;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShowUIState(true, false);
    }
    public void StartGame(int difficulty)
    {
        if (targets.Count > 0)
        {
            spawnRate /= difficulty;
            StartCoroutine(SpawnTarget());
        }

        isGameActive = true;
        UpdateScore(0);

        ShowUIState(false, false);
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
    }

    private void ShowUIState(bool showStart, bool showEnd)
    {
        endPanel.gameObject.SetActive(showEnd);
        titlePanel.gameObject.SetActive(showStart);
    }
    public void GameOver()
    {
        isGameActive = false;
        ShowUIState(false, true);
        //gameOverText.gameObject.SetActive(true);
        //restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
