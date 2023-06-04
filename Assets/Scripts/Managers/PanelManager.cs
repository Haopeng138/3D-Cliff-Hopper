using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PanelManager : Singleton<PanelManager>
{

    private List<PanelInstanceModel> _listInstances = new List<PanelInstanceModel>();

    private ObjectPool _objectPool;
    private SceneStateManager _sceneStateManager;

    private void Start()
    {
        Debug.Log("PanelManager Start");
        _objectPool = ObjectPool.Instance;
        _sceneStateManager = SceneStateManager.Instance;
    }

    public void ShowPanel(string panelId,PanelsShowBehaviours behaviour = PanelsShowBehaviours.KEEP_PREVIOUS)
    {
        _sceneStateManager.sceneState = SceneState.PAUSED;
        GameObject panelInstance = _objectPool.GetObjectFromPool(panelId);
        panelInstance.transform.localPosition = Vector3.zero;
        panelInstance.SetActive(true);

        if (panelInstance != null){
           
            if (behaviour == PanelsShowBehaviours.HIDE_PREVIOUS && GetAmountPanelsInQueue() > 0){
                var lastPanel = GetLastPanel();
                if (lastPanel != null){
                    lastPanel.PanelInstance.SetActive(false);
                }
            }

            _listInstances.Add(new PanelInstanceModel
            {
                PanelId = panelId,
                PanelInstance = panelInstance
            });
        }else
        {
            Debug.LogError("Panel with id " + panelId + " not found");
        }
    }

    PanelInstanceModel GetLastPanel(){
        return _listInstances[_listInstances.Count - 1];
    }

    public void HideLastPanel(){
        if (AnyPanelShowing()){
            var lastPanel = GetLastPanel();
            Debug.Log("All panels: " + _listInstances.Count);
            _listInstances.Remove(lastPanel);

            _objectPool.PoolObject(lastPanel.PanelInstance); 

            if (GetAmountPanelsInQueue() > 0){
                lastPanel = GetLastPanel();
                if(lastPanel != null && !lastPanel.PanelInstance.activeInHierarchy){
                    lastPanel.PanelInstance.SetActive(true);
                }
            } 
        }



    }

    public bool AnyPanelShowing(){
        return  GetAmountPanelsInQueue() > 0;
    }

    public int GetAmountPanelsInQueue(){
        return _listInstances.Count;
    }
}
