using UnityEngine;
using UnityEngine.Events;

public interface IMoveable
{
    void Move();
    void IncreaseMovementSpeed(int speedMultiplier);
}
