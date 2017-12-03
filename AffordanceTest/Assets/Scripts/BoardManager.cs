using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardManager : MonoBehaviour {

    public Vector3 origin;

    public GameObject pole;

    private GameObject leftPole;
    private GameObject rightPole;

    private float[] distances;
    private float[] distancesPoles;
    public float[] orderDistances;
    public float[] orderPoles;


    public void BoardSetup()
    {
        origin =new Vector3(0f, 0.5f, -1.5f);
        distances = new float[3] { 3f, 4.5f, 6f };
        distancesPoles = new float[6] { 0.25f, 0.3f, 0.35f, 0.4f, 0.45f, 0.5f };

        leftPole = Instantiate(pole, origin, Quaternion.identity) as GameObject;
        rightPole = Instantiate(pole, origin, Quaternion.identity) as GameObject;

        generateRandomOrder();
        Debug.Log("Setup Done");

        changeDistance(orderDistances[0], orderPoles[0]);
    }

    void generateRandomOrder()
    {
        orderDistances = new float[18];
        orderPoles = new float[18];

        System.Random rnd = new System.Random();
        var numbers = Enumerable.Range(0, 18).OrderBy(r => rnd.Next()).ToArray();
        for (int i = 0; i < distancesPoles.Length; i++)
        {
            for (int j = 0; j < distances.Length; j++)
            {
                int number = numbers[i * distances.Length + j];
                orderDistances[number] = distances[j];
                orderPoles[number] = distancesPoles[i];
            }
        }
    }

    void changeDistance(float distance, float between)
    {
        float middle = between / 2;
        leftPole.transform.position = origin + new Vector3(-middle, 0f, distance);
        rightPole.transform.position = origin + new Vector3(middle, 0f, distance);
    }

    public void BoardUpdate(int level)
    {
        changeDistance(orderDistances[level-1], orderPoles[level-1]);
    }


}
