using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{
    private GameObject _tilePrefab;

    public int size = 10; // 10 rows and 10 columns

    public GameObject[,] TileGrid;

    private RectTransform _rectTransform;

    private GridLayoutGroup _gridLayoutGroup;

    [Header("Probabilities")] 
    public int tier5 = 5;
   
    // Start is called before the first frame update
    void Start()
    {
        _tilePrefab = Resources.Load<GameObject>("Prefabs/Tile");
        _rectTransform = GetComponent<RectTransform>();
        _gridLayoutGroup = GetComponent<GridLayoutGroup>();
        TileGrid = new GameObject[size, size];
        
        StartCoroutine(SetupGrid());
        
    }

    void CreateGrid()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                GameObject newTile = Instantiate(_tilePrefab, transform);
                newTile.GetComponent<Tile>().Tier = GetTier();
                TileGrid[i, j] = newTile;
            }
        }
    }

    void SetupNeighbors()
    {
        
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                //left side
                if (i - 1 >= 0)
                {
                    TileGrid[i, j].GetComponent<Tile>().AddNeighbor(TileGrid[i-1, j]);
                    //top
                    if (j - 1 >= 0)
                    {
                        TileGrid[i, j].GetComponent<Tile>().AddNeighbor(TileGrid[i-1, j-1]);
                    }
                    //bottom
                    if (j + 1 < size)
                    {
                        TileGrid[i, j].GetComponent<Tile>().AddNeighbor(TileGrid[i-1, j+1]);
                    }
                }
                //center - top
                if (j - 1 >= 0)
                {
                    TileGrid[i, j].GetComponent<Tile>().AddNeighbor(TileGrid[i, j-1]);
                }
                //center - bottom
                if (j + 1 < size)
                {
                    TileGrid[i, j].GetComponent<Tile>().AddNeighbor(TileGrid[i, j+1]);
                }
                //right side
                if (i + 1 < size)
                {
                    TileGrid[i, j].GetComponent<Tile>().AddNeighbor(TileGrid[i+1, j]);
                    //top
                    if (j - 1 >= 0)
                    {
                        TileGrid[i, j].GetComponent<Tile>().AddNeighbor(TileGrid[i+1, j-1]);
                    }
                    //bottom
                    if (j + 1 < size)
                    {
                        TileGrid[i, j].GetComponent<Tile>().AddNeighbor(TileGrid[i+1, j+1]);
                    }
                }
                
            }
        }
    }

    int GetTier()
    {
        //tier 5 is the change of having yellow tiles.
        int randomNumber = Random.Range(0, 100);
        if (randomNumber < tier5)
        {
            return 5;
        }

        return 1;
    }

    void SetupYellowTierTile()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (TileGrid[i, j].GetComponent<Tile>().Tier == 5)
                {
                    SetupTierNeighbors(4, TileGrid[i, j]);
                }
            }
        }
        
    }
    void SetupTierNeighbors(int currentTier, GameObject tile)
    {
        if (currentTier <= 1) return;
        foreach (var neighbor in tile.GetComponent<Tile>().GetNeighbors())
        {
            if (neighbor.GetComponent<Tile>().Tier < currentTier) //just change if the value is gonna increase.
            {
                neighbor.GetComponent<Tile>().Tier = currentTier;
                SetupTierNeighbors(currentTier - 1, neighbor); // keep doing it till the currentTier is <= 1.
            }
        }
    }

    public IEnumerator SetupGrid()
    {
        yield return new WaitForEndOfFrame(); // so we can get the actual width of the RectTransform
       
        //resizing the tiles in the grid
        float width = _rectTransform.sizeDelta.x;
        float sizeOfTiles = width / size;
        _gridLayoutGroup.cellSize = new Vector2(sizeOfTiles, sizeOfTiles);
        
        CreateGrid();
        SetupNeighbors();
        SetupYellowTierTile();
    }

}
