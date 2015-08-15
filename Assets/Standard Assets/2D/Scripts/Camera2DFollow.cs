using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class Camera2DFollow : MonoBehaviour
    {
       // public Transform target;
		public PlatformerCharacter2D target;

        public float damping = 1;
        public float lookAheadFactor = 3;
        public float lookAheadReturnSpeed = 0.5f;
        public float lookAheadMoveThreshold = 0.1f;

		public float minY = 5.1f;
		public float maxY= 7.1f;

        private float m_OffsetZ;
        private Vector3 m_LastTargetPosition;
        private Vector3 m_CurrentVelocity;
        private Vector3 m_LookAheadPos;

        // Use this for initialization
        private void Start()
        {
            //m_LastTargetPosition = target.position;
            m_OffsetZ = (transform.position - target.transform.position).z;
            transform.parent = null;
        }

        // Update is called once per frame
        private void Update()
        {
            // only update lookahead pos if accelerating or changed direction
			float xMoveDelta = Mathf.Abs((target.transform.position - m_LastTargetPosition).x);

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;


            
			if (updateLookAheadTarget)
            {
                m_LookAheadPos = lookAheadFactor*Vector3.right*Mathf.Sign(xMoveDelta);
            }
            else
            {
                m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime*lookAheadReturnSpeed);
            }

            Vector3 aheadTargetPos = target.transform.position + m_LookAheadPos + Vector3.forward*m_OffsetZ;

//			if(!target.IsGrounded)
//				aheadTargetPos.y = transform.position.y;

			if(aheadTargetPos.y<minY)
				aheadTargetPos.y=minY;
			else if(aheadTargetPos.y>maxY)
				aheadTargetPos.y=maxY;

            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

			//newPos.y=transform.position.y;

            transform.position = newPos;

            m_LastTargetPosition = target.transform.position;
        }
    }
}
