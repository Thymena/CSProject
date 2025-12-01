using UnityEngine;
using TMPro;

public class CollectCountUI : MonoBehaviour
{
    public CollectibleManager collectibleManager;
    public TextMeshProUGUI countText;

    private void Start()
    {
        // Subscribe to the manager’s event
        collectibleManager.OnCountChanged += UpdateCountUI;

        // Initialize display
        UpdateCountUI(collectibleManager.CollectedCount);
    }

    private void UpdateCountUI(int count)
    {
        countText.text = "" + count;
    }

    private void OnDestroy()
    {
        if (collectibleManager != null)
            collectibleManager.OnCountChanged -= UpdateCountUI;
    }
}