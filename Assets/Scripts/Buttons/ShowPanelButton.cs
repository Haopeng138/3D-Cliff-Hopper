using UnityEngine;

public class ShowPanelButton : MonoBehaviour
{
    public string PanelId;

    // Cache the PanelManager instance
    private PanelManager _panelManager;

    public void Start()
    {
        _panelManager = PanelManager.Instance;
    }

    public void DoShowPanel()
    {
        _panelManager.ShowPanel(PanelId);
    }
}
