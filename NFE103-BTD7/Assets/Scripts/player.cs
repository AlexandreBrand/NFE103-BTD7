using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{

    private static Player _instance;

    public int LifePoints;
    public int GoldCoins;
    [SerializeField] TextMeshProUGUI Life;
    [SerializeField] TextMeshProUGUI Gold;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else { Destroy(this); }
    }

    public static Player GetInstance() { return _instance; }

    private void Start()
    {
        Life.text = LifePoints.ToString();
        Gold.text = GoldCoins.ToString();
    }
}
