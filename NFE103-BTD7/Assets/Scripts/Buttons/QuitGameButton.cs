using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class QuitGameButton : MonoBehaviour
{
    [SerializeField] GameObject QuitPanel;

    public void QuitGame()
    {
        Game.wave.quitMenu = true;
        Time.timeScale = 0;
        QuitPanel.SetActive(true);
    }

    public void ConfirmQuitGame()
    {
        Game.wave.endWave(0);
        SceneManager.LoadScene("MenuStart");
    }

    public void CancelQuitGame()
    {
        Game.wave.quitMenu = false;
        Time.timeScale = 1;
        QuitPanel.SetActive(false);
    }
}
