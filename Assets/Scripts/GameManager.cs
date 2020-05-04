using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public float spawnRate = 1f;
    private float score = 0;
    public int lives = 5;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI startScreenText;
    public TextMeshProUGUI pauzeText;
    public TextMeshProUGUI pauzeButtonText;
    public bool isGameActive;

    public Button restartButton;
    public Button eazyButton;
    public Button mediumButton;
    public Button hardButton;
    public Button pauzeButton;

    public Slider volumeSlider;
    private AudioSource backgroundMusic;

    // Start is called before the first frame update
    void Start()
    {
        isGameActive = true;
        livesText.text = "Lives: " + lives;
        scoreText.text = "Score: " + score;
        backgroundMusic = GameObject.Find("Main Camera").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        backgroundMusic.volume = volumeSlider.value;
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
    IEnumerator Counter(int count)
    {
        if (count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                score++;
                scoreText.text = "Score: " + score;
                yield return new WaitForSeconds(1 / (count * 10));
            }
        }
        if(count < 0)
        {
            for (int i = count; i < 0; i++)
            {
                score--;
                scoreText.text = "Score: " + score;
                yield return new WaitForSeconds(1 / (count * 10));
            }

        }
    }
    public void UpdateScore(int scoreToAdd)
    {
        StartCoroutine(Counter(scoreToAdd));
    }
    public void UpdateLives()
    {
        lives--;
        livesText.text = "Lives: " + lives;
        if(lives <= 0)
        {
            gameOverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);

            isGameActive = false;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ActiveGameScreen()
    {
        livesText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
        startScreenText.gameObject.SetActive(false);
        eazyButton.gameObject.SetActive(false);
        mediumButton.gameObject.SetActive(false);
        hardButton.gameObject.SetActive(false);
        pauzeButton.gameObject.SetActive(true);
    }
    public void Eazy()
    {
        spawnRate = 1.0f;
        StartCoroutine(SpawnTarget());
        ActiveGameScreen();
    }

    public void Medium()
    {
        spawnRate = 0.7f;
        StartCoroutine(SpawnTarget());
        ActiveGameScreen();
    }

    public void Hard()
    {
        spawnRate = 0.3f;
        StartCoroutine(SpawnTarget());
        ActiveGameScreen();

    }
    public void Pauze()
    {
        if (pauzeButtonText.text == "Pauze" && isGameActive)
        {
            Time.timeScale = 0;
            pauzeText.gameObject.SetActive(true);
            isGameActive = false;
            pauzeButtonText.text = "Resume";
        }
        if(pauzeButtonText.text == "Resume" && isGameActive)
        {
            Time.timeScale = 1;
            pauzeText.gameObject.SetActive(false);
            isGameActive = true;
            pauzeButtonText.text = "Pauze";
        }
    
    }
}
