using UnityEngine;
using System.Collections;

public class spt_modeBall : spt_skeeBall
{

    private Vector3 origin;

    protected override void Start()
    {
        base.Start();
        origin = transform.position;
    }

    // Update is called once per frame
    override protected void Update()
    {
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
                rb.useGravity = false;
                rb.velocity = Vector3.zero;
                transform.rotation = Quaternion.Euler(Vector3.zero);
                transform.position = origin;
                timer = 0;
                ballThrown = false;
            }
        }
    }
}
