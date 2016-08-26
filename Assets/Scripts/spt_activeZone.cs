using UnityEngine;
using System.Collections;

public class spt_activeZone : MonoBehaviour {

    //When the ball falls through the hole, increase score by scoreValue
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "skeeBall")
        {
            col.gameObject.GetComponent<spt_skeeBall>().timer = 0;
        }
    }
}
