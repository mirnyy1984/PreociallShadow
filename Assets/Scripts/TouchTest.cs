using UnityEngine;
using System.Collections;

public class TouchTest : MonoBehaviour
{
    public Transform Cursor;
    public bool IsTouched;
    public float Speed;
    private Vector3 _mousePosition;
    private Vector3 _startposition;
    private float _distanceToCamera;

    private void Start()
    {
        _distanceToCamera = Camera.main.transform.position.z - Cursor.position.z;
        _startposition = Cursor.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            IsTouched = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            IsTouched = false;
        }

        if (IsTouched)
        {
            Vector3 mouseposition = Input.mousePosition;

            Cursor.position = Vector3.MoveTowards(Cursor.position, mouseposition, Speed);
        }
        else
        {
            Cursor.position = Vector3.MoveTowards(Cursor.position, _startposition, Speed);
        }

    }


}
