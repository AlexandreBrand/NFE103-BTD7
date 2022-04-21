using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class PlaceTower : MonoBehaviour
{

    //@TODO mettre ? jour avec l'interface
    public GameObject obstacle;

    public GameObject tower;

    //private new BoxCollider2D collider;

    public static List<GameObject> towerTiles = new List<GameObject>();

    void Start()
    {
        towerTiles.Clear();
        //collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Wave.GetInstance().GetPlaceTower())
        {
            Vector2 clickPos = new Vector2(
                Mathf.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).x),
                Mathf.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).y));

            checkClickOnObstacleTower(clickPos);
        }
    }



    public void checkClickOnObstacleTower(Vector2 clickPos)
    {
        int h = MapGenerator.GetInstance().Height;
        int w = MapGenerator.GetInstance().Width;

        //Placement impossible (hors grille, max obstacles, chemin bloqu?)
        if (
            towerTiles.Count(item => item.transform.position.x == clickPos.x) == h - 1 || //Colonne bloqu?e
            towerTiles.Count(item => item.transform.position.x == clickPos.x) == h - 1 || //Colonne bloqu?e
            clickPos.x < 0 || clickPos.y < 0 || clickPos.x >= w || clickPos.y >= h) // Hors de la grille
        { Debug.Log("Placement impossible"); }

        else if (Wave.GetInstance().waveStarted) { Game.GetInstance().Message.text = "Vague en cours"; }

        else if (Wave.GetInstance().quitMenu) { /*Debug.Log("Menu Quitter la partie actif");*/}
        else if (Wave.GetInstance().diff_select)
        { }

        //check if on osbtacle
        //else if (collider != Physics2D.OverlapPoint(clickPos))
        else
        {
            foreach (GameObject obs in ObstaclesCreation.obstacleTiles)
            {
                Vector2 posObs = obs.transform.position;

                if (posObs == clickPos)
                {
                    if (towerTiles.Count > 0)
                    {
                        foreach (GameObject tower in towerTiles)
                        {

                            Vector2 posTower = tower.transform.position;
                            if (posTower == clickPos)
                            {
                                Game.GetInstance().Message.text = "Il y a déjà une tourelle";
                                break;
                            }
                            else
                            {
                                Game.GetInstance().Message.text = "Tourelle placée";
                                Debug.Log("Tourelle placée");
                                createTower(clickPos);
                                Wave.GetInstance().SetPlaceTower(false);
                            }
                        }
                    }
                    else
                    {
                        Game.GetInstance().Message.text = "Tourelle placée";
                        Debug.Log("Tourelle placée else");
                        createTower(clickPos);
                        Wave.GetInstance().SetPlaceTower(false);
                        Wave.GetInstance().placeTower = false;
                    }
                }
            }
        }
    }

    private void createTower(Vector2 clickPos)
    {
        GameObject newTower = Instantiate(tower);
        if (Player.GetInstance().SpendGold(newTower.GetComponent<Tower>().GetPrice()))
        {
            newTower.transform.position = clickPos;
            towerTiles.Add(newTower);
        }   
    }
}
