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

    public static TextMeshProUGUI error_msg;

    //private static PlaceTower _instance;

    //void Awake()
    //{
    //    _instance = this;
    //}

    //public static PlaceTower GetInstance() {
    //    return _instance; 
    //}


    // Start is called before the first frame update
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

        else if (Wave.GetInstance().waveStarted) { error_msg.text = "Vague en cours"; }

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
                                error_msg.text = "Une tourelle est deja presente";
                                break;
                            }
                            else
                            {
                                Debug.Log("tourelle pos�");
                                createTower(clickPos);
                                Wave.GetInstance().SetPlaceTower(false);
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("tourelle pos� else");
                        createTower(clickPos);
                        Wave.GetInstance().SetPlaceTower(false);
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
