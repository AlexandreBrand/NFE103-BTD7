using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    private static Game _instance;

    public Text waveState;
    public static Wave wave;

    [SerializeField] public int difficulty;


    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else { Destroy(this); }
    }

    public static Game GetInstance()
    {
        return _instance;
    }


    void Init()
    {
        wave = GetComponent<Wave>();
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
