using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private CharacterController _player;
    [SerializeField]
    private float _distance = 10f;    
    [SerializeField]
    private float _moveSpeed = 10f;

    private void Awake()
    {
        transform.position = _player.transform.position;        
        transform.position -= transform.forward * _distance;
    }

    private void LateUpdate()
    {
        MoveToPos();
    }

    private void MoveToPos()
    {
        Vector3 newPos = _player.transform.position - (transform.forward * _distance);

        transform.position = Vector3.Slerp(transform.position, newPos, _moveSpeed * Time.deltaTime);        
    }
}
