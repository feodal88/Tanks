using UnityEngine;

public class ProjectileEnemy : MonoBehaviour
{
    public Enemy owner;
    Tank tankScript;

    float destroyPositionZ = -20.5f;

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * owner.projectileSpeed);
        if (transform.position.z < destroyPositionZ)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Ally"))
        {
            if (other.CompareTag("Player"))
                tankScript = other.GetComponent<PlayerTank>();
            else
                tankScript = other.GetComponent<Ally>();

            tankScript.TakeDamage(owner.damage);
            gameObject.SetActive(false);
        }
    }
}
