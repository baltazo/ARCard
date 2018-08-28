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
    private int animalChosen;

    public GameObject[] unicornFood;
    public GameObject[] dragonFood;

    private GameObject[] foodList;

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

    public void Setup(GameObject obj, Anchor anc, int animalIndex)
    {
        plane = obj;
        anchor = anc;
        animalChosen = animalIndex; // 0-1 Unicorn 2-3 Dragon
        planeCollider = plane.GetComponent<Collider>();
        planeCenter = planeCollider.bounds.center;

        switch (animalChosen)
        {
            case 0:
                foodList = unicornFood;
                break;

            case 1:
                foodList = unicornFood;
                break;

            case 2:
                foodList = dragonFood;
                break;

            case 3:
                foodList = dragonFood;
                break;
        }

    }

    private void SpawnFoodInstance()
    {
        GameObject foodItem = foodList[Random.Range(0, unicornFood.Length)];


        // Pick a location. This is done by selecting a vertex at random and then a random point between it and the center of the plane
        Vector3 foodPosition = new Vector3(planeCenter.x + Random.Range(-0.75f, 0.75f), planeCenter.y, planeCenter.z + Random.Range(-0.75f, 0.75f));
        foodInstance = Instantiate(foodItem, foodPosition, Quaternion.identity, anchor.transform);
        foodAge = 0;
    }
}
