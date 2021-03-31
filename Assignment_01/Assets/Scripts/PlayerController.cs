using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    public Vector3 jump;
    public float jumpingForce = 9.0f;

    public bool isGrounded = true;
    private int currJump = 0;
    private int maxJump = 2;


    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    // Start is called before the first frame update
    void Start()
    {
          count = 0;
          rb = GetComponent<Rigidbody>();

          SetCountText();
          winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
      countText.text = "Count: " + count.ToString();
      if(count >= 6)
      {
        winTextObject.SetActive(true);
      }
    }

    void Update()
    {

      if(Keyboard.current.spaceKey.wasPressedThisFrame)
      {
        if(isGrounded || maxJump > currJump)
        {
            rb.AddForce(Vector3.up * jumpingForce, ForceMode.Impulse);
            isGrounded = false;
        }
      }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

   private void OnTriggerEnter(Collider other)
    {
      if(other.gameObject.CompareTag("PickUp"))
      {
        other.gameObject.SetActive(false);
        count+=1;
        SetCountText();
      }
    }

    void OnCollisionStay() {
      isGrounded = true;
      currJump = 0;
    }
}
