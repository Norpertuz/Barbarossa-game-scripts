using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    private bool detected, sailMode;
    private enum State
    {
        ship,
        character
    }
    private State perspective;
    private GameObject playerGameObject;
    [SerializeField] private GameObject[] cameras; // 0 - hero; 1 - ship
    private Rigidbody rb;

    //zmienne do dialogow
    [SerializeField] GameObject DialogController;
    [SerializeField] GameObject VoiceController;
    bool dialog_mode;
    ////////


    private MovementController running;
    private float threshold = 10f,
        verticalMovement,
        horizontalMovement,
        vertical,
        horizontal;

    private void Start()
    {
        perspective = State.character;
        running = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementController>();
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
    }

    private const float maxSpeedLimit = 0.15f, minSpeedLimit = -0.15f;

    private void Sail()
    {
        vertical = Input.GetAxis("Vertical");
        verticalMovement = Mathf.Lerp(verticalMovement, vertical, Time.deltaTime / threshold);
        if (verticalMovement > maxSpeedLimit) verticalMovement = maxSpeedLimit;
        else if (verticalMovement < minSpeedLimit) verticalMovement = minSpeedLimit;

        // Debug.Log("Speed: " + verticalMovement);

        transform.Translate(0, 0, verticalMovement * 1.25f);

        horizontal = Input.GetAxis("Horizontal");
        horizontalMovement = Mathf.Lerp(horizontalMovement, horizontal * 1.2f, Time.deltaTime / threshold);
        if (horizontalMovement > 0.275f) horizontalMovement = 0.275f;
        else if (horizontalMovement < -0.275f) horizontalMovement = -0.275f;
        //Debug.Log("Rotate: " + horizontalMovement);
        transform.Rotate(0, horizontalMovement, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        verticalMovement = 0;
        Debug.Log("boom");
    }

    private void changeCamera()
    {
        if (perspective == State.character)
        {
            cameras[0].SetActive(false);
            cameras[1].SetActive(true);
            perspective = State.ship;
        } 
        else if (perspective == State.ship)
        {
            cameras[0].SetActive(true);
            cameras[1].SetActive(false);
            perspective = State.character;
        }
    }

    private void setMovementController(State mode)
    {
        if (mode == State.ship)
        {
            DialogController.SetActive(true);
            dialog_mode = true;
            running.enabled = false;
            sailMode = true;
            playerGameObject.transform.SetParent(this.transform);
        }
        else
        {
            dialog_mode = false;

            DialogController.SetActive(false);
            running.enabled = true;
            sailMode = false;
            playerGameObject.transform.parent = null;
        }
    }

    //dialogi na statku
    private void dialogs_onShip(State mode)
    {
        if (mode == State.ship)
        {
            int randomizer = Random.Range(0, 2);
            Debug.Log(randomizer);
            VoiceController.GetComponent<VoiceController>().audio.PlayOneShot(VoiceController.GetComponent<VoiceController>().voices[randomizer]);
            float randomek = Random.Range(10f, 50.0f);
            Invoke("random_voice", randomek);
           randomek = Random.Range(51.0f, 100.0f);
            Invoke("random_voice", randomek);

        }
    }

    void random_voice()
    {
        if (dialog_mode == true)
        {
            int random = Random.Range(0, 2);
            DialogController.GetComponent<DialogController>().audio.PlayOneShot(DialogController.GetComponent<DialogController>().voices[random]);
        }
    }
    ////////////



    /*
    private Transform balance;
    private Vector3 careen;

    private void simulateBalance()
    {
        if(!balance)
        {
            balance = new GameObject("careen").transform;
            balance.SetParent(transform);
        }

        balance.position = careen;
        GetComponent<Rigidbody>().centerOfMass = balance.position;
    }
    */

    //[SerializeField] private GameObject steeringWheel;

    private void FixedUpdate()
    {
        if (sailMode)
        {
            // simulateBalance();
            Sail();
        }
    }

    private void Update()
    {
        // Poprawiæ to
        detected = GameObject.FindGameObjectWithTag("SteeringWheel").GetComponent<SteeringWheel>().detected; //GamesteeringWheel.GetComponent<SteeringWheel>().detected;

        if (Input.GetKeyDown(KeyCode.E) && detected)
        {
            changeCamera();
            setMovementController(perspective);
            dialogs_onShip(perspective);
        }
    }
}
