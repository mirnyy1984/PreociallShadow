using UnityEngine;
using System.Collections;

public class CollidresTest : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        print("collide with -- " + other.name);
    }
}
