using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldInstancing : MonoBehaviour
{

    //Create list
    public Queue<GameObject> MapQueue = new Queue<GameObject>();
    float offset;
    // Use this for initialization
    void Start()
    {
        //populate list
        GameObject FirstMap = Instantiate(Resources.Load("LevelSectionBlank 1", typeof(GameObject))) as GameObject;
        FirstMap.transform.position = new Vector3(0, 0, 0);

        offset = FirstMap.GetComponentInChildren<BoxCollider>().bounds.size.magnitude - 2;

        GameObject SecondMap = Instantiate(Resources.Load("LevelSectionBlank 1", typeof(GameObject))) as GameObject;
        SecondMap.transform.position = FirstMap.transform.position + new Vector3(0, 0, offset);

        GameObject ThirdMap = Instantiate(Resources.Load("LevelSectionBlank 1", typeof(GameObject))) as GameObject;
        ThirdMap.transform.position = SecondMap.transform.position + new Vector3(0, 0, offset);

        MapQueue.Enqueue(FirstMap);
        MapQueue.Enqueue(SecondMap);
        MapQueue.Enqueue(ThirdMap);


    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(offset);
        Debug.Log(MapQueue.Peek().transform.position.z);
        
        //Check and reset queue
        if (MapQueue.Peek().transform.position.z < -(offset * 1.5))
        {
            GameObject Temp = MapQueue.Dequeue();
            Temp.transform.position = new Vector3(0, 0, offset * 1.5f);
            MapQueue.Enqueue(Temp);
        }
    }
}
