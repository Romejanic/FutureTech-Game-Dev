using UnityEngine;

[RequireComponent(typeof(Camera))]
public class NoClip : MonoBehaviour
{
    public float moveSpeed = 5;
    public float shiftMultiplier = 2;
    public float smoothSpeed = 3;
    public float lookSpeed = 180;
    public bool invertY = false;
    public float lookLimit = 90f;
    public bool lockCursor = true;

    private float move = 0;
    private float strafe = 0;
    private float upDown = 0;
    private bool shift = false;
    private float tempMove = 0;
    private float tempStrafe = 0;
    private float tempUpDown = 0;
    private float moveVel, strafeVel, upDownVel;
    private float rotX, rotY;

    void Start() {
        Vector3 rot = transform.rotation.eulerAngles;
        rotX = rot.x;
        rotY = rot.y;
        if(lockCursor) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // actually move the camera each physics update
    void FixedUpdate()
    {
        float smooth = Time.fixedDeltaTime * this.smoothSpeed;
        this.tempMove = Mathf.SmoothDamp(this.tempMove, this.move, ref this.moveVel, smooth);
        this.tempStrafe = Mathf.SmoothDamp(this.tempStrafe, this.strafe, ref this.strafeVel, smooth);
        this.tempUpDown = Mathf.SmoothDamp(this.tempUpDown, this.upDown, ref this.upDownVel, smooth);
        // calculate move speed and add it to the camera's position
        float speed = this.moveSpeed * (this.shift ? this.shiftMultiplier : 1f) * Time.fixedDeltaTime;
        transform.position += ((transform.forward * this.tempMove) + (transform.right * this.tempStrafe)) * speed;
        // up and down movement
        transform.position += transform.up * speed * this.tempUpDown;
    }

    // get input from keyboard
    void Update()
    {
        // update movement variables
        this.move = Input.GetAxisRaw("Vertical");
        this.strafe = Input.GetAxisRaw("Horizontal");
        this.shift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        this.upDown = BoolToInt(Input.GetKey(KeyCode.Q)) - BoolToInt(Input.GetKey(KeyCode.E));
        // rotate the camera based on mouse movement
        float speed = this.lookSpeed * Time.deltaTime;
        rotX = rotX + (Input.GetAxis("Mouse Y") * speed * (this.invertY ? 1f : -1f));
        rotY = (rotY + (Input.GetAxis("Mouse X") * speed)) % 360f;
        rotX = Mathf.Clamp(rotX, -this.lookLimit+0.1f, this.lookLimit-0.1f); // x axis needs to be limited so you can't look backwards
        transform.rotation = Quaternion.Euler(rotX, rotY, 0);
    }

    public Vector2 GetRotation()
    {
        return new Vector2(rotX, rotY);
    }

    private int BoolToInt(bool value)
    {
        return value ? 1 : 0;
    }

}
