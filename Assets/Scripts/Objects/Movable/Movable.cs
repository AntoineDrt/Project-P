using UnityEngine;

public class Movable : Object
{
  public Vector2Int oldPosition;
  public Vector2Int currentPosition;
  public float moveSpeed = 10f;
  public bool isMoving = false;

  public bool CanMoveTo(Vector3 targetPosition)
  {
    var targetPosition2D = Converter.To2D(targetPosition);

    if (mapManager.FloorMap.ContainsKey(targetPosition2D))
    {
      if (mapManager.ObjectsMap.ContainsKey(targetPosition2D))
      {
        return false;
      }
      return true;
    }

    return false;
  }

  public void MoveTo(Vector3 targetPosition)
  {
    if (isMoving)
    {
      transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

      if (transform.position == targetPosition)
      {
        currentPosition = Converter.To2D(transform.position);
        mapManager.UpdateMapPosition(oldPosition, currentPosition, this);
        oldPosition = currentPosition;
        isMoving = false;
      }
    }
  }

  public Vector3 GetTargetPosition(Vector2Int direction)
  {
    return transform.position + new Vector3(direction.x, 0f, direction.y);
  }
}