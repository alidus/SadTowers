using UnityEngine;
using System.Collections;

public class explosion_destroy : MonoBehaviour {
    private float delay = 3;
    private float timer = 0;

	// Update is called once per frame
	void Update () {
	if (timer >= delay)
            {
            Destroy(gameObject);
            }
    else
            {
            timer += Time.deltaTime;
            }
	}
}
