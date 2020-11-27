using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public Rigidbody2D hook;
    public GameObject[] prefabsRopeSegments;
    public int numlinks = 5;

    public HingeJoint2D top;

    public PlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        GenerateRope();
    }

    void GenerateRope()
    {
        Rigidbody2D prevBod = hook;
        for(int i = 0; i<numlinks; i++)
        {
            int index = Random.Range(0, prefabsRopeSegments.Length);
            GameObject newSeg = Instantiate(prefabsRopeSegments[index]);
            newSeg.transform.parent = transform;
            newSeg.transform.position = transform.position;
            HingeJoint2D hj = newSeg.GetComponent<HingeJoint2D>();
            hj.connectedBody = prevBod;

            prevBod = newSeg.GetComponent<Rigidbody2D>();

            if (i == 0)
            {
                top = hj;
            }
        }
    }

    public void addLink()
    {
        int index = Random.Range(0, prefabsRopeSegments.Length);
        GameObject newLink = Instantiate(prefabsRopeSegments[index]);
        newLink.transform.parent = transform;
        newLink.transform.position = transform.position;
        HingeJoint2D hj = newLink.GetComponent<HingeJoint2D>();
        hj.connectedBody = hook;
        newLink.GetComponent<RopeSegment>().connectedBelow = top.gameObject;
        top.connectedBody = newLink.GetComponent<Rigidbody2D>();
        top.GetComponent<RopeSegment>().ResetAnchor();
        top = hj;
    }

    public void removeLink()
    {
/*        if (top.gameObject.GetComponent<RopeSegment>().isPlayerAttached)
        {
            player.Slide(-1);
        }*/
        HingeJoint2D newTop = top.gameObject.GetComponent<RopeSegment>().connectedBelow.GetComponent<HingeJoint2D>();
        newTop.connectedBody = hook;
        newTop.gameObject.transform.position = hook.gameObject.transform.position;
        newTop.GetComponent<RopeSegment>().ResetAnchor();
        Destroy(top.gameObject);
        top = newTop;
    }
}
