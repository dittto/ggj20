using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Camera _camera;
    public List<GameObject> _players;
    public float _maxBoundary = 0.1f;
    public float _minBoundary = 0.25f;
    public float _cameraSpeedOut = 0.2f;
    public float _cameraSpeedIn = 0.3f;
    
    void Update()
    {
        foreach (GameObject player in _players) {
            var pos = _camera.WorldToViewportPoint(player.transform.position);
            if (pos.x < _maxBoundary || pos.x > 1 - _maxBoundary || pos.y < _maxBoundary || pos.y > 1 - _maxBoundary) {
                _camera.transform.localPosition = new Vector3(_camera.transform.localPosition.x, _camera.transform.localPosition.y, _camera.transform.localPosition.z - _cameraSpeedOut);
            }
        }

        var allIn = true;
        foreach (GameObject player in _players) {
            var pos = _camera.WorldToViewportPoint(player.transform.position);
            if (pos.x < _minBoundary || pos.x > 1 - _minBoundary || pos.y < _minBoundary || pos.y > 1 - _minBoundary) {
                allIn = false;
            }
        }

        if (allIn) {
            _camera.transform.localPosition = new Vector3(_camera.transform.localPosition.x, _camera.transform.localPosition.y, _camera.transform.localPosition.z + _cameraSpeedIn);
        }
    }
}
