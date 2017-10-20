using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{

    private Transform boardHolder;
    public GameObject[] objectTiles;
    private List<GameObject> instances = new List<GameObject>();
    private List<Vector3> positions = new List<Vector3>();
    public int radius;
    public float delay = 2f;

    public void BoardSetup()
    {
        boardHolder = new GameObject("Board").transform;
        int count = objectTiles.Length;
        for (int i = 0; i < count; i++)
        {
            positions.Add(Circle(radius, i, count, 0));
            GameObject toInstantiate = objectTiles[i];
            GameObject instance = Instantiate(toInstantiate, positions[i], Quaternion.identity) as GameObject;
            instance.transform.SetParent(boardHolder);
            instances.Add(instance);
        }
    }

    public void resetBoard()
    {
        int count = instances.Count;
        Visibility(true);
        for (int i = 0; i < count; i++)
        {
            positions[i] = Circle(radius, i, count, 0);
            instances[i].transform.SetPositionAndRotation(positions[i], Quaternion.identity);
        }
    }

    Vector3 Circle(float radius, int number, int total, int ang)
    { // create random angle between 0 to 360 degrees 
        float angle = 360 / total;
        Vector3 pos = new Vector3();
        pos.x = radius * Mathf.Sin((number * angle + ang) * Mathf.Deg2Rad);
        pos.y = radius * Mathf.Cos((number * angle + ang) * Mathf.Deg2Rad);
        pos.z = 0;
        return pos;
    }

    public void BoardUpdate()
    {
        int count = instances.Count;
        int randomAngle = Mathf.RoundToInt(Random.value * 90);
        for (int i = 0; i < count; i++)
        {
            positions[i] = Circle(radius, i, count, randomAngle);
            instances[i].transform.SetPositionAndRotation(positions[i], Quaternion.identity);
        }

        StartCoroutine(AfterUpdate());
    }


    public void Visibility(bool visible)
    {
        for (int i = 0; i < instances.Count; i++)
        {
            SpriteRenderer render = instances[i].GetComponentInChildren<SpriteRenderer>();
            render.enabled = visible;
        }
    }

    IEnumerator AfterUpdate()
    {
        yield return new WaitForSeconds(delay);
        Visibility(false);
        GameManager.instance.playersTurn = true;
    }
}
