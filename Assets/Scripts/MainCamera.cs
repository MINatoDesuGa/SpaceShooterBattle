using UnityEngine;
public class MainCamera : MonoBehaviour
{
    public static Camera MainCam = null;
    private void Awake() {
        MainCam = GetComponent<Camera>();
    }
}
