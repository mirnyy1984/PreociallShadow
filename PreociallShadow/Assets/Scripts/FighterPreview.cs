using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterPreview : MonoBehaviour {

    public List<GameObject> fighters = new List<GameObject>();
    private GameObject selectedFighter;

    private void Start()
    {
        SelectFighter(0);
        StartCoroutine(Rotate());
    }

    public void SelectFighter(int n)
    {
        Destroy(selectedFighter);
        selectedFighter = Instantiate(fighters[n], transform.position, transform.rotation, transform);
    }

    IEnumerator Rotate()
    {
        while (true)
        {
            transform.Rotate(transform.up, 1);
            yield return null;
        }
    }

}
