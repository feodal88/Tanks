using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] int t0_start;
    [SerializeField] int t1_start;

    int t0;
    int t1;

    public void Init()
    {
        t0 = t0_start;
        t1 = t1_start;
    }

    public int GetEnemyNumberToSpawn()
    {
        int enemyLeftBefore = EnemiesLeft();
        int chamceToSpawnEasier = 70;
        int tankNumber = -1;
        while (enemyLeftBefore == EnemiesLeft())
        {

            if (t0 > 0 && Random.Range(0, 100) <= chamceToSpawnEasier)
            {
                t0--;
                tankNumber = 0;
            }
            else if (t1 > 0)
            {
                t1--;
                tankNumber = 1;
            }
        }

        return tankNumber;
    }

    public int EnemiesLeft()
    {
        return t0 + t1;
    }
}
