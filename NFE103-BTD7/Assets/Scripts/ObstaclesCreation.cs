using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObstaclesCreation : MonoBehaviour
{
    public GameObject obstacle;

    private new BoxCollider2D collider;

    private List<GameObject> obstacleTiles = new List<GameObject>();



    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
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
        Wave wave = GetComponent<Wave>();

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
                    Debug.Log("Obstacle supprimé");
                    break;
                }
            }
        }

        //Placement impossible (hors grille, max obstacles, chemin bloqué)
        else if (
            obstacleTiles.Count > wave.maxObstacles - 1 || //Max d'obstacles placés
            obstacleTiles.Count(item => item.transform.position.x == clickPos.x) == map.Height - 1 || //Colonne bloquée
            clickPos.x < 0 || clickPos.y < 0 || clickPos.x >= map.Width || clickPos.y >= map.Height) // Hors de la grille
        { Debug.Log("Placement impossible"); }

        //Cellule libre - OK pour placement
        else if (wave.waveStarted) { Debug.Log("Vague en cours"); }
        else { createObstacle(clickPos); }

    }

    private void createObstacle(Vector2 clickPos)
    {
        GameObject newObstacle = Instantiate(obstacle);
        newObstacle.transform.position = clickPos;
        obstacleTiles.Add(newObstacle);
        AstarPath.active.Scan();
        Debug.Log("Obstacle créé");

    }

}
