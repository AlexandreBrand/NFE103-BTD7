using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

public class ObstaclesCreation : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GameObject obstacle = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obstacle.transform.position = new Vector2(10, 4);
        obstacle.layer = 9;
    }

    public Vector2 mousePosition;

    // Update is called once per frame
    void Update()
    {
        //mousePosition = Mouse.current.position.ReadValue();
    }
}
