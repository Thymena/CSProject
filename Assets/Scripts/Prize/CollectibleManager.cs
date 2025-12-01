using System;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    [Header("Collectible Settings")]
    public GameObject[] collectibles;       
    public float baseTimer = 90f;           // Starting timer per collectible
    public float timerReduction = 10f;      // Time removed every 5 collectibles
    public float minTimer = 30f;
    public event Action<int> OnCountChanged;    

    public float CurrentTimer { get; private set; }
    public int CollectedCount { get; private set; }

    // UI listens to this
    public event Action<float> OnTimerChanged;

    private GameObject currentActiveCollectible;

    void Start()
    {
        // Disable all collectibles at the start
        foreach (GameObject obj in collectibles)
            obj.SetActive(false);

        // Start the logic
        CurrentTimer = baseTimer;
        SpawnRandomCollectible();
    }

    void Update()
    {
        if (currentActiveCollectible == null)
            return;

        // Countdown
        CurrentTimer -= Time.deltaTime;

        // Notify UI
        OnTimerChanged?.Invoke(CurrentTimer);

        // Timer expired
        if (CurrentTimer <= 0f)
        {
            currentActiveCollectible.SetActive(false);
            SpawnRandomCollectible();
        }
    }

    public void OnCollectiblePicked()
    {
        // Increase persistent count
        GameManager.Instance.collectedCount++;

        // Notify UI in this scene
        OnCountChanged?.Invoke(GameManager.Instance.collectedCount);

        // Disable current collectible
        if (currentActiveCollectible != null)
            currentActiveCollectible.SetActive(false);

        // Every 5 collected → reduce timer
        if (CollectedCount % 5 == 0)
        {
            baseTimer -= timerReduction;
            baseTimer = Mathf.Max(baseTimer, minTimer);
        }

        SpawnRandomCollectible();
    }

    private void SpawnRandomCollectible()
    {
        // Reset timer
        CurrentTimer = baseTimer;

        // Notify UI instantly
        OnTimerChanged?.Invoke(CurrentTimer);

        // Choose random collectible
        int index = UnityEngine.Random.Range(0, collectibles.Length);
        currentActiveCollectible = collectibles[index];

        // Activate it
        currentActiveCollectible.SetActive(true);
    }

}
