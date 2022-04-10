using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class PlaceTower : MonoBehaviour
{
    //@TODO mettre ? jour avec l'interface
    public GameObject obstacle;

    public GameObject tower;

    private new BoxCollider2D collider;

    private static List<GameObject> towerTiles = new List<GameObject>();

    public Text error_msg;

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

            checkClickOnObstacle(clickPos);
        }
    }



    private void checkClickOnObstacle(Vector2 clickPos)
    {
        int h = MapGenerator.GetInstance().Height;
        int w = MapGenerator.GetInstance().Width;

        

        //Placement impossible (hors grille, max obstacles, chemin bloqu?)
        if (
            towerTiles.Count(item => item.transform.position.x == clickPos.x) == h - 1 || //Colonne bloqu?e
            towerTiles.Count(item => item.transform.position.x == clickPos.x) == h - 1 || //Colonne bloqu?e
            clickPos.x < 0 || clickPos.y < 0 || clickPos.x >= w || clickPos.y >= h) // Hors de la grille
        { /*Debug.Log("Placement impossible");*/  }

        else if (Wave.GetInstance().waveStarted) { error_msg.text = "Vague en cours"; }

        //check if on osbtacle
        else if(collider != Physics2D.OverlapPoint(clickPos))
        {
            foreach (GameObject obs in ObstaclesCreation.obstacleTiles)
            {
                Vector2 posObs = obs.transform.position;
                if (posObs == clickPos)
                {
                    foreach (GameObject tower in towerTiles)
                    {
                        Vector2 posTower = tower.transform.position;
                        if (posTower == clickPos)
                        {
                            error_msg.text = "Une tourelle est d?j? pr?sente";
                            break;
                        }
                        else
                        {

                        }
                    }
                }
            }
        }
    }

    private void createObstacle(Vector2 clickPos)
    {
        GameObject newTower = Instantiate(tower);
        newTower.transform.position = clickPos;
        towerTiles.Add(newTower);
        error_msg.text = "Tower cr??";
    }
}
