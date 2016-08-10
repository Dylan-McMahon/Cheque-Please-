using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

    public float m_DestroyHeight;
    public int m_Multiplier;
    
    Player m_Player;

	// Use this for initialization
	void Start()
    {
        m_Player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if (transform.position.y < m_DestroyHeight)
            Destroy(gameObject);
    }

    public virtual void AddedToPlate()
    {

    }

    public virtual void RemovedFromPlate()
    {

    }

    void OnCollisionStay(Collision col)
    {
        Item _item = col.gameObject.GetComponent<Item>();

        m_Player.AddToPlate(gameObject.GetComponent<Item>());
    }

    void OnCollisionExit(Collision col)
    {
        Item _item = col.gameObject.GetComponent<Item>();
        
        m_Player.RemoveFromPlate(gameObject.GetComponent<Item>());
    }

}
