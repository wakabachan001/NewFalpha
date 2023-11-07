using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class antiHoming : MonoBehaviour
{
    GameObject playerObj;
    PlayerManager player;
    Transform playerTransform;

    private Camera mainCamera;
    public float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.GetComponent<PlayerManager>();
        playerTransform = playerObj.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && player.GetComponent<Rigidbody>().velocity.magnitude > 0)
        {
            Vector3 direction = (transform.position - playerObj.transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            Vector3 viewportPoint = mainCamera.WorldToViewportPoint(transform.position);
            if (viewportPoint.x < 0 || viewportPoint.x > 1 || viewportPoint.y < 0 || viewportPoint.y > 1)
            {
                transform.position -= direction * speed * Time.deltaTime;
            }
        }
    }
}
