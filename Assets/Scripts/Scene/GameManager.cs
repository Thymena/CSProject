using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int collectedCount = 0; // persistent count

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }
}