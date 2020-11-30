using UnityEngine;

public class Follow_player : MonoBehaviour
{
    public Transform player;
    // Update is called once per frame
    void Update()
    {
        //transform.position = player.position + Offset;
        Vector3 pos = transform.position;
        pos.x = player.position.x;
        transform.position = pos;
    }
}
