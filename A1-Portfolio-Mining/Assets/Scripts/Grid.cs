using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private GameObject _tilePrefab;

    public int size = 10; // 10 rows and 10 columns

    public GameObject[,] TileGrid;
    // Start is called before the first frame update
    void Start()
    {
        _tilePrefab = Resources.Load<GameObject>("Prefabs/Tile");
        TileGrid = new GameObject[size, size];
        
        CreateGrid();
        SetupNeighbors();
    }

    void CreateGrid()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                GameObject newTile = Instantiate(_tilePrefab, transform);
                newTile.GetComponent<Tile>().Tier = Random.Range(1, 6);
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
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
