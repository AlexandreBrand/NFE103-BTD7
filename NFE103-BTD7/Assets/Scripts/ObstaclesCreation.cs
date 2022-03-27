using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class ObstaclesCreation : MonoBehaviour
{
    public GameObject obstacle;

    private new BoxCollider2D collider;

    public static List<GameObject> obstacleTiles = new List<GameObject>();

    public TextMeshProUGUI error_msg;
    public TextMeshProUGUI obs_restants;

    // Start is called before the first frame update
    void Start()
    {
        obs_restants.text = "Obstacles restants : " + Wave.GetInstance().maxObstacles.ToString();
        collider = GetComponent<BoxCollider2D>();
        error_msg.text = "Placez des obstacles";
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 clickPos;
            clickPos = new Vector2(
                Mathf.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).x),
                Mathf.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).y));

            checkClickObstacle(clickPos);
            obs_restants.text = "Obstacles restants : " + (Wave.GetInstance().maxObstacles - obstacleTiles.Count());
        }

        if (Wave.GetInstance().waveStarted) { error_msg.text = "Vague en cours"; }
    }

    

    private void checkClickObstacle(Vector2 clickPos)
    {
        int h = MapGenerator.GetInstance().Height;
        int w = MapGenerator.GetInstance().Width;

        //Cellule occupée
        if (collider != Physics2D.OverlapPoint(clickPos))
        {
            foreach (GameObject obs in obstacleTiles)
            {
                Vector2 pos = obs.transform.position;
                if (pos == clickPos && !Game.wave.waveStarted)
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

        //Placement impossible (hors grille, max obstacles, chemin bloqué)
        else if (
            obstacleTiles.Count > Game.wave.maxObstacles - 1 || //Max d'obstacles placés
            obstacleTiles.Count(item => item.transform.position.x == clickPos.x) == h - 1 || //Colonne bloquée
            clickPos.x < 0 || clickPos.y < 0 || clickPos.x >= w || clickPos.y >= h) // Hors de la grille
        { /*Debug.Log("Placement impossible");*/  }
        else if (Game.wave.waveStarted) { error_msg.text = "Vague en cours"; }
        else if (Game.wave.quitMenu) { /*Debug.Log("Menu Quitter la partie actif");*/}

        //Cellule libre - OK pour placement
        else { createObstacle(clickPos); }
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
