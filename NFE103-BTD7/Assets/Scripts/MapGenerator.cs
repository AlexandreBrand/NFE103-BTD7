using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject mapTile;

    [SerializeField] private int mapWidth;
    [SerializeField] private int mapHeight;
    public int Width => mapWidth;
    public int Height => mapHeight;

    [SerializeField] private GameObject StartCell;
    [SerializeField] private GameObject EndCell;
    public GameObject StartC => StartCell;
    public GameObject EndC => EndCell;

    private List<GameObject> mapTiles = new List<GameObject>();

    private void generateMap()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                GameObject newTile = Instantiate(mapTile);
                mapTiles.Add(newTile);
                newTile.transform.position = new Vector2(x, y);
            }
        }
    }

    void Start()
    {
        generateMap();
        GameObject start = Instantiate(StartCell);
        GameObject end = Instantiate(EndCell);
    }

    void Update()
    {

    }
}
