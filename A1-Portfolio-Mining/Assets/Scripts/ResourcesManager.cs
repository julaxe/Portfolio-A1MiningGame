using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    public int Score = 0;
    private GameObject _resourceTilePrefab;
    private TextMeshProUGUI _scoreText;
    
    void Start()
    {
        _resourceTilePrefab = Resources.Load<GameObject>("Prefabs/ResourceTile");
        _scoreText = transform.Find("Score").GetComponent<TextMeshProUGUI>();
    }

    public void NewResourceAdded(int tier)
    {
        GameObject newResourceTile = Instantiate(_resourceTilePrefab, transform.Find("Resources"));
        newResourceTile.GetComponent<Tile>().SetTier(tier);
        
        //add score
        switch (tier)
        {
            case 1:
                Score += 2;
                break;
            case 2:
                Score += 4;
                break;
            case 3:
                Score += 8;
                break;
            case 4:
                Score += 16;
                break;
            case 5:
                Score += 32;
                break;
            default: break;
        }
        
        //update text
        _scoreText.text = Score.ToString();
    }
}
