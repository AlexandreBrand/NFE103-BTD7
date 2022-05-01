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
        Game.GetInstance().Quit();
    }

    public void ConfirmQuitGame()
    {
        Game.GetInstance().ConfirmQuit();
    }

    public void CancelQuitGame()
    {
        Game.GetInstance().CancelQuit();
    }
}
