using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour {
    

    public Image fader;
    public Slider progressBar;
    public float progressBarStep;
    public float fadeDuration = 0.5f; //Длительность фейда в секундах

    bool loaded = false;
    
	void Awake () {
        Color color = Color.black;
        fader.color = color;
	}

    private void Start()
    {
        StartCoroutine(ProgressBar());
        StartCoroutine("FadeIn");
    }

    private void Update()
    {
        if (loaded)
        {
            loaded = false;
            StartCoroutine("FadeOut");
        }
    }

    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(0.3f);
        Color newColor = fader.color;
        while (true)
        {
            newColor.a -= (1 / fadeDuration) * Time.deltaTime;
            fader.color = newColor;

            if (newColor.a <= 0f)
            {
                yield break;
            }
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        Color newColor = fader.color;
        while (true)
        {
            newColor.a += (1 / fadeDuration) * Time.deltaTime;
            fader.color = newColor;

            if (newColor.a >= 1f)
            {
                print("Loading complete");
                yield break;
            }
            yield return null;
        }
    }

    IEnumerator ProgressBar()
    {
        yield return new WaitForSeconds(1 + fadeDuration);

        while (true)
        {
            progressBar.value += Random.Range(0.1f, 0.2f);

            yield return new WaitForSeconds(Random.Range(0.25f, 0.5f));

            if (progressBar.value >= 1.0f)
            {
                loaded = true;
                yield break;
            }
            yield return null; 
        }
    }
}
