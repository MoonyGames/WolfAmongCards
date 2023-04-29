using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private Camera _camera;

    [SerializeField]
    private float _speed = 0.5f;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        float newSize = 15f + 10 * (CardsPooler.Instance.Level - 1);

        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, newSize, Time.timeScale * _speed);
    }
}
