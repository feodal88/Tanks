using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAlly : MonoBehaviour
{
    public GameObject target;
    public Ally owner;

    public float speed = 5f;
    float maxPositionZ = 0.5f;
    float minPositionZ = -20.5f;
    float boundsPositionX = 20;

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
            transform.LookAt(target.transform);
        }
        else
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }

        if (transform.position.z > maxPositionZ || transform.position.z < minPositionZ || transform.position.x > boundsPositionX ||
            transform.position.x < -boundsPositionX)
            gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.TakeDamage(owner.damage);
            gameObject.SetActive(false);
        }
    }
}
