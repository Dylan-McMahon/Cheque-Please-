using UnityEngine;
using System.Collections;

public class PointSpawner : MonoBehaviour {

    public GameObject m_Point;

    public float m_Offset;
    public float m_Density;

    float m_Time;
    float m_Timer;

    // Use this for initialization
    void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_Time += Time.deltaTime;
        m_Timer -= Time.deltaTime;

        if (m_Timer < 0)
        {
            SpawnObject();

            m_Timer = m_Density;
        }
    }

    void SpawnObject()
    {
        Instantiate(m_Point, transform.position + new Vector3((Mathf.Sin(m_Time) * m_Offset), 0, 0), Quaternion.identity);
    }
}
