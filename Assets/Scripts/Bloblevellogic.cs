using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloblevellogic : MonoBehaviour {

    private GameObject[] players;
    bool uDied = false;
    // Use this for initialization
    void Start() {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update() {

        int total = 0;
        foreach (GameObject player in players)
        {
            if (player.transform.position.y < -4)
            {
                total++;
            }
        }

        if (total >= 4)
        {
            uDied = true;
            //endfunction something
        }
        OnGUI();
    }


    private void OnGUI()
    {
        string endText = "You all died";
        if (uDied)
        {
            GUI.Label(new Rect(Screen.width/2, Screen.height/2, Screen.width - 20, 30), endText);
        }
    }

}
