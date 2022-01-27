using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    // Start is called before the first frame update
    private List<GameObject> _neighbors;


    public int Tier = 5;
    public bool isRevealed;

    private readonly string tier1HexColor = "#240E00";
    private readonly string tier2HexColor = "#6C3700";
    private readonly string tier3HexColor = "#B06800";
    private readonly string tier4HexColor = "#E0A200";
    private readonly string tier5HexColor = "#FFF500";

    private string currentHexColor;
    private Color currentColor;

    private ResourcesManager _resourcesManager;
    private Image _image;
    void Awake()
    {
        _image = GetComponent<Image>();
        _neighbors = new List<GameObject>();
    }

    private void Start()
    {
        _resourcesManager = FindObjectOfType<ResourcesManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddNeighbor(GameObject tile)
    {
        _neighbors.Add(tile);
    }

    public void SetTier(int newTier)
    {
        Tier = newTier;
        switch (Tier)
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
        if (ColorUtility.TryParseHtmlString(currentHexColor, out currentColor) && isRevealed)
        {
            _image.color = currentColor;
        }
    }

    private void LowOneTier()
    {
        Tier -= 1;
        if (Tier < 1) Tier = 1;
        SetTier(Tier);
    }

    public void Reveal()
    {
        isRevealed = true;
        SetTier(Tier); // show the true colors
    }

    public void OnClickEvent()
    {
        if (!Tool.IsScanning) //excavating
        {
            if (!isRevealed) return;
            if(Tool.CurrentExcavating >= Tool.MaxExcavatingAmount) return;
            
            //excavate the on-clicked tile
            _resourcesManager.NewResourceAdded(Tier);
            SetTier(1);
            
            List<GameObject> changedTiles = new List<GameObject>(); //record of the tiles that we change so we don't repeat.
            foreach (var neighbor in _neighbors)
            {
                //check if the neighbor is in the changedTiles list -> if not -> add.
                if(!changedTiles.Exists(x => x == neighbor))
                {
                    changedTiles.Add(neighbor);
                }

                //outer circle
                foreach (var neighbor2 in neighbor.GetComponent<Tile>()._neighbors)
                {
                    if(!changedTiles.Exists(x => x == neighbor2))
                    {
                        changedTiles.Add(neighbor2);
                    }
                }

            }
            //Low the tier of each tile in the list
            foreach (var tiles in changedTiles)
            {
                tiles.GetComponent<Tile>().LowOneTier();
            }

            Tool.CurrentExcavating++;
        }
        else //scanning
        {
            if (isRevealed) return;
            if(Tool.CurrentScanning >= Tool.MaxScanningAmount) return;
            Reveal();
            foreach (var neighbor in _neighbors)
            {
                neighbor.GetComponent<Tile>().Reveal();
            }

            Tool.CurrentScanning++;
        }
    }
    
}
