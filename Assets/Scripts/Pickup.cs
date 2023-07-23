using UnityEngine;

public class Pickup : MonoBehaviour
{
    float speed = 3;
    float destroyPositionZ = -20.5f;
    public int coinValue_start;
    int coinValue;
    public int hpValue;

    private void Start()
    {
        coinValue = coinValue_start + GameManager.Instanse.bonusGoldIncome;
    }

    void Update()
    {
        transform.Translate(-Vector3.up * Time.deltaTime * speed);

        if (transform.position.z < destroyPositionZ)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instanse.UpdateCoinsCount(coinValue);
            other.GetComponent<Tank>().TakeDamage(-hpValue);
            Destroy(gameObject);
        }
    }
}
