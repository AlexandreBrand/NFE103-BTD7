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
        Life.text = LifePoints.ToString(); Gold.text = GoldCoins.ToString();
    }

    private void Update()
    {
    }

    public void SetLife(int life)
    {
        LifePoints = life;
    }
    public void LooseLife(int life)
    {
        LifePoints -= life;
        if (LifePoints < 0)
        {
            LifePoints = 0;
        }
        Life.text = LifePoints.ToString();
    }

    public void SetGold(int gold)
    {
        GoldCoins = gold;
    }

    public void EarnGold(int gold)
    {
        GoldCoins += gold;
        Gold.text = GoldCoins.ToString();
    }

    public void SpendGold(int gold)
    {
        GoldCoins -= gold;
        Gold.text = GoldCoins.ToString();
    }
}
