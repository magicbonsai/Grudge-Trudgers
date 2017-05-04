using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bloblevellogic : MonoBehaviour
{

    private GameObject[] players;
    private PlayerController[] playerControllers;
    private GameObject enemy;
    bool uDied = false;
    private bool isCutscene;
    private float openTimer;
    private UIBehaviour UIcanvas;
    // Use this for initialization
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("enemy");
        UIcanvas = GameObject.FindGameObjectWithTag("UI").GetComponent<UIBehaviour>();
        isCutscene = true;
        openTimer = 4f;
        openingScene();
    }

    // Update is called once per frame
    void Update()
    {

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
    }

    private void FixedUpdate()
    {
        if (isCutscene)
        {
            foreach (GameObject player in players)
            {
                player.transform.Translate(Vector3.forward * Time.deltaTime * 2.5f);

            }

            openTimer -= Time.deltaTime;

            if (openTimer < 2)
            {
                UIcanvas.setInstructions("Watch out for the blob! It will grow and grow while chasing the closest player. " +
                    "But you can make him shrink from shyness if at least 2 of you stare at him! You have 2 minutes to survive.");
                
            }

            if (openTimer < 0)
            {
                isCutscene = false;
                foreach (GameObject player in players)
                {
                    //player.GetComponent<PlayerController>().lightning.enabled = false;
                    player.GetComponent<PlayerController>().enabled = true;

                }
                
                enemy.GetComponent<BlobController>().enabled = true;

            }
        }
    }


    private void OnGUI()
    {
        string endText = "You all died";
        if (uDied)
        {
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, Screen.width - 20, 30), endText);
        }
    }

    private void openingScene()
    {
        foreach (GameObject player in players)
        {
            //player.GetComponent<PlayerController>().lightning.enabled = false;
            player.GetComponent<PlayerController>().enabled = false;
            enemy.GetComponent<BlobController>().enabled = false;

        }

    }
}

    
