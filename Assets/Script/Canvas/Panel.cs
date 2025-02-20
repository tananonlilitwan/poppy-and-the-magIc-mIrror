using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Panel : MonoBehaviour
{
    
    [SerializeField] private GameObject menuPanel;         // Panel 'CanvasMenu'
    [SerializeField] private GameObject canvasPanel;       // Panel 'Canvas Ui in game'
    
    [SerializeField] private GameObject endCreditPanel;    // Panel 'EndCredit'
    [SerializeField] private GameObject canvasPuzzlePanel;  // Panel 'Canvas puzzle'
    
    [Header("--------------------HowToPanel--------------------")]
    [SerializeField] private GameObject howToPanel;  // Panel 'HowTo'
    
    public void ShowHowToPanel()
    {
        // เปิด Panel 'HowTo'
        if (howToPanel != null)
        {
            howToPanel.SetActive(true);
        }
    }
    public void HideHowToPanel()
    {
        // ปิด Panel 'HowTo'
        if (howToPanel != null)
        {
            howToPanel.SetActive(false);
        }
    }
    
    public void Quit()
    {
        // ปิด Panel 'Canvas Ui in game'
        if (canvasPanel != null)
        {
            canvasPanel.SetActive(false);
        }

        // ปิด Panel 'EndCredit' หากเปิดอยู่
        if (endCreditPanel != null)
        {
            endCreditPanel.SetActive(false);
        }
        
        // ปิด Panel 'Canvas puzzle'
        if (canvasPuzzlePanel != null)
        {
            canvasPuzzlePanel.SetActive(false);
        }

        // เปิด Panel 'CanvasMenu'
        if (menuPanel != null)
        {
            menuPanel.SetActive(true);
        }
    }
}
