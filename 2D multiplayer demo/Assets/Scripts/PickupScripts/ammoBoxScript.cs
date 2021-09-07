using Mirror;
using UnityEngine;

public class ammoBoxScript : NetworkBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            PlayerScript playerScript = other.gameObject.GetComponent<PlayerScript>();
            playerScript.AmmoPickUp();
            playerScript.CmdDestroyAmmoBox(gameObject);
        }
    }
}
