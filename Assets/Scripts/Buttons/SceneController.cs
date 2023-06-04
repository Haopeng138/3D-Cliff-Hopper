using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    private SceneStateManager sceneStateManager;
    private PanelManager _panelManager;

    // Start is called before the first frame update
    void Start()
    {
        sceneStateManager = SceneStateManager.Instance;
        _panelManager = PanelManager.Instance;
    }

    public void RestartGame()
    {
        sceneStateManager.RestartGame();
        _panelManager.HideLastPanel();
        
    }

    public void ResumeGame()
    {
        sceneStateManager.ResumeGame();
        _panelManager.HideLastPanel();
    }
}
