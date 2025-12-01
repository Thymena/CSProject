using UnityEngine;
using TMPro;

public class PersistentCollectCountUI : MonoBehaviour
{
    public TextMeshProUGUI countText;

    void Start()
    {
        // Initialize with current persistent count
        countText.text = "" + GameManager.Instance.collectedCount;
    }
}