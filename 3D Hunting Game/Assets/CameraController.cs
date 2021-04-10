using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Vector3 localPosition;
    [SerializeField]
    private float rotSpeed = 2000;
    private float camRotX;
    private float camRotY;
    // Start is called before the first frame update
    void Start()
    {
        camRotX = Camera.main.transform.eulerAngles.x;
        camRotX = Camera.main.transform.eulerAngles.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.position + localPosition;
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");

        camRotX += h * rotSpeed * Time.deltaTime;
        camRotY += v * rotSpeed * Time.deltaTime;

        camRotY = Mathf.Clamp(camRotY, -90, 90);

        transform.eulerAngles = new Vector3(-camRotY, camRotX);
    }
}
