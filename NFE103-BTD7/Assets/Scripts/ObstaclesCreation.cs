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
    public TextMeshProUGUI obs_restants;


    void Start()
    {
        obstacleTiles.Clear();
        obs_restants.text = "Obstacles restants : " + Wave.GetInstance().maxObstacles.ToString();
        collider = GetComponent<BoxCollider2D>();
        Game.GetInstance().Message.text = "Placez des obstacles";
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
        if (Wave.GetInstance().waveStarted) { Game.GetInstance().Message.text = "Vague en cours"; }
        obs_restants.text = "Obstacles restants : " + (Wave.GetInstance().maxObstacles - obstacleTiles.Count());
    }

    private void checkClickObstacle(Vector2 clickPos)
    {
        int h = MapGenerator.GetInstance().Height;
        int w = MapGenerator.GetInstance().Width;
        bool hasCollied = false;


        if (obstacleTiles.Count > Wave.GetInstance().maxObstacles - 1 || checkPath(clickPos) || Wave.GetInstance().quitMenu) { }
        else if (Game.GetInstance().GameStarted) { Game.GetInstance().Message.text = "Vague en cours"; }
        //else if (Wave.GetInstance().quitMenu) { }

        //Cellule libre - OK pour placement
        else
        {
            foreach (GameObject obs in obstacleTiles)
            {
                Vector2 pos = obs.transform.position;
                if (pos == clickPos && !Wave.GetInstance().waveStarted)
                {
                    if (PlaceTower.towerTiles != null && PlaceTower.towerTiles.Count > 0)
                    {
                        foreach (GameObject tower in PlaceTower.towerTiles)
                        {
                            Vector2 posTower = tower.transform.position;
                            if (posTower == clickPos)
                            {
                                Game.GetInstance().Message.text = "Il ya déjà une tourelle";
                                hasCollied = true;
                                break;
                            }
                            else
                            {
                                Destroy(obs);
                                obstacleTiles.Remove(obs);
                                Game.GetInstance().Message.text = "Obstacle supprimé";
                                hasCollied = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        Destroy(obs);
                        obstacleTiles.Remove(obs);
                        Game.GetInstance().Message.text = "Obstacle supprimé";
                        hasCollied = true;
                        break;
                    }
                }
            }
            if (!hasCollied)
            {
                createObstacle(clickPos);
            }
        }
    }

    private bool checkPath(Vector2 clickPos)
    {
        //TODO test si le placement va bloquer le chemin

        int h = MapGenerator.GetInstance().Height;
        int w = MapGenerator.GetInstance().Width;

        if (clickPos.x < 0 || clickPos.y < 0 || clickPos.x >= w || clickPos.y >= h)
        {
            return true; //Hors de la map
        }
        
        if (obstacleTiles.Count(item => item.transform.position.x == clickPos.x) == h - 1)
        {
            return true; // Colonne bloquée
        }
        return false;
    }

    private void createObstacle(Vector2 clickPos)
    {
        GameObject newObstacle = Instantiate(obstacle);
        newObstacle.transform.position = clickPos;
        obstacleTiles.Add(newObstacle);
        Game.GetInstance().Message.text = "Obstacle placé";
        AstarPath.active.Scan();
    }
}
