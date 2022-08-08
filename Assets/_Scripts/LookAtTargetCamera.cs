using UnityEngine;
using System.Collections;

public class LookAtTargetCamera : MonoBehaviour
{
	private Transform _player;
	private float _smooth;

	private float _offsetZ = -10f;

    public void Initialize(Player player)
    {
		_player = player.transform;
		_smooth = 5f;
    }

	private void LateUpdate()
	{
		if (_player != null)
        {
			Vector3 currentPosition = Vector3.Lerp(transform.position, _player.position, _smooth * Time.fixedTime);
			transform.position = new Vector3(currentPosition.x, currentPosition.y, _offsetZ);
		}
			
	}
}