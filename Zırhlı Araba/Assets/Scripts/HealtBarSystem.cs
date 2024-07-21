using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class HealtBarSystem : MonoBehaviour
{
    private CarMovement Player;

    public Image Healtbar;

    public float currentHealt;

    public float maxHealt;

    

    

    private void Start()
    {
        Player = GameObject.FindFirstObjectByType<CarMovement>();
        if (Healtbar  != null)
        {
            
        }
    }

    private void Update()
    {
        
        currentHealt = Player.Healt;
        Healtbar.fillAmount = currentHealt / maxHealt;
    }

    
}
