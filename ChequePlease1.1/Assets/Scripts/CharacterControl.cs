using UnityEngine;
using System.Collections;


public class CharacterControl : MonoBehaviour {

    GameObject m_ThisObject;
    Vector3 temp = new Vector3(0.1f, 0.0f, 0.0f);

	// Use this for initialization
	void Start () {
        m_ThisObject = GameObject.FindGameObjectWithTag("Character");
        Vector3 start = new Vector3(0, 0);
        Vector3 end = new Vector3((Screen.width / 2), -(Screen.height / 2));
        Color color = Color.white;
        Debug.DrawLine(start, end, color);
    }
	
	// Update is called once per frame
	void Update () {
        int nbTouches = Input.touchCount;

        if (nbTouches > 0)
        {
            print(nbTouches + " touch(es) detected");

            for (int i = 0; i < nbTouches; i++)
            {
                Touch touch = Input.GetTouch(i);

                print("Touch index " + touch.fingerId + " detected at position " + touch.position);

                if (touch.position.x < (Screen.width /2))
                {
                    m_ThisObject.transform.position -= temp;
                }
                else if (touch.position.x > (Screen.width / 2))
                {
                    m_ThisObject.transform.position += temp;
                }


            }
        }
    }
}
