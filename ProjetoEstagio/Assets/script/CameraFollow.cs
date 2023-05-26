using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public float cameraSpeed;

    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 followCamera = new Vector3(player.transform.position.x, player.transform.position.y);
        transform.position = Vector3.Slerp(transform.position, followCamera, cameraSpeed * Time.deltaTime);
    }
}
