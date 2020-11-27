using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    private HingeJoint2D hj;
    public float Ropespeed = 10f;
    public float Groundspeed = 10f;
    public float RopeJumpForce;
    public float GroundJumpForce;

    public bool isGrounded;
    public Transform groundChecker;
    public float checkRadius;
    public LayerMask ground;

    private float dirX;

    public bool attached = false;
    public Transform attachedTo;
    private GameObject disregard;

    public GameObject pulleySelected ;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hj = GetComponent<HingeJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckKeyboardInputs();
        CheckPulleyInputs();

        isGrounded = Physics2D.OverlapCircle(groundChecker.position, checkRadius, ground);
    }

    void CheckKeyboardInputs()
    {
        dirX = CrossPlatformInputManager.GetAxis("Horizontal");
        if (dirX < 0)
        {
            if (attached)
            {
                rb.AddRelativeForce(new Vector3(-1, 0, 0) * Ropespeed * Time.deltaTime);
            }
            else
            {
                rb.AddRelativeForce(new Vector3(-1, 0, 0) * Groundspeed * Time.deltaTime);
            }
        }
        if (dirX > 0)
        {
            if (attached)
            {
                rb.AddRelativeForce(new Vector3(1, 0, 0) * Ropespeed * Time.deltaTime);
            }
            else
            {
                rb.AddRelativeForce(new Vector3(1, 0, 0) * Groundspeed * Time.deltaTime);
            }
        }
        if (Input.GetKeyDown("w") && attached)
        {
            Slide(1);
        }
        if (Input.GetKeyDown("s") && attached)
        {
            Slide(-1);
        }
        if (CrossPlatformInputManager.GetButtonDown("Jump") && attached)
        {
            rb.AddRelativeForce(new Vector3(0, RopeJumpForce, 0) * (Ropespeed) * Time.deltaTime);
            Detach();
        }
        if(CrossPlatformInputManager.GetButtonDown("Jump") && !attached && isGrounded)
        {
            rb.AddRelativeForce(new Vector3(0, GroundJumpForce, 0) * Groundspeed * Time.deltaTime);
        }
    }

    public void Attach(Rigidbody2D ropeBone)
    {
        ropeBone.gameObject.GetComponent<RopeSegment>().isPlayerAttached = true;
        hj.connectedBody = ropeBone;
        hj.enabled = true;
        attached = true;
        attachedTo = ropeBone.gameObject.transform.parent;
    }

    void Detach()
    {
        hj.connectedBody.gameObject.GetComponent<RopeSegment>().isPlayerAttached = false;
        attached = false;
        hj.enabled = false;
        hj.connectedBody = null;
    }

    public void Slide(int direction)
    {
        RopeSegment myConnection = hj.connectedBody.gameObject.GetComponent<RopeSegment>();
        GameObject newSeg = null;
        if (direction > 0)
        {
            if (myConnection.connectedAbove != null)
            {
                if (myConnection.connectedAbove.gameObject.GetComponent<RopeSegment>() != null)
                {
                    newSeg = myConnection.connectedAbove;
                }
            }
        }
        else
        {
            if (myConnection.connectedBelow != null)
            {
                newSeg = myConnection.connectedBelow;
            }
        }
        if (newSeg != null)
        {
            transform.position = newSeg.transform.position;
            myConnection.isPlayerAttached = false;
            newSeg.GetComponent<RopeSegment>().isPlayerAttached = true;
            hj.connectedBody = newSeg.GetComponent<Rigidbody2D>();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!attached)
        {
            if (collision.gameObject.tag == "Rope")
            {
                if (attachedTo != collision.gameObject.transform.parent)
                {
                    if (disregard == null || collision.gameObject.transform.parent.gameObject != disregard)
                    {
                        Attach(collision.gameObject.GetComponent<Rigidbody2D>());
                    }
                }
            }
        }
/*        if(collision.gameObject.tag == "Rope2")
        {
            Debug.Log(collision.gameObject);
            this.transform.parent = collision.transform;
            this.transform.position = transform.parent.position;
        }*/
    }

    void CheckPulleyInputs()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null && hit.transform.gameObject.CompareTag("Crank"))
            {
                if (pulleySelected != hit.transform.gameObject)
                {
                    if (pulleySelected != null)
                    {
                        pulleySelected.GetComponent<Crank>().Deselect();
                    }

                    pulleySelected = hit.transform.gameObject;
                    pulleySelected.GetComponent<Crank>().Select();
                }
                else if (pulleySelected == hit.transform.gameObject)
                {
                    pulleySelected.GetComponent<Crank>().Deselect();
                    pulleySelected = null;
                }
            }
            else
            {
                if (pulleySelected != null)
                {
                    pulleySelected.GetComponent<Crank>().Deselect();
                    pulleySelected = null;
                }
            }
        }
        if(Input.GetKeyDown("f")&&pulleySelected != null)
        {
            pulleySelected.GetComponent<Crank>().Rotate(1);
        }
        if (Input.GetKeyDown("r") && pulleySelected != null)
        {
            pulleySelected.GetComponent<Crank>().Rotate(-1);
        }
    }
}
