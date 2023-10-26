using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusUIScript : MonoBehaviour
{

    private StatusPanel statusPanel;

    void Start()
    {
        statusPanel = new StatusPanel();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("再用");
            statusPanel.GamePause();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            statusPanel.PushSafe();
        }
    }

    public StatusPanel GetStatusPanel() 
    {
        if (statusPanel != null)  
        {
            return statusPanel;
        }

        Debug.LogWarning("尝试获得StatusPanel失败！");
        return null;
    }
}
