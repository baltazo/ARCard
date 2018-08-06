using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour {

    private float lifetime = 2f;
    private float birthTime;

	// Use this for initialization
	void Start () {
        birthTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(birthTime + lifetime < Time.time)
        {
            Destroy(gameObject);
        }
	}

}
