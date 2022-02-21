using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject mapTile;

    [SerializeField] private int mapWidth;
    [SerializeField] private int mapHeight;

    private List<GameObject> mapTiles = new List<GameObject>();
    private List<GameObject> pathTiles = new List<GameObject>();

    private bool reachedX = false;
    private bool reachedY = false;

    private GameObject currentTile;
    private int currentIndex;
    private int nextIndex;

    public Color pathColor;
    public Color startColor;
    public Color endColor;

    private void moveRight()
    {
        Debug.Log("Mouvement vers la droite");
        pathTiles.Add(currentTile);
        currentIndex = mapTiles.IndexOf(currentTile);
        nextIndex = currentIndex+mapHeight;
        currentTile = mapTiles[nextIndex];
    }

    private void moveUp()
    {
        Debug.Log("Mouvement vers le haut");
        pathTiles.Add(currentTile);
        currentIndex = mapTiles.IndexOf(currentTile);
        nextIndex = currentIndex+1;
        currentTile = mapTiles[nextIndex];
    }

    private void moveDown()
    {
        Debug.Log("Mouvement vers le bas");
        pathTiles.Add(currentTile);
        currentIndex = mapTiles.IndexOf(currentTile);
        nextIndex = currentIndex-1;
        currentTile = mapTiles[nextIndex];
    }

    private void generateMap()
    {
        //Génération de la map -------
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                GameObject newTile = Instantiate(mapTile);
                mapTiles.Add(newTile);
                newTile.transform.position = new Vector2(x, y);
            }
        }
        // ----------

        //Définition cellules de départ et d'arrivée -------

        List<GameObject> startEdgeTiles = getStartEdgeTiles();
        List<GameObject> endEdgeTiles = getEndEdgeTiles();

        GameObject startTile;
        GameObject endTile;

        int randStart = Random.Range(0, mapHeight);
        int randEnd = Random.Range(0, mapHeight);
        
        startTile = startEdgeTiles[randStart];
        endTile = endEdgeTiles[randEnd];

        // ---------

        currentTile = startTile;

        int loopCount = 0;

        while (!reachedY)
        {
            loopCount++;

            if (loopCount > 100)
            {
                Debug.Log("Loop ran too long ! Broke out of it !");
                break;
            }

            if(currentTile.transform.position.y < endTile.transform.position.y) { moveUp(); }
            else if(currentTile.transform.position.y > endTile.transform.position.y) { moveDown(); }
            else { reachedY = true; }
        }

        while (!reachedX)
        {
            if (currentTile.transform.position.x < endTile.transform.position.x) { moveRight(); }
            else { reachedX = true; }
        }

        pathTiles.Add(endTile);

        foreach (GameObject obj in pathTiles)
        {
            obj.GetComponent<SpriteRenderer>().color = pathColor; 
        }

        startTile.GetComponent<SpriteRenderer>().color = startColor;
        endTile.GetComponent<SpriteRenderer>().color = endColor;
    }

    //Liste des cellules de la première colonne
    private List<GameObject> getStartEdgeTiles()
    {
        List<GameObject> edgeTiles = new List<GameObject>();
        for (int i = 0; i < mapHeight; i++)
        {
            edgeTiles.Add(mapTiles[i]);
        }
        return edgeTiles;
    }

    //Liste des cellules de la dernière colonne
    private List<GameObject> getEndEdgeTiles()
    {
        List<GameObject> edgeTiles = new List<GameObject>();
        for (int i = mapHeight * (mapWidth - 1); i < mapHeight * mapWidth; i++)
        {
            edgeTiles.Add(mapTiles[i]);
        }
        return edgeTiles;
    }


    // Start is called before the first frame update
    void Start()
    {
        generateMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
