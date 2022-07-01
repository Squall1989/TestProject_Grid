using UnityEngine;

public interface ISafeAreaMovable
{
    Vector2 StartPos { get; set; }
    RectTransform MovableRect { get; } 
}