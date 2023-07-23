using UnityEngine;


public class PlayerController : MonoBehaviour
{
    PlayerTank playerTankScript;

    float horizontalInput;
    float boundX = 17;

    void Start()
    {
        playerTankScript = GetComponent<PlayerTank>();       
    }

    void Update()
    {
        if (GameManager.Instanse.gameIsRunning)
        {
            Move();

            if (Input.GetKeyDown(KeyCode.Space))
                playerTankScript.Fire();
        }
    }

    void Move()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * playerTankScript.GetMovementSpeed());

        if (transform.position.x > boundX)
            transform.position = new Vector3(boundX, transform.position.y, transform.position.z);
        else if (transform.position.x < -boundX)
            transform.position = new Vector3(-boundX, transform.position.y, transform.position.z);
    }
   
}
