using UnityEngine;
using Mirror;

public class PlayerScript : NetworkBehaviour
{
    [SerializeField]private float speed = 10;
    [SerializeField]private GameObject bulletPrefab = null;
    [SerializeField]private GameObject firePoint = null;
    [SerializeField]private float bulletsInAmmoBox = 5;


    private Rigidbody2D rb = null;
    private bool hasGun = false;
    private float ammo = 10;

    private void Start() 
    {
        if(!isLocalPlayer){return;}
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() 
    {
        if(!isLocalPlayer){return;}
        Move();
    }
    private void Update() 
    {
        if(!isLocalPlayer){return;}
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Move()
    {
        float MoveX = Input.GetAxis("Horizontal");
        float MoveY  = Input.GetAxis("Vertical");
        Vector2 mousePos = Input.mousePosition;

        rb.MovePosition(new Vector2((transform.position.x + MoveX * speed * Time.fixedDeltaTime),
            transform.position.y + MoveY * speed * Time.fixedDeltaTime));
        
        Vector2 lookPos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        mousePos.x = mousePos.x - lookPos.x;
        mousePos.y = mousePos.y - lookPos.y;
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
    }
    private void Shoot()
    {
        if(hasGun && ammo > 0)
        {
            CmdSpawnBullet();
        }
    }

    [Command]
    public void CmdSpawnBullet()
    {
        GameObject bulletInstance = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
        NetworkServer.Spawn(bulletInstance, connectionToClient);
    }

    public void gunPickedUp(GameObject gun)
    {
        hasGun = true;
        Destroy(gun);   
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.collider.tag == "Enemy")
        {
            Destroy(gameObject);
        }
        if(other.collider.tag == "ammoBox")
        {
            ammo += bulletsInAmmoBox;
        }
    }
}