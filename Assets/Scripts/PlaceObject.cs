using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObject : MonoBehaviour
{
    [SerializeField] private GameObject _placeable;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Place()
    {
        //Debug.Log("Placing block");
        GameObject placeable = Instantiate(_placeable, Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
