using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSpawner : MonoBehaviour
{
    public GameObject Rope;
    public PlayerMovement player;
    public float respawnTime = 1.0f;
    private Vector2 screenBounds;
    public Camera mainCamera;

    private Vector2 spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        StartCoroutine(ropeSpawn());
        spawnPoint = Rope.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        spawnPoint.x += 10;
    }
    private void spawnRope()
    {
        GameObject r = Instantiate(Rope) as GameObject;
        r.transform.position = new Vector2(spawnPoint.x, 13f);
    }
    IEnumerator ropeSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            spawnRope();
        }
    }
}
