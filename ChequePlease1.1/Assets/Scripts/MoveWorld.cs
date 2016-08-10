using UnityEngine;
using System.Collections;

public class MoveWorld : MonoBehaviour {

    [SerializeField]float m_WorldMoveSpeed = 10.0f;

    // Use this for initialization
    void Start() {
    }
	
	// Update is called once per frame
	void Update () {

            gameObject.transform.position -= Vector3.forward * m_WorldMoveSpeed * Time.deltaTime;
	}
}
