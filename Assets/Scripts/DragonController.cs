using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour {

    public GameObject pointer;
    public Camera firstPersonCamera;
    public float pointerSpeed = 20f;
    public float dragonSpeed = 0.5f;

    private Rigidbody rb;
    private Animator animator;
    private bool isEating = false;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        firstPersonCamera = Camera.main;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 pos = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        Ray ray = firstPersonCamera.ScreenPointToRay(pos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {

            if (hit.transform.gameObject.name == "Plane")
            {
                Debug.Log("Hit " + hit.transform.gameObject.name);
                Vector3 pt = hit.point;
                Vector3 pointerPos = pointer.transform.position;
                pointerPos.y = hit.transform.position.y;
                pointer.transform.position = pointerPos;

                pointer.transform.position = Vector3.Lerp(pointer.transform.position, pt, Time.smoothDeltaTime * pointerSpeed);
            }
        }
        if (!isEating)
        {
            float dist = Vector3.Distance(pointer.transform.position, transform.position) - 0.05f;
            if (dist < 0)
            {
                dist = 0;
            }
            Vector3 targetPos = new Vector3(pointer.transform.position.x, transform.position.y, pointer.transform.position.z);
            rb.transform.LookAt(targetPos);
            rb.velocity = transform.localScale.x * transform.forward * dist / 0.5f;
            animator.SetFloat("velocity", rb.velocity.magnitude);
        }

    }


    public void StartEating()
    {
        animator.SetTrigger("Eat");
        isEating = true;
        rb.velocity = Vector3.zero;
    }

    public void DoneEating()
    {
        isEating = false;
    }
}
