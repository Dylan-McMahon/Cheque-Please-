using UnityEngine;
using System.Collections;

public class Point : MonoBehaviour {

    public int m_Point;

    public int m_Speed;

    Rigidbody m_RigidBody;

    // Use this for initialization
    void Start ()
    {
        m_RigidBody = GetComponent<Rigidbody>();

        m_RigidBody.velocity = new Vector3(0, 0, -m_Speed);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player _player = other.gameObject.GetComponent<Player>();

            if (_player)
            {
                _player.GivePoints(m_Point);
                Destroy(gameObject);
            }
        }
    }
}
