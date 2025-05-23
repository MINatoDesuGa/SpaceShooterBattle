using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShipController : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private Rigidbody _rigidBody;

    [Space(10)]
    [Header("Control Bindings")]
    [SerializeField] private KeyCode _rotateLeft;
    [SerializeField] private KeyCode _rotateRight;
    [SerializeField] private KeyCode _accelerate;
    [SerializeField] private KeyCode _brake;
    [SerializeField] private KeyCode _shootPrimary;

    private bool _isAccelerating = false, _isHoldingBrake = false;
    //=======================================================================
    private void OnValidate() {
        if(_rigidBody == null) _rigidBody = GetComponent<Rigidbody>();
    }
    private void Awake() {
        if (_rigidBody == null) _rigidBody = GetComponent<Rigidbody>();
    }
    private void Update() {
        HandlePlayerInput();
    }
    private void FixedUpdate() {
        if (_isAccelerating) {
            _rigidBody.MovePosition(transform.position + transform.forward * 1 * Time.fixedDeltaTime);
        } else {
            
        }

        if (_isHoldingBrake) {
            _rigidBody.MovePosition(transform.position - transform.forward * 1 * Time.fixedDeltaTime);
        } else {

        }
    }
    //=======================================================================
    private void HandlePlayerInput() {
        if (!Input.anyKey) {
            _isHoldingBrake = _isAccelerating = false;
        }
        if (Input.GetKey(_rotateLeft)) {
            transform.rotation = Quaternion.Euler((transform.rotation.eulerAngles - Vector3.up * 1)); 
        }
        if (Input.GetKey(_rotateRight)) {
            transform.rotation = Quaternion.Euler((transform.rotation.eulerAngles + Vector3.up * 1));
        }
        if (Input.GetKeyDown(_accelerate)) {
            _isAccelerating = true;
        }
        if (Input.GetKeyDown(_brake)) {
            _isHoldingBrake = true;
        }
    }
}
