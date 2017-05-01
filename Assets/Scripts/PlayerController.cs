using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;

    private Rigidbody rb;
    public string horizontalMove;
    public string verticalMove;
    public string action;
    public int playerNum;
    public bool isActive;
    public new SkinnedMeshRenderer renderer;

    public LineRenderer lightning;


    private float timeLimit = 3.0f;

    Coroutine coFlash;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isActive = true;
        renderer = GetComponentInChildren<SkinnedMeshRenderer>();
        lightning = transform.FindChild("SimpleLightningBoltPrefab" + playerNum).gameObject.GetComponent<LineRenderer>();
        lightning.enabled = false;
    }

    void FixedUpdate()
    {
        // Get input movement directions
        float moveHorizontal = Input.GetAxisRaw(horizontalMove);
        float moveVertical = Input.GetAxisRaw(verticalMove);

        // Set movement velocity
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement.normalized * speed;
        if (moveHorizontal != 0 || moveVertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(movement, Vector3.up);
        }

        if (Input.GetButtonDown(action) && isActive)
        {
            startFlash();
            isActive = false;
            // show lightning
            lightning.enabled = true;
            punishPlayer();
        }

        if (!isActive)
        {
            if (timeLimit < 2.9)
            {
                lightning.enabled = false;
            }
            if (timeLimit <= 0.0)
            {
                isActive = true;
                StopCoroutine(coFlash);
                renderer.enabled = true; // make sure renderer is on
                timeLimit = 3.0f;
            }
            else
            {
                timeLimit -= Time.deltaTime;
            }
        }
    }

    // Update is called once per frame
    void Update () {

    }

    void punishPlayer()
    {
        
        int plusMinus = Random.Range(1, 6);

        if (ScoreBehavior.PlayerScores[playerNum - 1] > 7 + plusMinus)
        {

            for (int i = 0; i < 7 + plusMinus; i++)
            {
                GameObject coin = Instantiate(Resources.Load("coin", typeof(GameObject)), transform.position + Vector3.up, Random.rotation) as GameObject;
            }
            ScoreBehavior.PlayerScores[playerNum - 1] = ScoreBehavior.PlayerScores[playerNum - 1] - 10;
        }
        else
        {
            
            for (int i = 0; i < ScoreBehavior.PlayerScores[playerNum - 1]; i++)
            {
                GameObject coin = Instantiate(Resources.Load("coin", typeof(GameObject)), transform.position + Vector3.up, Random.rotation) as GameObject;
            }
            ScoreBehavior.PlayerScores[playerNum - 1] = 0;
        }
        
        rb.AddExplosionForce(5f, transform.position, 5.0f, 5.0f);
    }

    public void beAbsorbed()
    {
        
        punishPlayer();

        transform.Translate(Vector3.down * 10f, Space.World);
        rb.isKinematic = true;

    }

    /**
     * startFlash() creates and instantiates a coroutine called Flash under coFlash 
     */
    public void startFlash()
    {
        coFlash = StartCoroutine(Flash(5f, 0.1f));
    }

    /**
     * Flash is a coroutine which causes the model renderer to turn on and off repeatedly to give of the
     * flashing frames look when a player is hit
     */
    IEnumerator Flash(float time, float intervalTime)
    {
        float elapsedTime = 0f;
        int index = 0;
        while (elapsedTime < time)
        {
            renderer.enabled = ((index % 2) == 0);//colors[index % 2];

            elapsedTime += Time.deltaTime;
            index++;
            yield return new WaitForSeconds(intervalTime);
        }
    }
}
