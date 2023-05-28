using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePanelButton : MonoBehaviour
{
    // Cache the PanelManager instance
    private PanelManager _panelManager;

    public void Start()
    {
        _panelManager = PanelManager.Instance;
    }

    public void HideLastPanel()
    {
        _panelManager.HideLastPanel();
    }
}
