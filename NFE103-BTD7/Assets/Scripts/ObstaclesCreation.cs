using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using Pathfinding;
using UnityEngine.EventSystems;

public class ObstaclesCreation : MonoBehaviour
{
    private new BoxCollider2D collider;
    public GameObject obstacle;
    public static List<GameObject> obstacleTiles = new List<GameObject>();
    public TextMeshProUGUI obs_restants;
    private bool hasCollied = false;

    void Start()
    {
        obstacleTiles.Clear();
        collider = GetComponent<BoxCollider2D>();
        Game.GetInstance().Message.text = "Placez des obstacles";
    }


    void Update()
    {
        onClick();
    }


    private void onClick()
    {
        if (Input.GetMouseButtonDown(0) && !Wave.GetInstance().placeTower)
        {
            Vector2 clickPos;
            clickPos = new Vector2(
                Mathf.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).x),
                Mathf.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).y));

            checkClickObstacle(clickPos);

        }

        if (Wave.GetInstance().waveStarted) { Game.GetInstance().Message.text = "Vague en cours"; }
    }



    private void checkClickObstacle(Vector2 clickPos)
    {
        hasCollied = false;

        if (
            !(obstacleTiles.Count > Wave.GetInstance().maxObstacles - 1) && //Max d'obstacles atteint
            !Wave.GetInstance().quitMenu && //Menu quitter actif
            !OutOfMap(clickPos) &&
            !Game.GetInstance().GameStarted
            )
        {
            checkOccupied(clickPos);

            if (checkPath(clickPos) && !hasCollied)
                createObstacle(clickPos);
        }
    }


    private bool OutOfMap(Vector2 clickPos)
    {
        int h = MapGenerator.GetInstance().Height;
        int w = MapGenerator.GetInstance().Width;

        if (clickPos.x < 0 || clickPos.y < 0 || clickPos.x >= w || clickPos.y >= h)
        {
            return true; //Hors de la map
        }

        return false;
    }


    private void checkOccupied(Vector2 clickPos)
    {
        foreach (GameObject obs in obstacleTiles)
        {
            Vector2 pos = obs.transform.position;
            if (pos == clickPos && !Wave.GetInstance().waveStarted && !checkTower(clickPos))
            {
                destroyObstacle(obs);
                break;
            }
        }
    }


    private bool checkTower(Vector2 clickPos)
    {
        if (PlaceTower.towerTiles != null && PlaceTower.towerTiles.Count > 0)
        {
            foreach (GameObject tower in PlaceTower.towerTiles)
            {
                Vector2 posTower = tower.transform.position;
                if (posTower == clickPos)
                {
                    Game.GetInstance().Message.text = "Vendez la tourelle pour pouvoir retirer l'obstacle";
                    hasCollied = true;
                    return true;
                }
            }
        }
        return false;
    }


    private bool checkPath(Vector2 clickPos)
    {
        GameObject testObstacle = Instantiate(obstacle);
        testObstacle.transform.position = clickPos;
        AstarPath.active.Scan();

        GraphNode node1 = AstarPath.active.GetNearest(MapGenerator.GetInstance().StartC.transform.position, NNConstraint.Default).node;
        GraphNode node2 = AstarPath.active.GetNearest(MapGenerator.GetInstance().EndC.transform.position, NNConstraint.Default).node;

        if (!PathUtilities.IsPathPossible(node1, node2))
        {
            Destroy(testObstacle);
            Game.GetInstance().Message.text = "Placement impossible";
            return false; // Chemin bloqué
        }
        Destroy(testObstacle);

        return true;
    }


    private GameObject createObstacle(Vector2 clickPos)
    {
        GameObject newObstacle = Instantiate(obstacle);
        newObstacle.transform.position = clickPos;
        obstacleTiles.Add(newObstacle);
        Game.GetInstance().Message.text = "Obstacle placé";
        obs_restants.text = "Obstacles restants : " + (Wave.GetInstance().maxObstacles - obstacleTiles.Count());
        AstarPath.active.Scan();

        return newObstacle;
    }


    private void destroyObstacle(GameObject obs)
    {
        Destroy(obs);
        obstacleTiles.Remove(obs);
        Game.GetInstance().Message.text = "Obstacle supprimé";
        obs_restants.text = "Obstacles restants : " + (Wave.GetInstance().maxObstacles - obstacleTiles.Count());
        hasCollied = true;
        AstarPath.active.Scan();
    }
}
