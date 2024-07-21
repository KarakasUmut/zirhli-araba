using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public GameObject FinishScreen;
    public GameObject RestartButton;
    public GameObject HealtBar;
    public GameObject BackGround;
    public CarMovement Player;
   


    public void finishScreen()
    { 
    
        FinishScreen.SetActive(true);
        BackGround.SetActive(false);
        HealtBar.SetActive(false);
        
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Oyun zaman ölçeðini varsayýlan deðere ayarla (devam eden hýz)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Aktif sahneyi yeniden yükle
    }

    

    
}
