using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using VRStandardAssets.Utils;

public class spt_skeeBall : MonoBehaviour {

    //Force constants
    public float baseThrowForce = 300f;
    public float maxThrowForce = 800f;

    //Respawn time
    public float respawnTime = 1.25f;

    protected Rigidbody rb;
    protected float throwForce;
    protected bool ballThrown = false;
    protected bool ballHeld = false;
    [HideInInspector] public float timer = 0f;
    
    public Image selectionRadial;         // This controls when the selection is complete.

    [SerializeField] private VRInteractiveItem m_InteractiveItem;       // The interactive item for where the user should click to load the level.

    virtual protected void OnEnable()
    {
        m_InteractiveItem.OnClick += HandleClick;
        m_InteractiveItem.OnDown += HandleDown;
        m_InteractiveItem.OnUp += HandleUp;
    }

    virtual protected void OnDisable()
    {
        m_InteractiveItem.OnClick -= HandleClick;
        m_InteractiveItem.OnDown -= HandleDown;
        m_InteractiveItem.OnUp -= HandleUp;
    }

    // Use this for initialization
    virtual protected void Start()
    {
        //Find radial and rigidbody component, initialize throwForce to base
        selectionRadial = GameObject.Find("UISelectionBar").GetComponent<Image>();
        rb = GetComponent<Rigidbody>();
        throwForce = baseThrowForce;
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        //Destroy skeeball in hand on gameOver
        if (ballHeld && spt_scoreKeeper.gameOver) {
            selectionRadial.fillAmount = 0;
            selectionRadial.enabled = false;
            Destroy(gameObject);
        } 
        //Keep the skeeball in front of players at all times
        if (ballHeld)
        {
            rb.velocity = Vector3.zero;
            transform.rotation = GameObject.Find("HoldPoint").transform.rotation;
            transform.position = GameObject.Find("HoldPoint").transform.position;
        }

        //After ball is thrown, respawn after [respawnTime] seconds
        if (ballThrown)
        {
            timer += Time.deltaTime; //Increment timer every frame

            //If the ball has been thrown more than [respawnTime] seconds ago, respawn in ballReturn
            if (timer >= respawnTime)
            {
                //If there are still ball left to spawn, respawn ball
                if (spt_scoreKeeper.ballsToSpawn > 0 || spt_scoreKeeper.infiniteMode)
                {
                    transform.rotation = Quaternion.Euler(Vector3.zero);
                    transform.position = GameObject.Find("BallRespawnPoint").transform.position;
                    rb.velocity = Vector3.zero;
                    rb.AddForce(0, 0, -50);
                    timer = 0;
                    ballThrown = false;
                }
                //Otherwise permanently destroy ball
                else {
                    Destroy(gameObject);
                }
                if (!spt_scoreKeeper.infiniteMode) spt_scoreKeeper.ballsRemaining--;
            }
        }
    }

    virtual protected void HandleClick()
    {
        //PICK UP
        if (!ballHeld && !ballThrown && !spt_scoreKeeper.gameOver)
        {
            rb.velocity = Vector3.zero;
            transform.rotation = Quaternion.Euler(Vector3.zero);
            ballHeld = true;
        }
    }

    virtual protected void HandleDown()
    {
        //STORE POWER
        if (ballHeld && Input.GetButton("Fire1") && throwForce < maxThrowForce) {
            throwForce += 5;
            //MIN: 300 | MAX: 800 | RANGE: 500
            //Normalize the current throwforce to set the radial fillAmount
            selectionRadial.enabled = true;
            selectionRadial.fillAmount = (throwForce - 300) / 500;
        }
    }

    virtual protected void HandleUp()
    {
        //FIRE AT STORED POWER
        if (ballHeld) {
            //Throw ball
            rb.useGravity = true;
            Vector3 finalForce = throwForce * transform.forward;
            rb.AddForce(finalForce);
            //Reset throw force and ball state
            throwForce = baseThrowForce;
            ballThrown = true;
            ballHeld = false;
            //Disable radial and return it to 0
            selectionRadial.fillAmount = 0;
            selectionRadial.enabled = false;
        }
    }

}
