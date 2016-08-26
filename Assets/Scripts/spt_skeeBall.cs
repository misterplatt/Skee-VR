﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using VRStandardAssets.Utils;

public class spt_skeeBall : MonoBehaviour {

    Rigidbody rb;
    public float baseThrowForce = 300f;
    public float maxThrowForce = 800f;
    public float respawnTime = 1.25f;

    private float throwForce;
    private bool ballThrown = false;
    private bool ballHeld = false;
    [HideInInspector] public float timer = 0f;
    
    public Image selectionRadial;         // This controls when the selection is complete.

    [SerializeField] private VRInteractiveItem m_InteractiveItem;       // The interactive item for where the user should click to load the level.

    private void OnEnable()
    {
        m_InteractiveItem.OnClick += HandleClick;
        m_InteractiveItem.OnDown += HandleDown;
        m_InteractiveItem.OnUp += HandleUp;
    }

    private void OnDisable()
    {
        m_InteractiveItem.OnClick -= HandleClick;
        m_InteractiveItem.OnDown -= HandleDown;
        m_InteractiveItem.OnUp -= HandleUp;
    }

    private void HandleClick()
    {
        //PICK UP
        if (!ballHeld && !ballThrown)
        {
            rb.velocity = Vector3.zero;
            transform.rotation = Quaternion.Euler(Vector3.zero);
            ballHeld = true;
        }
    }

    private void HandleDown()
    {
        //STORE POWER
        if (ballHeld && Input.GetButton("Fire1") && throwForce < maxThrowForce) {
            throwForce += 5;
            Debug.Log("ThrowForce: " + throwForce);
            //Increment radial to reflect current power
            //MIN: 300 | MAX: 800 | RANGE: 500
            //Normalize the current throwforce to set the radial fillAmount
            selectionRadial.enabled = true;
            selectionRadial.fillAmount = (throwForce - 300) / 500;
        }
    }

    private void HandleUp()
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


    // Use this for initialization
    void Start () {
        //Find radial and rigidbody component, initialize throwForce to base
        selectionRadial = GameObject.Find("UISelectionBar").GetComponent<Image>();
        rb = GetComponent<Rigidbody>();
        throwForce = baseThrowForce;
	}
	
	// Update is called once per frame
	void Update () {

        //Keep the skeeball in front of players at all times
        if (ballHeld) {
            rb.velocity = Vector3.zero;
            transform.rotation = GameObject.Find("HoldPoint").transform.rotation;
            transform.position = GameObject.Find("HoldPoint").transform.position;
        } 

        //After ball is thrown, respawn after [respawnTime] seconds
        if (ballThrown) {
            timer += Time.deltaTime; //Increment timer every frame

            //If the ball has been thrown more than [respawnTime] seconds ago, respawn in ballReturn
            if (timer >= respawnTime)
            {
                //If there are balls remaining, respawn ball
                if (spt_scoreKeeper.ballsRemaining > 0)
                {
                    transform.rotation = Quaternion.Euler(Vector3.zero);
                    transform.position = GameObject.Find("BallRespawnPoint").transform.position;
                    rb.velocity = Vector3.zero;
                    rb.AddForce(0, 0, -50);
                    timer = 0;
                    ballThrown = false;
                    if (!spt_scoreKeeper.infiniteMode) spt_scoreKeeper.ballsRemaining--;
                }
                //Otherwise permanently destroy ball
                else Destroy(gameObject);
            }
        } 
	}
}
