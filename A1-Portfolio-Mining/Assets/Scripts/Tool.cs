using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public static bool IsScanning;
    public static int MaxScanningAmount = 6;
    public static int CurrentScanning = 0;
    public static int MaxExcavatingAmount = 3;
    public static int CurrentExcavating = 0;

    private Texture2D _cursorTextureScan;
    private Texture2D _cursorTextureExcavate;
    private CursorMode _cursorMode = CursorMode.Auto;
    private readonly Vector2 _hotSpot = Vector2.zero;

    private TMPro.TextMeshProUGUI _scanText;
    private TMPro.TextMeshProUGUI _excavatorText;
    
    void Start()
    {
        _cursorTextureScan = Resources.Load<Texture2D>("Textures/loupeCursor");
        _cursorTextureExcavate = Resources.Load<Texture2D>("Textures/pickaxeCursor");

        _scanText = transform.Find("Scan/Text").GetComponent<TextMeshProUGUI>();
        _excavatorText = transform.Find("Excavator/Text").GetComponent<TextMeshProUGUI>();
        
        Color32[] pixels = _cursorTextureExcavate.GetPixels32();
        pixels = RotateMatrix(pixels, _cursorTextureExcavate.width);
        _cursorTextureExcavate.SetPixels32(pixels);

        ClickOnScanning();
    }

    private void Update()
    {
        _scanText.text = (Tool.MaxScanningAmount - Tool.CurrentScanning).ToString();
        _excavatorText.text = (Tool.MaxExcavatingAmount - Tool.CurrentExcavating).ToString();
    }

    public void ClickOnScanning()
    {
        IsScanning = true;
        Cursor.SetCursor(_cursorTextureScan,_hotSpot,_cursorMode);
    }
    public void ClickOnExcavating()
    {
        IsScanning = false;
        Cursor.SetCursor(_cursorTextureExcavate,_hotSpot,_cursorMode);
    }

    static Color32[] RotateMatrix(Color32[] matrix, int n) {
        Color32[] ret = new Color32[n * n];
             
        for (int i = 0; i < n; ++i) {
            for (int j = 0; j < n; ++j) {
                ret[i*n + j] = matrix[(n - j - 1) * n + i];
            }
        }
             
        return ret;
    }
}
