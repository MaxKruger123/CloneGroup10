using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    public float speed = 3.95f;
    private bool isGrounded;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;

    private bool lerpCrouch;
    private bool crouching;
    public bool sprinting;
    public float crouchTimer;

    public Vector3 moveDirection;

    public Animator weaponAnimation;

    public GunController gunController;
    public Animator crosshairAnim;

    public AudioSource foostepSound;

    public bool bull = true;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if (crouching)
            {
                controller.height = Mathf.Lerp(controller.height, 1, p);
                speed = 2f;
            }
            else
            {
                controller.height = Mathf.Lerp(controller.height, 2, p);
                speed = 3.95f;
            }
            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }

        

    }

    //recieve input from InputManager and apply them to our character controller
    public void ProcessMove(Vector2 input)
    {
        moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
        
        playerVelocity.y = -2f;
        controller.Move(playerVelocity * Time.deltaTime);
        

        if (moveDirection.x == 1 || moveDirection.z == 1 || moveDirection.x == -1 || moveDirection.z == -1)
        {
            
            weaponAnimation.SetBool("isWalking", true);
            crosshairAnim.SetBool("isWalking", true);
            
            if (bull)
            {
                foostepSound.Play();
            }
            bull = false;
            
        }
        else if (moveDirection.x < -0.7 && moveDirection.z > 0.7 || moveDirection.x > 0.7 && moveDirection.z > 0.7 || moveDirection.x < -0.7 && moveDirection.z < -0.7 || moveDirection.x > 0.7 && moveDirection.z < -0.7)
        {
           
            weaponAnimation.SetBool("isWalking", true);
            crosshairAnim.SetBool("isWalking", true);
            
            if (bull)
            {
                foostepSound.Play();
            }
            bull = false;
        }
        else
        {
            weaponAnimation.SetBool("isWalking", false);
            crosshairAnim.SetBool("isWalking", false);
            
            foostepSound.Stop();
            bull = true;
        }



    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
    }

    public void Sprint()
    {
        sprinting = !sprinting;
        if (sprinting){
            speed = 6.6f;
            weaponAnimation.SetBool("isRunning", true);   
            crosshairAnim.SetBool("isSprinting", true);
            gunController._canShoot = false;           
            weaponAnimation.SetBool("isWalking", false);
            gunController.bul = true;
            foostepSound.Stop();
            bull = false;
        }
        else 
        {
            crosshairAnim.SetBool("isSprinting", false);
            crosshairAnim.SetBool("isWalking", true);
            Debug.Log("Stopped sprinting");
            speed = 3.95f;
            gunController._canShoot = true;
            weaponAnimation.SetBool("isRunning", false);
            weaponAnimation.SetBool("isWalking", true);
        }
        
        

    }
}
