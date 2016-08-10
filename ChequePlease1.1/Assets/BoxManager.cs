using UnityEngine;
using System.Collections;

public class BoxManager : MonoBehaviour {

    public int m_NumberOfSections;

    public GameObject[] m_SectionList;

    public ArrayList m_Queue = new ArrayList();

    // Use this for initialization
    void Start ()
    {
        for (int i = 0; i < m_NumberOfSections; i++)
        {
            int _index = Random.Range(0, m_SectionList.Length);

            GameObject _object = Instantiate(m_SectionList[_index], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

            m_Queue.Add(_object);

            float _i = _object.GetComponentInChildren<BoxCollider>().bounds.size.magnitude;

            _object.transform.position = new Vector3(0, 0, _i * i);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        GameObject _front = m_Queue[0] as GameObject;
        GameObject _object = m_Queue[m_Queue.Count - 1] as GameObject;

        if (_object.transform.position.z > 0)
        {
            m_Queue.Remove(_front);
            m_Queue.Add(_front);
            
            //_front.
        }

	}
}
