using UnityEngine;
using System.Collections;

public class BoxSpawner : MonoBehaviour {

    public GameObject[] m_Food;

    public float m_Offset;

    float m_Timer;

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_Timer -= Time.deltaTime;

        if(m_Timer < 0)
        {
            SpawnObject();

            m_Timer = 3;
        }
	}

    void SpawnObject()
    {
        int _index = Random.Range(0, m_Food.Length);

        Instantiate(m_Food[_index], transform.position + new Vector3(Random.Range(-m_Offset, m_Offset), 0), Quaternion.identity);
    }
}
