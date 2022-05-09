using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Bullet : NetworkBehaviour 
{
    public Vector3 velocity;
    public float damage = 10;
    public float lifetime;

    // Start is called before the first frame update
    //void Start()
    //{
    //    Destroy(gameObject, lifetime);
    //}

    // Update is called once per frame
    [Server]
    void Update()
    {
        transform.position += velocity * Time.deltaTime;
    }

    [Server]
    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();

        if (health)
            health.health -= damage;

        NetworkServer.Destroy(gameObject);
    }

    public override void OnStartServer()
    {
        Invoke(nameof(DestroySelf), lifetime);
    }

    [Server]
    void DestroySelf()
    {
        NetworkServer.Destroy(gameObject);
    }
}
