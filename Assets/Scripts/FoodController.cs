using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class FoodController : MonoBehaviour {

    private GameObject plane;
    private GameObject foodInstance;
    private float foodAge;
    private readonly float maxAge = 10f;
    private Collider planeCollider;
    private Vector3 planeCenter;
    private Anchor anchor;

    public GameObject[] foodModels;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(plane == null)
        {
            return;
        }

        if(foodInstance == null || foodInstance.activeSelf == false)
        {
            SpawnFoodInstance();
            return;
        }

        foodAge += Time.deltaTime;
        if(foodAge >= maxAge)
        {
            Destroy(foodInstance);
            foodInstance = null;
        }

	}

    public void SetPlane(GameObject obj, Anchor anc)
    {
        plane = obj;
        anchor = anc;
        planeCollider = plane.GetComponent<Collider>();
        planeCenter = planeCollider.bounds.center;
    }

    private void SpawnFoodInstance()
    {
        GameObject foodItem = foodModels[Random.Range(0, foodModels.Length)];

        

        // Pick a location. This is done by selecting a vertex at random and then a random point between it and the center of the plane
        Vector3 foodPosition = new Vector3(planeCenter.x + Random.Range(-0.75f, 0.75f), planeCenter.y, planeCenter.z + Random.Range(-0.75f, 0.75f));
        foodInstance = Instantiate(foodItem, foodPosition, Quaternion.identity, anchor.transform);
        foodAge = 0;
    }
}
