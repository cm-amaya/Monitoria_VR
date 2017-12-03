using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class YesClick : MonoBehaviour {

    public float gazeTime = 2f;
    private float timer;
    private bool gazedAt;

	// Use this for initialization
	void Start () {
        timer = 0;
        gazedAt = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(gazedAt)
        {
            timer += Time.deltaTime;

            if(timer>=gazeTime)
            {
                ExecuteEvents.Execute(gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);
            }
        }
	}

    public void enter()
    {
        gazedAt = true;
    }

    public void exit()
    {
        gazedAt = false;
    }

    public void click()
    {
        timer = 0f;
        GameManager.instance.canIPass = true;
        GameManager.instance.nextLevel = true;
        StartCoroutine(clickAnimation());
        Debug.Log("YES");
    }

    IEnumerator clickAnimation()
    {
        //This is a coroutine
        Vector3 temp = transform.position;

        transform.position += new Vector3(0f, 0f, 0.05f);

        yield return new WaitForSeconds(1);

        transform.position = temp;
    }
}
