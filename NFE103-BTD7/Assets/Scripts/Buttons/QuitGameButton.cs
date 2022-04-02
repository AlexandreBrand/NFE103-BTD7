using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class QuitGameButton : MonoBehaviour
{
    [SerializeField] GameObject QuitPanel;
    [SerializeField] GameObject DifficultyPanel;

    public void QuitGame()
    {
        Wave.GetInstance().quitMenu = true;
        Time.timeScale = 0;
        QuitPanel.SetActive(true);
    }

    public void ConfirmQuitGame()
    {
        Wave.GetInstance().endWave();
        Game.GetInstance().GameStarted = false;
        DifficultyPanel.SetActive(true);
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void CancelQuitGame()
    {
        Wave.GetInstance().quitMenu = false;
        if (Wave.GetInstance().paused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        
        QuitPanel.SetActive(false);
    }
}
