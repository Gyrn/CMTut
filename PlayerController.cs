using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    float interactDistance = 2f;
    private bool isWalking;
    private Vector3 lastInteractDir;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;

    private void Update() { 
        Handlemovement();
        HandleInteractions();   
}

    public bool IsWalking() {
        return isWalking;
    }

    private void HandleInteractions() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero) {
            lastInteractDir = moveDir;
        }

        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance)) 
        {
          
        }
    }

   
   
   
   
    private void Handlemovement() {
        
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed *Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

    if (!canMove) {
        // Cant move towards direction, so the code will attempt to move only on the X axis.
        Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
        canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

        if (canMove) {
        // If it can move on the X axis it will do so.
            moveDir = moveDirX;
        } else {
        // If it cant, however, it will attempt to move on the Z axis.
             Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
        canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

            if (canMove) {
        // Again, if it can it will.
                moveDir = moveDirZ;
            } else {
        // But if it cant, nothing will happen.
            }
        }
    }




       
if (canMove) {
    transform.position += moveDir * moveDistance;
}

 isWalking = moveDir != Vector3.zero;
        
 transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);

    
    }

}
