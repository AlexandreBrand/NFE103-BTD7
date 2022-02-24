using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObstaclesCreation : MonoBehaviour
{
    public GameObject obstacle;
    private new BoxCollider2D collider;

    [SerializeField] private int maxObstacles;

    private List<GameObject> obstacleTiles = new List<GameObject>();

    public GameObject testEnnemy;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        GameObject newEnnemy = Instantiate(testEnnemy);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 clickPos = new Vector2(
                Mathf.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).x),
                Mathf.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).y));

            checkClickObstacle(clickPos);
        }
    }

    

    private void checkClickObstacle(Vector2 clickPos)
    {
        MapGenerator map = GetComponent<MapGenerator>();

        float y = clickPos.y;
        float x = clickPos.x;

        int counter_x = obstacleTiles.Count(item => item.transform.position.x == x);

        //Cellule occupée
        if (collider != Physics2D.OverlapPoint(clickPos))
        {
            foreach (GameObject obs in obstacleTiles)
            {
                Vector2 pos = obs.transform.position;
                if (pos == clickPos)
                {
                    Destroy(obs);
                    obstacleTiles.Remove(obs);
                    //Debug.Log("Obstacle supprimé");
                    break;
                }
            }
        }

        //Cellule libre - nombre max d'obstacles placés
        else if ( obstacleTiles.Count > maxObstacles || counter_x == map.Height - 1 || x < 0 || y < 0 || x >= map.Width || y >= map.Height)
        {
            //Debug.Log("Placement impossible");
        }

        //Cellule libre - OK pour placement
        else
        {
            createObstacle(clickPos);
        }

    }

    private void createObstacle(Vector2 clickPos)
    {
        MapGenerator map = GetComponent<MapGenerator>();

        GameObject newObstacle = Instantiate(obstacle);
        newObstacle.transform.position = clickPos;
        obstacleTiles.Add(newObstacle);
        AstarPath.active.Scan();
        //Debug.Log("Obstacle créé");

    }

}
