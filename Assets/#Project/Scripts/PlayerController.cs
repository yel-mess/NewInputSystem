using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 moveVector;
    float verticalSpeed;
    public float movementSpeed;
    public CharacterController controller;
    public float gravityMultiplier;
    public void Shoot(InputAction.CallbackContext context) { //input action = action créer dans l'action map --> 
        if (context.performed) {
            Debug.Log("PEW !");
        } //si on vient de faire l'action
        else if (context.canceled) {
            Debug.Log("...");
        }
    }
    public void Move(InputAction.CallbackContext context) {
        moveVector = context.ReadValue<Vector2>();
    }
    public void Jump(InputAction.CallbackContext context) {
        if (context.performed && controller.isGrounded) { //au moment ou on appuye et on check si controller isgrounded is true
            verticalSpeed = 20f; //vertical speed devient positive
        }
    }
    void Update() {
        if (controller.isGrounded && verticalSpeed < 0) { //si on n'est pas sur le sol, on donne une valeur négative
            verticalSpeed = 0;
        }
        verticalSpeed += Physics.gravity.y * Time.deltaTime * gravityMultiplier;

        Vector3 movement = new Vector3(moveVector.x, 0, moveVector.y) * movementSpeed;

        if (movement.magnitude >= 0) { //ou != Vector3.zero
            transform.forward = Vector3.Lerp(transform.forward, movement, 0.01f);
        }

        movement.y = verticalSpeed;
        controller.Move(movement * Time.deltaTime);
    }
}
