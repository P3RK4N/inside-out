using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEgg : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) {
        if(other != null)
        {
            Debug.Log("Ante");
            FindObjectOfType<BNG.PlayerTeleport>().TeleportPlayer(FindObjectOfType<EndTP>().transform.position + new Vector3(0,2,0), Quaternion.identity);
        }
    }
}
