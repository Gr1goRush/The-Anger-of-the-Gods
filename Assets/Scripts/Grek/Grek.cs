using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Grek : MonoBehaviour
{
    public static Grek singenton;
    public float speed = 3;
    public Rigidbody2D rb;
    public GrekColDrag colDrag;
    public SpriteRenderer spriteRenderer;
    public Health health;
    public Attack attack;
    public AudioEffect audioEffect;
    public Animator anim;
    public float JumpForce=300;
    public float coulDown = 0.1f;
    public bool isGrount;
    bool AllowJump = true;
    bool isClimb;
    // Start is called before the first frame update
    void Awake()
    {
        singenton = this;
    }
    private void FixedUpdate()
    {
        if(transform.position.x < -2.17)
        {
            transform.position = new Vector2(-2.17f, transform.position.y);
        }
        if(transform.position.x > 2.4)
        {
            transform.position = new Vector2(2.4f, transform.position.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (attack.isAttacking) return;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            Jump(mousePos);
        }
        //else if (rb.velocity.y > 0)
        //{
        //    rb.velocity = new Vector2(rb.velocity.x, 0);
        //}

        if (colDrag.isCol && rb.velocity.y<=0)
        {
            isClimb = true;
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else
        {
            isClimb = false;
        }

        anim.SetBool("climb", isClimb);
        anim.SetBool("ground", isGrount);
    }

    void CoulDownJump()
    {
        AllowJump = true;
    }
    bool isUI()
    {
        foreach (Touch touch in Input.touches)
        {
            int id = touch.fingerId;
            if (EventSystem.current.IsPointerOverGameObject(id))
            {
                return true;
            }
        }
        return false;
    }
    void Jump(Vector2 pos)
    {
        if (AllowJump && !isUI())
        {
            Vector2 dir = pos - (Vector2)transform.position;
            dir.Normalize();
            dir *= Mathf.Clamp(Vector2.Distance(transform.position, pos),0,3)/3;
            if (!isGrount)
            {
                dir.y = 0;
            }
            if (isClimb) dir.x /=4;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(dir * JumpForce);
            audioEffect.jump.Play();
            AllowJump = false;
            Invoke(nameof(CoulDownJump), coulDown);
            if (dir.x > 0 && !isClimb)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (dir.x < 0 && !isClimb)
                transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        isGrount = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrount = false;
    }

    public void Attack()
    {
        anim.SetTrigger("attack");
    }
}
