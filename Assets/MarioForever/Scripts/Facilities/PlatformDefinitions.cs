﻿using SweetMoleHouse.MarioForever.Scripts.Base;
using SweetMoleHouse.MarioForever.Scripts.Constants;
using UnityEngine;

namespace SweetMoleHouse.MarioForever.Scripts.Facilities
{
    public static class PlatformDefinitions
    {
        public static bool IsStandingOnPlatform(this BasePhysics thiz, in Vector3 platform)
        {
            return thiz.transform.position.y >= platform.y - Consts.OnePixel;
        }
    }
}
