using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Camera _camera;
    public List<GameObject> _players;
    private float _maxerBoundary = 0.05f;
    private float _maxBoundary = 0.15f;
    private float _minBoundary = 0.4f;
    private float _cameraSpeedOuter = 0.2f;
    private float _cameraSpeedOut = 0.1f;

	[SerializeField]
    private float _cameraSpeedIn = 0.2f;

	[SerializeField]
    private float _maxDistance = 15f;

    private bool _allowZoom = false;
    
    [SerializeField]
    private GameObject eventUtilsPrefab;

    [SerializeField]
    private UIEventForwarder eventForwarder;

    void Start()
    {
        if (eventUtilsPrefab)
        {
            eventForwarder = eventUtilsPrefab.GetComponent<UIEventForwarder>();
        }

        if (eventForwarder)
        {
            UIEventForwarder.OnForwardedEvent += StartPanEventHandler;
        }
    }

    private void StartPanEventHandler(UIEventArgsBase args)
    {
        if (args != null)
        {
            Type targetType = args.target;
            if (targetType.ToString() == "IntroCameraPan")
            {
                StartCoroutine(RemoveOverlayAndActivatePlayers(1));
            }
        }
    }

    private IEnumerator RemoveOverlayAndActivatePlayers(float seconds)
    {
        yield return new WaitForSeconds(seconds);
		eventForwarder.EnqueueEvent(new UIEventArgsBase(typeof(ActivatePlayers), this.GetType()));

		_allowZoom = true;
	}
    
    void Update()
    {
        if (!_allowZoom) {
            return;
        }

        var allIn = true;
        var totalVector = new Vector3(0, 0, 0);
        foreach (GameObject player in _players) {
            var pos = _camera.WorldToViewportPoint(player.transform.position);
            
            if (pos.x < _maxerBoundary || pos.x > 1 - _maxerBoundary || pos.y < _maxerBoundary || pos.y > 1 - _maxerBoundary) {
                _camera.transform.localPosition = new Vector3(_camera.transform.localPosition.x, _camera.transform.localPosition.y, _camera.transform.localPosition.z - _cameraSpeedOuter);
            }
            else if (pos.x < _maxBoundary || pos.x > 1 - _maxBoundary || pos.y < _maxBoundary || pos.y > 1 - _maxBoundary) {
                _camera.transform.localPosition = new Vector3(_camera.transform.localPosition.x, _camera.transform.localPosition.y, _camera.transform.localPosition.z - _cameraSpeedOut);
            }

            if (_camera.transform.localPosition.z > _maxDistance) {
                _camera.transform.localPosition = new Vector3(_camera.transform.localPosition.x, _camera.transform.localPosition.y, _maxDistance);
            }

            if (pos.x < _minBoundary || pos.x > 1 - _minBoundary || pos.y < _minBoundary || pos.y > 1 - _minBoundary) {
                allIn = false;
            }

            totalVector += player.transform.position;
        }

        totalVector = totalVector / _players.Count;

        if (allIn) {
            _camera.transform.localPosition = new Vector3(_camera.transform.localPosition.x, _camera.transform.localPosition.y, _camera.transform.localPosition.z + _cameraSpeedIn);
        }

        _camera.transform.localPosition = Vector3.Lerp(_camera.transform.localPosition, new Vector3(totalVector.x, totalVector.y, _camera.transform.localPosition.z), 10f);
    }
}