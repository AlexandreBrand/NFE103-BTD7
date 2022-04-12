using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using Pathfinding;

public class ObstaclesCreation : MonoBehaviour
{
    private new BoxCollider2D collider;
    public GameObject obstacle;
    public static List<GameObject> obstacleTiles = new List<GameObject>();

    public TextMeshProUGUI error_msg;
    public TextMeshProUGUI obs_restants;


    void Start()
    {
        obstacleTiles.Clear();
        obs_restants.text = "Obstacles restants : " + Wave.GetInstance().maxObstacles.ToString();
        collider = GetComponent<BoxCollider2D>();
        error_msg.text = "Placez des obstacles";
    }


    void Update()
    {
        //if (Input.GetMouseButtonDown(0) && !Wave.GetInstance().placeTower)
        if (Input.GetMouseButtonDown(0) && !Wave.GetInstance().placeTower)
        {
            Vector2 clickPos;
            clickPos = new Vector2(
                Mathf.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).x),
                Mathf.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).y));

            checkClickObstacle(clickPos);   
        }
        if (Wave.GetInstance().waveStarted) { error_msg.text = "Vague en cours"; }
        obs_restants.text = "Obstacles restants : " + (Wave.GetInstance().maxObstacles - obstacleTiles.Count());
    }

    

    private void checkClickObstacle(Vector2 clickPos)
    {
        int h = MapGenerator.GetInstance().Height;
        int w = MapGenerator.GetInstance().Width;

        //GraphNode node1 = AstarPath.active.GetNearest(MapGenerator.GetInstance().StartC.transform.position, NNConstraint.Default).node;
        //GraphNode node2 = AstarPath.active.GetNearest(MapGenerator.GetInstance().EndC.transform.position, NNConstraint.Default).node;

        //Cellule occupée
        if (collider != Physics2D.OverlapPoint(clickPos))
        {
            foreach (GameObject obs in obstacleTiles)
            {
                Vector2 pos = obs.transform.position;
                if (pos == clickPos && !Wave.GetInstance().waveStarted)
                {
                    Destroy(obs);
                    obstacleTiles.Remove(obs);
                    error_msg.text = "Obstacle supprimé";
                    break;
                }
                else
                {
                    error_msg.text = "Vague en cours";
                }
            }
        }

        else if (
            obstacleTiles.Count > Wave.GetInstance().maxObstacles - 1 || //Max d'obstacles placés
            obstacleTiles.Count(item => item.transform.position.x == clickPos.x) == h - 1 || //Colonne bloquée
            clickPos.x < 0 || clickPos.y < 0 || clickPos.x >= w || clickPos.y >= h) // Hors de la grille
        { /*Debug.Log("Placement impossible");*/  }
        else if (Wave.GetInstance().waveStarted && Game.GetInstance().GameStarted) { error_msg.text = "Vague en cours"; }
        else if (Wave.GetInstance().quitMenu) { /*Debug.Log("Menu Quitter la partie actif");*/}
        else if (Wave.GetInstance().diff_select) { /*Debug.Log("Menu Sélection de la difficulté actif");*/}
        /*else if (PathUtilities.IsPathPossible(node1, node2))
        {
            Debug.Log("chemin possible");
            createObstacle(clickPos);
        }*/

        //Cellule libre - OK pour placement
        else
        {
            createObstacle(clickPos);
        }
    }

    private void createObstacle(Vector2 clickPos)
    {
        GameObject newObstacle = Instantiate(obstacle);
        newObstacle.transform.position = clickPos;
        obstacleTiles.Add(newObstacle);
        error_msg.text = "Obstacle placé";
        AstarPath.active.Scan();
    }
}
