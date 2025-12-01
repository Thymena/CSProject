using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    
    public GameObject player;
    public GameObject berryControl;
    public PlayerHP playerHP;
    public BerryControl setTime;
    public float damage = 100;
    public float time;

    void Start()
    {
        setTime = berryControl.GetComponent<BerryControl>(); 
        time = setTime.time;
    }

  
    void Update()
    {
        time -=  Time.deltaTime;
        
        if (time <= 0)
        {
            playerHP = player.GetComponent<PlayerHP>();
            playerHP.health -= damage;
        }
        
        int minutes =  Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        text.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        
        
        
    }
}
