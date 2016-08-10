using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

    [System.Serializable]
    public struct FoodObject
    {
        public GameObject prefab;
        public int frequency;
        public float fallSpeed;
        [Tooltip("It can take up to X much longer for this object to spawn (In seconds)")]
        public float spawnRandomness;
    }

    private float precalculatedX;
    private float precalculatedY;
    private Vector2 spawnPosition;
    //Food Array
    public FoodObject[] food;
    private int totalFrequency = 0;
    private int selectedFood = 0;
    //Spawn timers
    public float spawnDelay;
    public float globalSpawnRandomness;
    private float spawnTimer = 0;

    [Tooltip(@"Value between 1 and 0.
(1 = objects spawning anywhere, 0 = objects only spawning directly center)
This is precalculated so don't cry if you change it during runtime and nothing happens.")]
    public Vector2 sizeBuffer;

    [Tooltip("What do the objects spawn relative to? (Should be the player)")]
    public GameObject anchor;
    public float anchorOffset = 0.0f;
    private Vector3 anchorPosition;


    // Use this for initialization
    void Start()
    {
        //Clamp the buffer
        if (sizeBuffer.x > 1.0f)
            sizeBuffer.x = 1.0f;
        else if (sizeBuffer.x < 0.0f)
            sizeBuffer.x = 0.0f;
        if (sizeBuffer.y > 1.0f)
            sizeBuffer.y = 1.0f;
        else if (sizeBuffer.y < 0.0f)
            sizeBuffer.y = 0.0f;
        //Get various things from the main camera
        float playerZLocation = Camera.main.WorldToScreenPoint(anchor.transform.position).z;
        Vector3 cameraTopLeft = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, Camera.main.pixelHeight, playerZLocation));
        Vector3 cameraTopRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, playerZLocation));
        //Precalculate
        precalculatedX = (cameraTopLeft.x - cameraTopRight.x) * sizeBuffer.x;
        precalculatedY = cameraTopRight.y * sizeBuffer.y;
        spawnPosition.y = precalculatedY + 0.5f;
        //Setup some stuff to do with the food array
        for (int i = 0; i < food.Length; ++i)
        {
            totalFrequency += food[i].frequency;
            food[i].fallSpeed = -food[i].fallSpeed;
        }
        selectedFood = (int)((Random.value * short.MaxValue) % food.Length);
        spawnTimer += (Random.value - 0.5f) * (food[selectedFood].spawnRandomness * 2.0f);
    }

    // Update is called once per frame
    void Update ()
    {
        anchorPosition = anchor.transform.position;

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnDelay)
        {
            spawnTimer -= spawnDelay;
            SpawnPrefab(selectedFood);
            selectedFood = (int)((Random.value * int.MaxValue) % totalFrequency);
            for (int i = 0; i < food.Length; ++i)
            {
                if (selectedFood <= food[i].frequency)
                {
                    selectedFood = i;
                    break;
                }
                else
                {
                    selectedFood -= food[i].frequency;
                }
            }
            spawnTimer += (Random.value - 0.5f) * (globalSpawnRandomness * 2.0f);
            spawnTimer += Random.value * -food[selectedFood].spawnRandomness;
        }
        //Emergency Escape
        if (selectedFood >= food.Length)
        {
            selectedFood = (int)((Random.value * short.MaxValue) % food.Length);
        }
	}

    private void SpawnPrefab(int index)
    {
        GameObject newGameObject = Instantiate<GameObject>(food[index].prefab);
        //Gets a random point in worldspace (X), spawns it offscreen (Y), spawn it on the anchor (Z)
        spawnPosition.x = Camera.main.transform.position.x + ((Random.value * (precalculatedX)) - (precalculatedX / 2.0f));
        newGameObject.transform.position = new Vector3(spawnPosition.x, spawnPosition.y, 0.0f);
        //Set velocity to fall speed

        newGameObject.transform.rotation = Quaternion.identity;

        newGameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, food[index].fallSpeed, 0);
        //Debug.Log(newGameObject.GetComponent<Rigidbody>().velocity);
    }
}
