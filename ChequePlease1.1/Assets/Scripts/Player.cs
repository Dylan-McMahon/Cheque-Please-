using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    Rigidbody m_RigidBody;

    public int m_Score;
    public int m_Multiplier;
    public float m_Speed;
    public float m_TimeLimit;

    public float m_PointFrequency;

    float m_Time;
    float m_Seconds;

    public Text m_ScoreText;
    public Text m_MultiplierText;
    public Text m_TimeText;

    public ArrayList m_Plate = new ArrayList();

    // Use this for initialization
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();

        m_Time = m_TimeLimit;
    }
	
	// Update is called once per frame
	void Update()
    {
        if (Input.GetKey(KeyCode.A))
            m_RigidBody.AddForce(new Vector2(+1, 0) * 5 * m_RigidBody.mass);

        if (Input.GetKey(KeyCode.D))
            m_RigidBody.AddForce(new Vector2(-1, 0) * 5 * m_RigidBody.mass);

        if (Input.GetKey(KeyCode.Space))
            Time.timeScale = 2.0f;
        else
            Time.timeScale = 1.0f;

        int nbTouches = Input.touchCount;

        if (nbTouches > 0)
        {
            print(nbTouches + " touch(es) detected");

            for (int i = 0; i < nbTouches; i++)
            {
                Touch touch = Input.GetTouch(i);

                print("Touch index " + touch.fingerId + " detected at position " + touch.position);
                
                if (touch.position.x < (Screen.width / 2))
                    m_RigidBody.AddForce(new Vector2(+1, 0) * m_Speed * m_RigidBody.mass);
                else
                    m_RigidBody.AddForce(new Vector2(-1, 0) * m_Speed * m_RigidBody.mass);
            }
        }

        if (m_ScoreText)
            m_ScoreText.text = m_Score.ToString();

        if (m_MultiplierText)
            m_MultiplierText.text = "x" + m_Multiplier.ToString() + " Points";

        if (m_TimeText)
            m_TimeText.text = "Time: " + Mathf.Round(m_Time).ToString();

        m_Time -= Time.deltaTime;
        m_Seconds -= Time.deltaTime;

        if(m_Seconds < 0)
        {
            GivePoints(1);

            m_Seconds = m_PointFrequency;
        }

        if(m_Time < 0)
            Time.timeScale = 0;
    }

    public void SetMultiplier(int _amount)
    {
        m_Multiplier = _amount;
    }

    public void GivePoints(int _amount)
    {
        m_Score += _amount * m_Multiplier;
    }

    public int GetScore()
    {
        return m_Score;
    }

    public int GetMultiplier()
    {
        return m_Multiplier;
    }
    
    void OnTriggerStay(Collider other)
    {
        Item _item = other.GetComponent<Item>();

        AddToPlate(_item);
    }

    void OnTriggerExit(Collider other)
    {
        Item _item = other.GetComponent<Item>();

        RemoveFromPlate(_item);
    }

    public void AddToPlate(Item _item)
    {
        if (_item)
            if (!m_Plate.Contains(_item))
            {
                _item.AddedToPlate();
                m_Plate.Add(_item);
            }

        CalculateMultiplier();
    }

    public void RemoveFromPlate(Item _item)
    {
        if (_item)
            if (m_Plate.Contains(_item))
            {
                _item.RemovedFromPlate();
                m_Plate.Remove(_item);
            }

        CalculateMultiplier();
    }

    public bool OnPlate(Item _item)
    {
        if (_item)
            if (m_Plate.Contains(_item))
                return true;

        return false;
    }

    void CalculateMultiplier()
    {
        int _mult = 0;

        foreach (Item i in m_Plate)
                _mult += i.m_Multiplier;

        if (_mult < 0)
            _mult = 0;

        SetMultiplier(_mult);
    }

    public void Pause()
    {
        m_Time = 0;
    }

}
