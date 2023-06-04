using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStateManager : Singleton<SceneStateManager>
{
    public enum SceneState {START,PLAYING, PAUSED, GAMEOVER};
    public static SceneState sceneState = SceneState.START;

    public string PanelId;
    // Cache the PanelManager instance
    private PanelManager _panelManager;
    public PanelsShowBehaviours Behaviour;


    void Awake(){
        DontDestroyOnLoad(GameObject.Find("AudioManager"));
        DontDestroyOnLoad(GameObject.Find("PopUpsManagers"));
    }

    public void Start()
    {
        _panelManager = PanelManager.Instance;
        Time.timeScale = 1;
    }

    public void DoShowPanel()
    {
        _panelManager.ShowPanel(PanelId, Behaviour);
    }

    public void HideLastPanel()
    {
        _panelManager.HideLastPanel();
    }



    void Update(){
        
        switch(sceneState){
            case SceneState.START:
                if (Input.GetKeyDown(KeyCode.Space)){
                    sceneState = SceneState.PLAYING;
                }
                break;
            case SceneState.PLAYING:
                if (Input.GetKeyDown(KeyCode.Escape)){
                    PauseGame();
                }
                if (Input.GetKeyDown(KeyCode.M)){
                    sceneState = SceneState.GAMEOVER;
                }
                break;
            case SceneState.PAUSED:
                if (Input.GetKeyDown(KeyCode.Escape)){
                    ResumeGame();
                }
                break;
            case SceneState.GAMEOVER:
                Debug.Log("Game Over");
                break;
        }

        if (Input.GetKeyDown(KeyCode.R)){
            RestartGame();
        }
        if (Input.GetKeyDown(KeyCode.F)){
            PlayerPrefs.DeleteAll();
        }
    }
    public void PauseGame(){
        sceneState = SceneState.PAUSED;
        Time.timeScale = 0;
        DoShowPanel();
    }

    public void ResumeGame(){
        sceneState = SceneState.PLAYING;
        Time.timeScale = 1;
        HideLastPanel();
    }

    public void GameOver(){
        sceneState = SceneState.GAMEOVER;
        Time.timeScale = 0;
        DoShowPanel();
    }

    public void RestartGame(){
        sceneState = SceneState.START;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
