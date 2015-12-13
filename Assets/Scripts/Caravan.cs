using UnityEngine;
using System.Collections;

public class Caravan : MonoBehaviour {

    private bool dying;
    private float deathTimer;

    public float timeToDie;
    public float deathThresholdVelocity;
    public float deathFriction;

	// Use this for initialization
	void Start () {
        dying = false;
        deathTimer = float.MaxValue;
    }

	// Update is called once per frame
	void Update () {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        float speed = rb.velocity.magnitude;

        if(dying)
        {
            if (speed > deathThresholdVelocity + deathFriction)
                StartDying();
            else
            {
                deathTimer -= Time.deltaTime;
                if (deathTimer <= 0)
                    onDeath();
            }
        }
        else
        {
            if (speed <= deathThresholdVelocity)
                StartDying();
        }
	}

    private void updateDying()
    {

    }

    private void StartDying()
    {
        dying = true;
        deathTimer = timeToDie;
        GetComponent<Blinking>().enabled = true;
    }

    private void StopDying()
    {
        dying = false;
        deathTimer = float.MaxValue;
        GetComponent<Blinking>().enabled = false;
    }

    private void onDeath()
    {
        Destroy(gameObject);
    }
}
