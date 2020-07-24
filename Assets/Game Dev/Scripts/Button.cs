using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour
{
    public EventTrigger.TriggerEvent onPressEvent;
    public KeyCode activateKey = KeyCode.E;
    public float activateRange = 2f;
    public string activateLabel = "Press";

    private Text activateText;
    private Animator animator;
    private Transform player;

    private float playerDistSqr;

    void Start()
    {
        this.activateText = GetComponentInChildren<Text>();
        this.animator = GetComponent<Animator>();
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj)
            player = playerObj.transform;
        else {
            Debug.Log("No player object found! Make sure there's an object in your scene tagged \"Player\"");
        }
    }

    void FixedUpdate()
    {
        if (!player)
            return;
        this.playerDistSqr = Vector3.SqrMagnitude(transform.position - player.position);
    }

    void Update()
    {
        if (this.playerDistSqr < this.activateRange * this.activateRange) {
            activateText.enabled = true;
            activateText.text = "[" + this.activateKey + "] " + this.activateLabel;
            if(Input.GetKeyDown(this.activateKey)) {
                animator.Play("Button_Press");
                BaseEventData data = new BaseEventData(EventSystem.current);
                this.onPressEvent.Invoke(data);
            }
        } else {
            activateText.enabled = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, this.activateRange);
    }

}
