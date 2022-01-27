using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void RestartGame()
    {
        Tool.CurrentExcavating = 0;
        Tool.CurrentScanning = 0;
        SceneManager.LoadScene(0);
    }
    
}
