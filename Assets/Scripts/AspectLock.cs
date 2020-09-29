﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SweetMoleHouse
{
    /// <summary>
    /// 锁定纵横比
    /// </summary>
    public class AspectLock : MonoBehaviour 
    {
        [SerializeField]
        private float targetAspect = 4f / 3;
        private void Update()
        {
            if (Camera.main.aspect != targetAspect)
            {
                Camera.main.aspect = targetAspect;
            }
        }
    }
}