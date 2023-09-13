using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimatons : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] upAnimations;
    [SerializeField] private Sprite[] downAnimations;
    [SerializeField] private Sprite[] sideAnimations;
    [SerializeField] private Sprite[] diagonalUpAnimations;
    [SerializeField] private Sprite[] diagonalDownAnimations;

    public Vector3 direction = default;
    [SerializeField] private float animationDelay = 0.2f;
    private float timer = 0.0f;
    private bool isMoving = false;

    private void Update()
    {
        direction = new Vector3(Input.GetAxisRaw("Movement X"), 0, Input.GetAxisRaw("Movement Y"));

        if (direction != Vector3.zero)
        {
            if (!isMoving)
            {
                isMoving = true;
                timer = 0.0f;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
        else
        {
            isMoving = false;
            timer = 0.0f;
        }

        if (isMoving && timer > animationDelay)
        {
            if (direction.z > 0)
            { // Moving North
                if (direction.x > 0)
                { // Moving North East
                    spriteRenderer.flipX = true;
                    spriteRenderer.sprite = diagonalUpAnimations[0];
                }
                else if (direction.x < 0)
                { // Moving North West
                    spriteRenderer.flipX = false;
                    spriteRenderer.sprite = diagonalUpAnimations[0];
                }
                else
                { // North
                    spriteRenderer.sprite = upAnimations[0];
                }
            }
            else if (direction.z < 0)
            { // Moving South
                if (direction.x > 0)
                { // Moving South East
                    spriteRenderer.flipX = true;
                    spriteRenderer.sprite = diagonalDownAnimations[0];
                }
                else if (direction.x < 0)
                { // Moving South West
                    spriteRenderer.flipX = false;
                    spriteRenderer.sprite = diagonalDownAnimations[0];
                }
                else
                { // South
                    spriteRenderer.sprite = downAnimations[0];
                }
            }
            else if (direction.x != 0)
            { // Moving Left and Right
                spriteRenderer.flipX = (direction.x > 0);
                spriteRenderer.sprite = sideAnimations[0];
            }

            timer = 0.0f;
        }
    }
}