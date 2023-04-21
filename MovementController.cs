using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class MovementController : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private Transform camera;
    [SerializeField] private Image map;
    [SerializeField] private Sprite[] elementsofmap;
    [SerializeField] private Text win_text;
    [SerializeField] private Text znak_text;
    [SerializeField] private GameObject skarb;
    private Collision myGround;
    private CharacterController controller;
    public int score=0;
    private float 
        smoothVelocity,
        speed, 
        ground = 0.1f, 
        gravity = -9.81f, 
        jump = -1.8f;
    private bool isGrounded;
    private Vector3 target, velocity;
    private Animator anim;
    public float health;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        // jjump0 = false;
        speed = 2f;
        health = 100f;
    }

    private void handleRunning()
    {
        anim.SetFloat("speed", 1, 0.1f, Time.deltaTime);
    }

    private void handleIdle()
    {
        anim.SetFloat("speed", 0, 0.1f, Time.deltaTime);
    }
    private void handleWalking()
    {
        anim.SetFloat("speed", 0.5f, 0.1f, Time.deltaTime);
    }

    public void collecting_map()
    {
        switch (score)
        {
            case 1:
                map.sprite = elementsofmap[1];
                znak_text.gameObject.SetActive(false);
                break;
            case 2:
                map.sprite = elementsofmap[2];
                break;
            case 3:
                map.sprite = elementsofmap[3];
                break;
            case 4:
                map.sprite = elementsofmap[4];
                win_text.gameObject.SetActive(true);
                skarb.SetActive(true);
                break;
        }
       
    }


    /*
    [SerializeField]
    private AudioClip[] sandClip;


    private AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Step()
    {
        //AudioClip clip = GetRandomClip();
        //audioSource.PlayOneShot(clip);
       // audioSource.Play();
    }

    private AudioClip GetRandomClip()
    {
        return sandClip[UnityEngine.Random.Range(0, sandClip.Length)];
    }

    */



    private void Step()
    {
        //AudioClip clip = GetRandomClip();
        //audioSource.PlayOneShot(clip);
        // audioSource.Play();
    }

    /*

    private void FixedUpdate()
    {
        if (controller.isGrounded == true && controller.velocity.magnitude > 2f && audioSource.isPlaying == false)
        {
            audioSource.volume = Random.Range(0.8f, 1);
            audioSource.pitch = Random.Range(0.8f, 1);
            audioSource.Play();
        } else
        {
            audioSource.Stop();
        }
    }
    */

    private void Update()
    {
        // Sprawdü, czy obiekt gracza dotyka powierzchni
        isGrounded = Physics.CheckSphere(transform.position, ground, groundLayerMask);
        if (velocity.y < 0 && isGrounded) velocity.y = -2f;


        // Poruszanie siÍ
        target = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        if (isGrounded)
        {
            if (target == Vector3.zero)
            {
                handleIdle();
            }
            else if (target != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                speed = 6f;
                handleRunning();
            }

            else 
            {
                speed = 1.6f;
                handleWalking();
            }


            // Skakanie
            if (Input.GetKeyDown(KeyCode.Space))
            {
                velocity.y = Mathf.Sqrt(jump * gravity);
                anim.SetBool("jump",true);
            }   
        }
        else
        {
            // jjump0 = false;
            anim.SetBool("jump", false);
        }
        
        
        if (Input.GetKey(KeyCode.LeftControl))
        {
            anim.SetTrigger("kucanie");
        }
        else 
        {
            anim.ResetTrigger("kucanie");
        } 
            
        
        // ZmieÒ pozycjÍ
        if (target.magnitude >= 0.1f)
        {
            //float targetAngle = Mathf.Atan2(target.x, target.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
            //float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothVelocity, 0.1f);
            //transform.rotation = Quaternion.Euler(0f, angle, 0);
            //target = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            target = Camera.main.transform.forward * target.z  + Camera.main.transform.right * target.x;
            controller.Move(target * speed * Time.deltaTime);
        }

        if (target != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(target.x, target.z) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 3f);
        }

        // Wykonaj skok
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
