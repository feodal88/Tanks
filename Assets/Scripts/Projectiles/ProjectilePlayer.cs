using System;
using UnityEngine;

public class ProjectilePlayer : MonoBehaviour
{
    PlayerTank playerScript;

    float destroyPositionZ = 0.5f;

    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerTank>();
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * playerScript.projectileSpeed);

        if (transform.position.z > destroyPositionZ)
        {
            playerScript.IncreaseCurrentNumberOfBullets();
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.TakeDamage(playerScript.damage);
            gameObject.SetActive(false);
            playerScript.IncreaseCurrentNumberOfBullets();
        }
    }

}
