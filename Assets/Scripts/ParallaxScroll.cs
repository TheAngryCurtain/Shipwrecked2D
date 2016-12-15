using UnityEngine;
using System.Collections;

public class ParallaxScroll : MonoBehaviour
{
    [SerializeField] private Transform[] _backgroundContainers;
    [SerializeField] private Transform _cameraTransform;

    [SerializeField] private float _smoothing;

    private float[] _parallaxScales;
    private Vector3 _previousCamPosition;

    private void Start()
    {
        _previousCamPosition = _cameraTransform.position;

        // set parallaxing scale to the (negative) z value of the container
        // this way, moving an object back in the scene dictates the fast the scroll effect is
        _parallaxScales = new float[_backgroundContainers.Length];
        for (int i = 0; i < _parallaxScales.Length; i++)
        {
            _parallaxScales[i] = _backgroundContainers[i].position.z * -1f;
        }
    }

    private void Update()
    {
        for (int i = 0; i < _backgroundContainers.Length; i++)
        {
            float parallax = (_previousCamPosition.x - _cameraTransform.position.x) * _parallaxScales[i];
            float targetPosX = _backgroundContainers[i].position.x + parallax;

            Vector3 targetPos = _backgroundContainers[i].position;
            targetPos.x = targetPosX;

            _backgroundContainers[i].position = Vector3.Lerp(_backgroundContainers[i].position, targetPos, _smoothing * Time.deltaTime);
        }

        _previousCamPosition = _cameraTransform.position;
    }
}
