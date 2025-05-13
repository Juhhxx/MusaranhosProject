using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2             _mov;

    public void NoGridMov(Vector2 dir, float velocity, CharacterController _playerController)
    {
        _mov.x = dir.x*velocity;
        _mov.y = 0;

        _playerController.Move(_mov);
    }
}
