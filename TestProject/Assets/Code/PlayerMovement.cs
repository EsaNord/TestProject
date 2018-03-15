using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 5f;
    [SerializeField]
    private Transform _playerTransform;

    private CharacterController _charCont;
    private Vector3 _movement;    

    private void Awake()
    {
        _charCont = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {        
        // Reseting movement vector to prevent sliding.
        _movement = Vector3.zero;

        _movement = new Vector3(-Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));

        if (_movement != Vector3.zero)
        {
            Vector3 newRotation = _movement;
            transform.rotation = Quaternion.LookRotation(newRotation);
            _playerTransform.rotation = Quaternion.LookRotation(newRotation);

            float offsetY = _playerTransform.eulerAngles.y;
            _playerTransform.eulerAngles = new Vector3(_playerTransform.eulerAngles.x,
                offsetY + 45, _playerTransform.eulerAngles.z);

            _charCont.Move((transform.forward + transform.right) * _moveSpeed * Time.deltaTime);

            // Move animation.
        } 
        
        else
        {
            // Idle animations.
        }            
    }
}