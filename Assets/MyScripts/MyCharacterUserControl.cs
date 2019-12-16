﻿using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (MyCharacterController))]
    public class MyCharacterUserControl : MonoBehaviour
    {
        private bool canControl = true;
        private MyCharacterController m_Character;
        private bool m_Jump;


        private void Awake()
        {
            m_Character = GetComponent<MyCharacterController>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }


        private void FixedUpdate()
        {
            if ( canControl ){
                float h = CrossPlatformInputManager.GetAxis("Horizontal");
                // Pass all parameters to the character control script.
                m_Character.Move(h, m_Jump);
                m_Jump = false;
            }
            else {
                m_Character.Move(0, false);
                m_Jump = false;
            }
            
        }

        public bool CanControl(){
            return canControl;
        }

        public void SetCanControl(bool b){
            canControl = b;
        }
    }
}
