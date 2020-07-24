using UnityEngine;

[RequireComponent(typeof(Camera))]
[AddComponentMenu("Game Dev Workshop/Follow Camera")]
public class FollowCamera : MonoBehaviour
{
    [SerializeField] public Transform following;
    [SerializeField] public Transform[] followingMulti;
    public bool multi = false;

    public float lookSpeed = 180;
    public bool invertY = false;
    public float lookLimit = 90f;
    public bool lockCursor = true;
    public float smoothSpeed = 3;

    public float followDistance = 10f;
    public float minDistance = 5f;
    public float maxDistance = 10f;
    public float zoomSpeed = 3f;

    private float rotX, rotY;
    private Vector3 velocity;
    private Vector3 avgTemp;

    void Start()
    {
        Vector3 rot = transform.rotation.eulerAngles;
        rotX = rot.x;
        rotY = rot.y;
        if (lockCursor) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        followDistance = minDistance;
    }

    void Update()
    {
        if (!following && !multi)
            return;
        // look
        float speed = this.lookSpeed * Time.deltaTime;
        rotX = rotX + (Input.GetAxis("Mouse Y") * speed * (this.invertY ? 1f : -1f));
        rotY = (rotY + (Input.GetAxis("Mouse X") * speed)) % 360f;
        rotX = Mathf.Clamp(rotX, -this.lookLimit + 0.1f, this.lookLimit - 0.1f); // x axis needs to be limited so you can't look backwards
        // zoom
        followDistance = Mathf.Clamp(followDistance + Input.GetAxis("Mouse ScrollWheel") * this.zoomSpeed, this.minDistance, this.maxDistance);
    }
    
    void FixedUpdate()
    {
        if (!following && !multi)
            return;
        // rotation
        Quaternion target = Quaternion.Euler(rotX, rotY, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.fixedDeltaTime * this.smoothSpeed);
        // movement
        Vector3 targetPos = this.GetFollowingPoint() - transform.forward * this.followDistance;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, this.smoothSpeed * Time.fixedDeltaTime);
    }

    Vector3 GetFollowingPoint()
    {
        if (!multi)
            return following.position;
        avgTemp.Set(0, 0, 0);
        foreach(Transform t in followingMulti) {
            avgTemp += t.position;
        }
        avgTemp /= followingMulti.Length;
        return avgTemp;
    }

}
