using UnityEngine;

public class ShowPanelButton : MonoBehaviour
{
    public string PanelId;

    // Cache the PanelManager instance
    private PanelManager _panelManager;

    public PanelsShowBehaviours Behaviour;

    public void Start()
    {
      
        _panelManager = PanelManager.Instance;
    
    }

    public void DoShowPanel()
    {
       Debug.Log(_panelManager);
        _panelManager.ShowPanel(PanelId, Behaviour);
    }

}
