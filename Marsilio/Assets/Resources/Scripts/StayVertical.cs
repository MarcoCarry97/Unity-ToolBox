using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayVertical : MonoBehaviour
{
    private Vector3 x;
    private Vector3 y;
    private Vector3 z;

    // Start is called before the first frame update
    void Start()
    {
        x = Vector3.right;
        y = Vector3.up;
        z = Vector3.forward;
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.GetContact(0);
        y = contact.normal;
        z = Vector3.Cross(x, y);
        x = Vector3.Cross(y, z);
    }

    private void OnCollisionStay(Collision collision)
    {
        OnCollisionEnter(collision);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float xValue = Mathf.Atan2(x.x, 1);
        float yValue = Mathf.Atan2(y.y, 1);
        float zValue = Mathf.Atan2(y.z, 1);
        Vector3 rot = new Vector3(zValue, xValue, yValue);
        print(rot);
        this.transform.Rotate(rot);
    }

    private void Update()
    {
        Debug.DrawRay(this.transform.position, x * 5,Color.red);
        Debug.DrawRay(this.transform.position, y * 5,Color.yellow);
        Debug.DrawRay(this.transform.position, z * 5,Color.blue);
        Debug.Log(x + " " + y + " " + z);
    }
}
