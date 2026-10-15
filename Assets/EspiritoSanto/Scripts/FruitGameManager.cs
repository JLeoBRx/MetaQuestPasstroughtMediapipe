using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class FruitGameManager : MonoBehaviour
{
    public static FruitGameManager Instance;

    [Header("UI")]
    public TMP_Text collectedText;
    public TMP_Text timerText;

    [Header("Audio")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioClip menuMusic;
    public AudioClip gameMusic;

    public AudioClip correctClip;
    public AudioClip wrongClip;

    [Header("Game Config")]
    public int totalFruits = 9;
    public float gameTime = 60f;

    [Header("Events")]
    public UnityEvent OnStartGame;
    public UnityEvent OnVictory;
    public UnityEvent OnDefeat;

    private int collected;
    private float currentTime;
    private bool gameRunning;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        collected = 0;
        currentTime = gameTime;

        UpdateUI();

        if (timerText != null)
            timerText.text = Mathf.CeilToInt(currentTime).ToString();

        if (musicSource != null && menuMusic != null)
        {
            musicSource.clip = menuMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    private void Update()
    {
        if (!gameRunning)
            return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;

            if (timerText != null)
                timerText.text = "0";

            Defeat();
            return;
        }

        if (timerText != null)
            timerText.text = Mathf.CeilToInt(currentTime).ToString();
    }

    //==================================================
    // START GAME
    //==================================================

    public void StartGame()
    {
        collected = 0;
        currentTime = gameTime;

        gameRunning = true;

        UpdateUI();

        if (timerText != null)
            timerText.text = Mathf.CeilToInt(currentTime).ToString();

        if (musicSource != null && gameMusic != null)
        {
            musicSource.clip = gameMusic;
            musicSource.loop = true;
            musicSource.Play();
        }

        OnStartGame?.Invoke();

        Debug.Log("Game Started");
    }

    //==================================================
    // CORRECT FRUIT
    //==================================================

    public void CollectFruit()
    {
        if (!gameRunning)
            return;

        collected++;

        if (sfxSource != null && correctClip != null)
            sfxSource.PlayOneShot(correctClip);

        UpdateUI();

        if (collected >= totalFruits)
        {
            Victory();
        }
    }

    //==================================================
    // WRONG FRUIT
    //==================================================

    public void WrongFruit()
    {
        if (!gameRunning)
            return;

        if (sfxSource != null && wrongClip != null)
            sfxSource.PlayOneShot(wrongClip);

        currentTime -= 5f;

        if (currentTime < 0f)
            currentTime = 0f;

        if (timerText != null)
            timerText.text = Mathf.CeilToInt(currentTime).ToString();
    }

    //==================================================
    // VICTORY
    //==================================================

    private void Victory()
    {
        if (!gameRunning)
            return;

        gameRunning = false;

        Debug.Log("Victory");

        OnVictory?.Invoke();

        StartCoroutine(ReloadSceneRoutine());
    }

    //==================================================
    // DEFEAT
    //==================================================

    private void Defeat()
    {
        if (!gameRunning)
            return;

        gameRunning = false;

        Debug.Log("Defeat");

        OnDefeat?.Invoke();

        StartCoroutine(ReloadSceneRoutine());
    }

    //==================================================
    // RELOAD
    //==================================================

    private IEnumerator ReloadSceneRoutine()
    {
        yield return new WaitForSeconds(4f);

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex);
    }

    //==================================================
    // UI
    //==================================================

    private void UpdateUI()
    {
        if (collectedText != null)
            collectedText.text = collected + "/" + totalFruits;
    }
}