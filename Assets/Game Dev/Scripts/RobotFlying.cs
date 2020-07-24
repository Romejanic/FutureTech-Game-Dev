using UnityEngine;

public class RobotFlying : MonoBehaviour
{
    public float searchRange = 20f;
    public float hoverRange = 5f;
    public float turnSpeed = 10f;
    public float flySpeed = 5f;

    private Transform player;
    private Rigidbody rb;
    private bool spottedPlayer = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // find the player in the scene
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if(!playerObj) {
            Debug.Log("Found no player to kill! Make sure there's an object with the tag \"Player\"");
        } else {
            player = playerObj.transform;
        }
    }

    void FixedUpdate()
    {
        if (!player)
            return;
        float sqrDist = Vector3.SqrMagnitude(transform.position - player.position);
        if (!spottedPlayer) {
            if(sqrDist < searchRange * searchRange) {
                spottedPlayer = true;
            }
        } else {
            float sqrHover = hoverRange * hoverRange;
            Vector3 toPlayer = (player.position - transform.position).normalized;
            Quaternion rot = Quaternion.LookRotation(toPlayer);
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, rot, this.turnSpeed * Time.fixedDeltaTime));
            Vector3 toPlayerScaled = toPlayer * this.flySpeed * Time.fixedDeltaTime;
            if (sqrDist > sqrHover) {
                rb.MovePosition(transform.position + toPlayerScaled);
            } else if(sqrDist < sqrHover) {
                rb.MovePosition(transform.position - toPlayerScaled);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, searchRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, hoverRange);
    }

}
