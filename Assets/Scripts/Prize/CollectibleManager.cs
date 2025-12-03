using System; 
using UnityEngine; 

public class CollectibleManager : MonoBehaviour { 
    [Header("Collectible Settings")] 
    public GameObject[] collectibles; 
    public float baseTimer = 90f; // Starting timer per collectible
    public float timerReduction = 10f; // Time removed every 5 collectibles
    public float minTimer = 30f;
    public event Action<int> OnCountChanged; 
    public float CurrentTimer { get; private set; } 
    public int CollectedCount { get; private set; } 
    // UI listens to this
    public event Action<float> OnTimerChanged; 
    private GameObject currentActiveCollectible; 
    
    void Start() { 
        // Reset count for this round
        GameManager.Instance.ResetRound(); 
        
        foreach (GameObject obj in collectibles) 
            obj.SetActive(false); 
        
        CurrentTimer = baseTimer; 
        SpawnRandomCollectible(); 
    } 
    void Update() { 
        if (currentActiveCollectible == null) 
            return; 
        // Countdown
        CurrentTimer -= Time.deltaTime; 
        // Notify UI
        OnTimerChanged?.Invoke(CurrentTimer); 
        // Timer expired
        if (CurrentTimer <= 0f)
        {
            currentActiveCollectible.SetActive(false); SpawnRandomCollectible();
        } 
    } 
    
    public void OnCollectiblePicked() { 
        // Increase count for this round
        GameManager.Instance.lastRoundCollected++; 
        OnCountChanged?.Invoke(GameManager.Instance.lastRoundCollected); 
        // Rest of your logic
        if (GameManager.Instance.lastRoundCollected % 5 == 0)
        {
            baseTimer -= timerReduction; baseTimer = Mathf.Max(baseTimer, minTimer);
        } 
        SpawnRandomCollectible(); 
    } 
    private void SpawnRandomCollectible()
    {
        // Reset timer
        CurrentTimer = baseTimer;
        OnTimerChanged?.Invoke(CurrentTimer);

        if (collectibles.Length == 0)
            return;

        int newIndex;

        // If no previous collectible, choose any random one
        if (currentActiveCollectible == null)
        {
            newIndex = UnityEngine.Random.Range(0, collectibles.Length);
        }
        else
        {
            // Find index of last active collectible
            int lastIndex = Array.IndexOf(collectibles, currentActiveCollectible);

            // Pick until a different collectible is chosen
            do
            {
                newIndex = UnityEngine.Random.Range(0, collectibles.Length);
            }
            while (newIndex == lastIndex);
        }

        // Disable previous collectible (important)
        foreach (var obj in collectibles)
            obj.SetActive(false);

        // Activate the new collectible
        currentActiveCollectible = collectibles[newIndex];
        currentActiveCollectible.SetActive(true);
    }

}
