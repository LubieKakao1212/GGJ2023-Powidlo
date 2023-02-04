using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField]
    private int obstacleCountMin = 3;

    [SerializeField]
    private int obstacleCountMax = 5;

    [SerializeField]
    private List<GameObject> obstacles;

    private void Start()
    {
        int count = Random.Range(obstacleCountMin, obstacleCountMax);
        List<int> selected = new List<int>(count);

        for (int i = 0; i < count; i++) 
        {
            int r = Random.Range(0, obstacles.Count - i);
            for(int j = 0; j < i; j++)
            {
                if (r >= selected[j])
                {
                    r++;
                }
                else
                {
                    break;
                }
            }
        }

        int k = 0;

        for (int i = 0; i < obstacles.Count; i++)
        {
            if (selected[k] == i)
            {
                k++;
            }
            else
            {
                obstacles[i].SetActive(false);
            }
        }
    }
}
