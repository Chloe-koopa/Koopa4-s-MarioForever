﻿using System;
using SweetMoleHouse.MarioForever.Scripts.Util;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace SweetMoleHouse.MarioForever.Scripts.Player
{
    /// <summary>
    /// 马里奥跳跃
    /// </summary>
    public class MarioJump : MonoBehaviour
    {
        [RenameInInspector("跳跃高度")]
        public float jumpHeight;
        [Header("重力加速度倍率")]
        [SerializeField, RenameInInspector("走")]
        private float gravityScaleNonRunning = 0.6f;
        [SerializeField, RenameInInspector("跑")]
        private float gravityScaleRunning = 0.5f;
        /// <summary>
        /// X速度大于这个值才算跑起来
        /// </summary>
        [Space]
        [SerializeField, RenameInInspector("跑动高跳的最小x速度")]
        private float runSpeedThreshold = 5f / 8f;

        [Header("音效设置")]
        [SerializeField, RenameInInspector("跳跃音效")]
        private AudioClip jumpSound = null;

        private Mario mario;
        private MarioMove mover;
        private bool isReadyToJump;
        public bool IsHoldingJumpKey { get; private set; }
        public float GetGravityScale()
        {
            if (!IsHoldingJumpKey || mover.YSpeed <= 0)
            {
                return 1f;
            }
            return (Math.Abs(mover.XSpeed) > runSpeedThreshold)
                ? gravityScaleRunning : gravityScaleNonRunning;
        }

        private void Start()
        {
            mario = GetComponent<Mario>();
            mover = GetComponent<MarioMove>();
            Global.Inputs.Mario.Jump.performed += OnJumpInput;
            Global.Inputs.Mario.Jump.canceled += OnJumpRelease;
        }
        private void OnDisable()
        {
            Global.Inputs.Mario.Jump.performed -= OnJumpInput;
            Global.Inputs.Mario.Jump.canceled -= OnJumpRelease;
        }
        private void OnJumpInput(CallbackContext ctx)
        {
            IsHoldingJumpKey = true;
            if (mover.YSpeed <= 0)
            {
                isReadyToJump = true;
            }
        }
        private void OnJumpRelease(CallbackContext ctx)
        {
            IsHoldingJumpKey = false;
            isReadyToJump = false;
        }
        private void Update()
        {
            if (isReadyToJump && mover.IsOnGround)
            {
                Jump();
            }
        }

        public void Jump()
        {
            Global.PlaySound(jumpSound);
            Jump(jumpHeight);
        }
        public void Jump(in float height)
        {
            mover.YSpeed = height;
            isReadyToJump = false;
        }
    }
}