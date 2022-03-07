using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] private int lifePoints;
    [SerializeField] private int goldCoins;

    public static MapGenerator map;
    public Text waveState;
    public static Wave wave;


    void Init()
    {
        
        wave = GetComponent<Wave>();
        map = GetComponent<MapGenerator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
