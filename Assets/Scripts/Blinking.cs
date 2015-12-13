using UnityEngine;
using System.Collections;

public class Blinking : MonoBehaviour {

    private bool blinking = false;

    public float blinkDelay = 0.1f;

	// Use this for initialization
	void Start () {
        StartCoroutine(Blink());
	}

    void Update()
    {
        if (!blinking) StartCoroutine(Blink());
    }
    	
	private IEnumerator Blink()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        blinking = true;

        while (enabled && blinking)
        {
            renderer.enabled = false;
            yield return new WaitForSeconds(blinkDelay);
            renderer.enabled = true;
            yield return new WaitForSeconds(blinkDelay);
        }

        renderer.enabled = true;
        blinking = false;
    }
}
