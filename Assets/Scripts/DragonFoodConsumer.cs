using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFoodConsumer : MonoBehaviour {

    private DragonController dragonController;
    private ScoreBoardController scoreBoard;

    public GameObject particleSystemPrefab;

    private void Start()
    {
        dragonController = GetComponent<DragonController>();
        scoreBoard = GameObject.Find("Scoreboard").GetComponent<ScoreBoardController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Food")
        {
            GameObject particleSystemInstance = Instantiate(particleSystemPrefab, other.transform.position, Quaternion.identity);
            particleSystemInstance.AddComponent<AutoDestroy>();
            Destroy(other.gameObject);
            dragonController.StartEating();
            scoreBoard.SetScore(10); // TODO: Set different scores for different food 
        }
    }
}
