using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class PlaceObject : MonoBehaviour
{
    [SerializeField] private float _placeDelay = 0.1f;

    private GameObject _placeable;
    private Camera _camera;
    private Vector3 _cursorPos;
    private bool _canPlace = false;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    //public void selectPlaceable(GameObject prefab)
    //{

    //}
    public void SelectPlaceable(GameObject prefab)
    {
        _placeable = prefab;
        StartCoroutine(PlacementDelay(_placeDelay));   
    }

    public void TryPlace()
    {
        if (_canPlace)
        {
            Instantiate(_placeable, _cursorPos.Flatten(), Quaternion.identity);
            _canPlace = false;
        }
    }

    private IEnumerator PlacementDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _canPlace = true;
    }

    // Update is called once per frame
    void Update()
    {
 

        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            _cursorPos = hit.point;
        }
    }
}
 