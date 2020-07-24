using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform following;
    public Transform[] followingMulti;
    public bool multi = false;

    public float followDistance = 3f;
    public float lookSpeed = 180;
    public bool invertY = false;
    public float lookLimit = 90f;
    public bool lockCursor = true;
    public float smoothSpeed = 3;

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
    }

    void Update()
    {
        if (!following && !multi)
            return;
        float speed = this.lookSpeed * Time.deltaTime;
        rotX = rotX + (Input.GetAxis("Mouse Y") * speed * (this.invertY ? 1f : -1f));
        rotY = (rotY + (Input.GetAxis("Mouse X") * speed)) % 360f;
        rotX = Mathf.Clamp(rotX, -this.lookLimit + 0.1f, this.lookLimit - 0.1f); // x axis needs to be limited so you can't look backwards
        Quaternion target = Quaternion.Euler(rotX, rotY, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * this.smoothSpeed);
    }
    
    void FixedUpdate()
    {
        if (!following && !multi)
            return;
        Vector3 target = this.GetFollowingPoint() - transform.forward * this.followDistance;
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, this.smoothSpeed * Time.fixedDeltaTime);
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
