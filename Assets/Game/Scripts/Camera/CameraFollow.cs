using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject mainPlayer;
    public float offsetZ = 0f;
    public float offsetY = 0f;
    public float offsetX = 0f;
    public float Multiplier = 1f;
    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, mainPlayer.transform.position + new Vector3(offsetX, offsetY, offsetZ), Time.deltaTime * Multiplier);
    }
}
