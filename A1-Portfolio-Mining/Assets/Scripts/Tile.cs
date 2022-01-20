using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> neighbors;

    public int tier = 1;
    public bool isRevealed;

    private readonly string tier1HexColor = "#240E00";
    private readonly string tier2HexColor = "#6C3700";
    private readonly string tier3HexColor = "#B06800";
    private readonly string tier4HexColor = "#E0A200";
    private readonly string tier5HexColor = "#FFF500";

    private string currentHexColor;
    private Color currentColor;

    private Image _image;
    void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Start()
    {
        neighbors = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddNeighbor(GameObject tile)
    {
        neighbors.Add(tile);
    }

    public void SetTier(int newTier)
    {
        switch (newTier)
        {
            case 1:
                currentHexColor = tier1HexColor;
                break;
            case 2:
                currentHexColor = tier2HexColor;
                break;
            case 3:
                currentHexColor = tier3HexColor;
                break;
            case 4:
                currentHexColor = tier4HexColor;
                break;
            case 5:
                currentHexColor = tier5HexColor;
                break;
            default:
                return;
        }
        tier = newTier;
        if (ColorUtility.TryParseHtmlString(currentHexColor, out currentColor))
        {
            _image.color = currentColor;
        }
    }

    public void OnClickEvent()
    {
        Debug.Log(neighbors);
    }
}
