using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShipController : MonoBehaviour
{
    [SerializeField] private PlayerColorId _colorId;
    [SerializeField] private Rigidbody _rigidBody;

    [Space(10)]
    [Header("Control Bindings")]
    [SerializeField] private KeyCode _rotateLeft;
    [SerializeField] private KeyCode _rotateRight;
    [SerializeField] private KeyCode _accelerate;
    [SerializeField] private KeyCode _shootPrimary;

    private bool _isAccelerating = false;
    //=======================================================================
    private void OnValidate() {
        if(_rigidBody == null) _rigidBody = GetComponent<Rigidbody>();
    }
    private void Awake() {
        if (_rigidBody == null) _rigidBody = GetComponent<Rigidbody>();
    }
    private void Start() {
        Init();
    }
    private void Update() {
        HandlePlayerInput();
    }
    private void FixedUpdate() {
        if (_isAccelerating) {
            _rigidBody.AddForce(transform.forward * 100 * Time.fixedDeltaTime);
           // _rigidBody.MovePosition(transform.position + transform.forward * 1 * Time.fixedDeltaTime);
        }
    }
    //=======================================================================
    private void HandlePlayerInput() {
        if (!Input.anyKey) {
            _isAccelerating = false;
            return;
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
    }
    private void Init() {
        Vector3 spawnPos = Vector3.zero;
        Quaternion spawnRotation = Quaternion.identity;
        switch(_colorId) {
            case PlayerColorId.Blue:
                spawnPos = MainCamera.MainCam.ViewportToWorldPoint(new Vector3(0.1f, 0.9f, 10f));
                spawnRotation = Quaternion.Euler(Vector3.up * 135f);
                break;
            case PlayerColorId.Green:
                spawnPos = MainCamera.MainCam.ViewportToWorldPoint(new Vector3(0.9f, 0.1f, 10f));
                spawnRotation = Quaternion.Euler(Vector3.up * -45f);
                break;
            case PlayerColorId.Red:
                spawnPos = MainCamera.MainCam.ViewportToWorldPoint(new Vector3(0.9f, 0.9f, 10f));
                spawnRotation = Quaternion.Euler(Vector3.up * -135f);
                break;
            case PlayerColorId.Yellow:
                spawnPos = MainCamera.MainCam.ViewportToWorldPoint(new Vector3(0.1f, 0.1f, 10f));
                spawnRotation = Quaternion.Euler(Vector3.up * 45f);
                break;
        }
        transform.SetPositionAndRotation(spawnPos, spawnRotation);
    }
}
public enum PlayerColorId {
    Blue, Green, Red, Yellow
}
