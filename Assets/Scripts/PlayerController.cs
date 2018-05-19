using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Rigidbody2D rbody2d;

    public float speed;
    public float jumpSpeed;
    public Vector2 velocity;
    public Vector2 maxVelocity;
    public float gravity;
    public bool onGround;
    public BoxCollider2D boxCollider2d;
    public SpriteRenderer mainRenderer;
    public Animator mainAnimator;

    public LayerMask groundMask;
    public SpriteRenderer interactionIcon;

    public int inRoomTransition;
    public AudioSource walking;
    private float smoothVel;

    private void Awake()
    {
        walking.Play();
        inRoomTransition = 0;
        rbody2d = GetComponent<Rigidbody2D>();
        rbody2d.gravityScale = 0;
       // Application.targetFrameRate = 60;
    }

    private void Start()
    {
        ManagerBase mb = GameObject.FindObjectOfType<ManagerBase>();
        mb.playerController = this;
        interactionIcon.gameObject.SetActive(false);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    // Update is called once per frame
    void Update () {
        if (VirtualController.virtualController.isRightPressed)
        {
            velocity.x = 1;
            mainRenderer.flipX = false;
            if (onGround)
            {
                mainAnimator.SetBool("walking", true);

            }
            walking.volume = Mathf.MoveTowards(walking.volume, 0.55f, Time.deltaTime);
            if (!walking.isPlaying)
            {
                walking.volume = 0;
                walking.Play();
            }
        }
        else if (VirtualController.virtualController.isLeftPressed)
        {
            velocity.x = -1;
            mainRenderer.flipX = true;
            if (onGround)
            {
                mainAnimator.SetBool("walking", true);
            }
            walking.volume = Mathf.MoveTowards(walking.volume, 0.55f, Time.deltaTime);
            if (!walking.isPlaying)
            {
                walking.volume = 0;
                walking.Play();
            }
        }
        else
        {
            if (onGround)
            {
                mainAnimator.SetBool("walking", false);

                velocity.x = 0;
            }
            else
            {
                velocity.x *= 0.89f;
            }
            walking.volume = Mathf.MoveTowards(walking.volume, 0f, Time.deltaTime * 2f);
            if(walking.volume < 0.04f)
            {
                walking.Stop();
            }
        }

        if(VirtualController.virtualController.wasSpacePressed && onGround)
        {
            velocity.y = jumpSpeed;
            mainAnimator.SetTrigger("jumped");
            onGround = false;
        }

        mainAnimator.SetBool("onground", onGround);

        if(inRoomTransition == 1)
        {
            velocity.x = 1;
        }else if(inRoomTransition == -1)
        {
            velocity.x = -1;
        }

        if(!onGround)
        {
            walking.Stop();
        }
	}

    private void GroundCheck(Vector2 newPosition)
    {
        RaycastHit2D rightSideCheck = Physics2D.Raycast(newPosition + Vector2.right * boxCollider2d.size.x * 0.48f, Vector2.down, Mathf.Abs(velocity.y) * Time.deltaTime, groundMask);
        RaycastHit2D leftSideCheck = Physics2D.Raycast(newPosition + Vector2.left * boxCollider2d.size.x * 0.48f, Vector2.down, Mathf.Abs(velocity.y) * Time.deltaTime, groundMask);
        RaycastHit2D centerCheck = Physics2D.Raycast(newPosition, Vector2.down, Mathf.Abs(velocity.y) * Time.deltaTime, groundMask);

        if(rightSideCheck.collider == null && leftSideCheck.collider == null && centerCheck.collider == null)
        {
            onGround = false;
        }
        else
        {
            onGround = true;
        }
    }

    private void FixedUpdate()
    {
        Vector2 newPosition = rbody2d.position;
        float horizontalVelocity = velocity.x;
        velocity.y -= gravity * Time.deltaTime;

        velocity.y = Mathf.Clamp(velocity.y, -maxVelocity.y, maxVelocity.y*30);
        Vector2 finalVelocity = velocity;
        finalVelocity.x *= speed;
        newPosition.x = Mathf.SmoothDamp(rbody2d.position.x, newPosition.x + finalVelocity.x * Time.deltaTime, ref smoothVel, 0.16f);
        newPosition.y = newPosition.y + finalVelocity.y * Time.deltaTime;
        rbody2d.MovePosition(newPosition);

        GroundCheck(rbody2d.position);
    }
}
