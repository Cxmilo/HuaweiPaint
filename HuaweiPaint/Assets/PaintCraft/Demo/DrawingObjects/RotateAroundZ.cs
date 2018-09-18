using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace PaintCraft.Demo
{
	public class RotateAroundZ : MonoBehaviour
	{
		public float speed = 1.0f;
		public  Vector3 Axis = Vector3.forward;

		// Update is called once per frame
		void Update()
		{
			
			transform.RotateAround(transform.position, transform.TransformDirection(Axis), speed * Time.deltaTime);
		}
	}
}