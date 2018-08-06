using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodConsumer : MonoBehaviour {

    private UnicornController unicornController;
    private ScoreBoardController scoreBoard;

    public GameObject particleSystemPrefab;

    private void Start()
    {
        unicornController = GetComponent<UnicornController>();
        scoreBoard = GameObject.Find("Scoreboard").GetComponent<ScoreBoardController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Food")
        {
            GameObject particleSystemInstance = Instantiate(particleSystemPrefab, other.transform.position, Quaternion.identity);
            particleSystemInstance.AddComponent<AutoDestroy>();
            Destroy(other.gameObject);
            unicornController.StartEating();
            scoreBoard.SetScore(10); // TODO: Set different scores for different food 
        }
    }
}
