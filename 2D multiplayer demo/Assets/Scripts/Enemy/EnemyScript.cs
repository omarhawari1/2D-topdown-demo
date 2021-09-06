using Mirror;
using UnityEngine;

public class EnemyScript : NetworkBehaviour
{
    [SerializeField]private enemyFollow enemyFollowScript;
    [SerializeField]private GameObject ammoBoxPrefab;
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.collider.tag == "Bullet")
        {
            ServerSpawnAmmoBox();
            ServerDestroyEnemy(gameObject);
        }
        if(other.collider.tag == "Player")
        {
            enemyFollowScript.StopFollowing();
        }
    }

    [ServerCallback]
    private void ServerDestroyEnemy(GameObject Enemy)
    {
        NetworkServer.Destroy(Enemy);
    }
    [ServerCallback]
    private void ServerSpawnAmmoBox()
    {
        GameObject ammoBoxInstance = Instantiate(ammoBoxPrefab, transform.position, ammoBoxPrefab.transform.rotation);
        NetworkServer.Spawn(ammoBoxInstance);
    }
}
