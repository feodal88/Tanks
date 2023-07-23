using System.Collections.Generic;
using UnityEngine;

public class AllyUtility : MonoBehaviour
{
    public static AllyUtility instance;

    [SerializeField] GameObject allyPrefab;
    float zPosition = -13;
    float yPosition = 0.25f;
    float xBounds = 16;
    List<float> usedXPositions;
    float minDistanceBetweenAllies = 2;

    public int count;
    public int bonusHP, bonusDmg;
    public float ASReduceKoef;

    private void Start()
    {
        instance = this;

        ASReduceKoef = 1f;
        bonusDmg = 0;
        bonusDmg = 0;

        usedXPositions = new List<float>();
    }

    public void StartLevel()
    {
        usedXPositions.Clear();
        DestroyAllAllies();
        CreateAlly();
    }

    void DestroyAllAllies()
    {
        GameObject[] allies = GameObject.FindGameObjectsWithTag("Ally");
        foreach (GameObject item in allies)
            Destroy(item);
    }

    void CreateAlly()
    {
        for(int i = 0; i < count; i++)
        {
            GameObject allyGO = Instantiate(allyPrefab, GetRandomPosition(), allyPrefab.transform.rotation);
            Ally ally = allyGO.GetComponent<Ally>();
            ally.UpgradeDamage(bonusDmg);
            ally.UpgradeMaxHP(bonusHP);
            ally.UpgradeAttackSpeed(ASReduceKoef);
        }
    }

    Vector3 GetRandomPosition()
    {
        float xPos;
        bool xPosIsCorrect;

        do 
        {
            xPosIsCorrect = true;
            xPos = Random.Range(-xBounds, xBounds);
            foreach(float usedXPos in usedXPositions)
            {
                if (xPos < usedXPos + minDistanceBetweenAllies && xPos > usedXPos - minDistanceBetweenAllies)
                {
                    xPosIsCorrect = false;
                    break;
                }
            }
        }
        while (!xPosIsCorrect);

        usedXPositions.Add(xPos);

        return new Vector3(xPos, yPosition, zPosition);     
    }
}
