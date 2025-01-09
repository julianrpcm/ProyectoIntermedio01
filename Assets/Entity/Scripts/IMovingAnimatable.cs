using UnityEngine;

public interface IMovingAnimatable
{
    bool GetIsGorunded();
    float GetJumpProgress();
    float GetNormalizedLocalForwardlVelocity();
    float GetNormalizedLocalHorizontalVelocity();


 
}
