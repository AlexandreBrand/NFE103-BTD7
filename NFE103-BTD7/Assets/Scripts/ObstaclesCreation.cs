using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObstaclesCreation : MonoBehaviour
{

    public GameObject obstacle;
    private new BoxCollider2D collider;

    [SerializeField] private int maxObstacles;

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

            checkClick(clickPos);
        }
    }

    private void checkClick(Vector2 clickPos)
    {
        //Clic sur une cellule occupée
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

        //Clic sur une cellule vide mais nbr max d'obstacles atteint
        else if (obstacleTiles.Count > maxObstacles)
        {
            Debug.Log("Nombre maximum d'obstacles placés");
        }

        //Clic sur une cellule vide
        else
        {
            GameObject newObstacle = Instantiate(obstacle);
            newObstacle.transform.position = clickPos;
            obstacleTiles.Add(newObstacle);
            AstarPath.active.Scan();
            Debug.Log("Obstacle créé");
        }
    }
}
