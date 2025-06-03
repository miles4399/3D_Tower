using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObject : MonoBehaviour
{
    [SerializeField] private GameObject _placeable;

    private Camera _camera;
    private Vector3 _cursorPos;
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    public void Place()
    {
        //Debug.Log("Placing block");
        GameObject placeable = Instantiate(_placeable, _cursorPos.Flatten(), Quaternion.identity);
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
