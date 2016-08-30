using UnityEngine;
using System.Collections;

public class spt_activeZone : MonoBehaviour {

    //While the ball is rolling near the holes don't despawn
    void OnTriggerStay(Collider col) {
        if (col.gameObject.tag == "skeeBall") {
            col.gameObject.GetComponent<spt_skeeBall>().timer = 0;
        }
    }
}
