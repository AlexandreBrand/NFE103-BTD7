using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class QuitGameButton : MonoBehaviour
{

    public void QuitGame()
    {
        Debug.Log("test confirm");
        Game.GetInstance().Quit();
    }
}
