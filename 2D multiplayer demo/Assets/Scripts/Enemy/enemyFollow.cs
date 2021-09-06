using UnityEngine;

public class enemyFollow : MonoBehaviour
{
    [SerializeField]private float speed;

    private GameObject Enemy;
    private bool isFollowing;
    private GameObject player;

    private void Start() 
    {
        Enemy = transform.parent.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            player = other.gameObject;
            isFollowing = true;
        }
    }
    private void Update() 
    {
        if(isFollowing)
        {
            Enemy.transform.position = Vector2.MoveTowards(Enemy.transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    public void StopFollowing()
    {
        isFollowing = false;
    }
}
