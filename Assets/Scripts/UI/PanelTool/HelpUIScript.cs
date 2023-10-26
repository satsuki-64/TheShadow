using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpUIScript : MonoBehaviour
{

    HelpPanel helpPanel;
    // Start is called before the first frame update
    void Start()
    {
        helpPanel = new HelpPanel();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            helpPanel.ExitHelpPanel();
        }
    }
}
