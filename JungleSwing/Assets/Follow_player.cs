using UnityEngine;

public class Follow_player : MonoBehaviour
{
    public Transform player;
    public Vector3 Offset;
    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + Offset;
        Vector3 pos = transform.position;
        pos.y = Mathf.Clamp(transform.position.y, -0.87f, -0.87f);
    }
}
