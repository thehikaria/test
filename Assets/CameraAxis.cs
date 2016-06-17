using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CameraAxis : MonoBehaviour
{
	public float RotateSpeed = 0.1f;
	public float UpDownSpeed = 0.01f;

	float yRotation = 0;
	float xRotation = 0;

	void Update()
	{	
		


		int touchCount = Input.touches
			.Count(t => t.phase != TouchPhase.Ended && t.phase != TouchPhase.Canceled);
		if (touchCount == 1)
		{
			Touch t = Input.touches.First();
			switch (t.phase)
			{
			case TouchPhase.Moved:


				yRotation += t.deltaPosition.y * RotateSpeed;
				yRotation = Mathf.Clamp(yRotation, -80, 80);
				xRotation -= t.deltaPosition.x * RotateSpeed;
				xRotation = xRotation % 360;
				this.transform.localEulerAngles = new Vector3(yRotation, xRotation, 0);

//				//移動量
//				float xDelta = t.deltaPosition.x * RotateSpeed;
//				float yDelta = t.deltaPosition.y * RotateSpeed;
//
//				//左右回転
//				this.transform.Rotate(yDelta, xDelta, 0, Space.World);

				break;
			}
		}
	}
}