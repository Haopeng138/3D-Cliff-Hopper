using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStateManager : Singleton<SceneStateManager>
{
    public enum SceneState {Playing, Paused, GameOver};
    public SceneState sceneState = SceneState.Playing;

    void Awake(){
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(GameObject.Find("AudioManager"));
        DontDestroyOnLoad(GameObject.Find("PopUpsManagers"));

    }

    public void Start()
    {
        _panelManager = PanelManager.Instance;
    }

    public void PauseGame(){
        sceneState = SceneState.Paused;
        Time.timeScale = 0;
        DoShowPanel();
    }

    public void ResumeGame(){
        sceneState = SceneState.Playing;
        Time.timeScale = 1;
        HideLastPanel();
    }

    public void GameOver(){
        sceneState = SceneState.GameOver;
        Time.timeScale = 0;
        DoShowPanel();
    }

    public void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public string PanelId;

    // Cache the PanelManager instance
    private PanelManager _panelManager;

    public PanelsShowBehaviours Behaviour;


    public void DoShowPanel()
    {
        _panelManager.ShowPanel(PanelId, Behaviour);
    }

    public void HideLastPanel()
    {
        _panelManager.HideLastPanel();
    }



    void Update(){
        
        if (Input.GetKeyDown(KeyCode.Escape)){
            switch (sceneState){
                case SceneState.Playing:
                    PauseGame();
                    break;
                case SceneState.Paused:
                    ResumeGame();
                    break;
                case SceneState.GameOver:
                    ResumeGame();
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.R)) {
           
            // Load main menu
            RestartGame();
        }

    }


}
