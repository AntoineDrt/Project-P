using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Movable))]
public class InputMovement : MonoBehaviour
{
  [SerializeField] Movable movement;
  [SerializeField] bool isMirror = false;

  void Start()
  {
    TurnManager.Instance.MovePhase.AddListener(HandleMoveInput);
  }

  void OnDestroy()
  {
    TurnManager.Instance.MovePhase.RemoveListener(HandleMoveInput);
  }

  public void HandleMoveInput(InputAction.CallbackContext context)
  {
    Vector2 input = context.ReadValue<Vector2>();
    Vector2Int direction = Vector2Int.zero;

    if (input.x > 0) direction = Vector2Int.right;
    else if (input.x < 0) direction = Vector2Int.left;
    else if (input.y > 0) direction = Vector2Int.up;
    else if (input.y < 0) direction = Vector2Int.down;

    if (isMirror)
    {
      direction = -direction;
    }

    var targetPosition = movement.GetTargetPosition(direction);
    movement.TryToMoveTo(Converter.To2D(targetPosition));
  }
}

