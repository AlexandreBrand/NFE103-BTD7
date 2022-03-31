using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    private static Game _instance;

    public static Game GetInstance()
    {
        return _instance;
    }

    void Init()
    {
        
    }

    void Start()
    {
        Init();
    }

    public void Lost()
    {
        //TODO fin de partie
    }
}
