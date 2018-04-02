﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace SVHandles.Displays.Handle
{
    public class BoundsHandleDisplay : ITypeHandleDisplay
    {
        public Type ExecutingType
        {
            get { return typeof(Bounds); }
        }

        public void Draw(SVArgs args, out object obj)
        {
            Transform t = args.MonoInstance.transform;

            Bounds inBounds = (args.Value as Bounds?).GetValueOrDefault();
            Bounds outBounds = inBounds;

            outBounds.center = t.InverseTransformPoint(Handles.DoPositionHandle(t.TransformPoint(inBounds.center), Quaternion.identity));
            //outBounds.size = Handles.DoScaleHandle(inBounds.size, t.TransformPoint(outBounds.center), Quaternion.identity, 1f);

            Vector3 center = t.TransformPoint(outBounds.center);
            Vector3 extents = outBounds.extents;

            // Draw our handles for dragging scale.
            Vector3 outSize = outBounds.size;


            BoxCollider b;

            float xSideScale = (outSize.z * outSize.y) / 2f;
            outSize.x = Handles.ScaleSlider(outSize.x, center + (Vector3.right * outSize.x / 2f), Vector3.right, Quaternion.LookRotation(Vector3.right), HandleUtility.GetHandleSize(center + (Vector3.right * outSize.x / 2f)), 1f);
            outSize.x = Handles.ScaleSlider(outSize.x, center + (Vector3.left * outSize.x / 2f), Vector3.left, Quaternion.LookRotation(Vector3.left), HandleUtility.GetHandleSize(center + (Vector3.left * outSize.x / 2f)), 1f);

            float ySideScale = (outSize.x * outSize.z) / 2f;
            outSize.y = Handles.ScaleSlider(outSize.y, center + (Vector3.up * outSize.y / 2f), Vector3.up, Quaternion.LookRotation(Vector3.up), HandleUtility.GetHandleSize(center + (Vector3.up * outSize.y / 2f)), 1f);
            outSize.y = Handles.ScaleSlider(outSize.y, center + (Vector3.down * outSize.y / 2f), Vector3.down, Quaternion.LookRotation(Vector3.down), HandleUtility.GetHandleSize(center + (Vector3.down * outSize.y / 2f)), 1f);

            float zSideScale = (outSize.x * outSize.y) / 2f;
            outSize.z = Handles.ScaleSlider(outSize.z, center + (Vector3.forward * outSize.z / 2f), Vector3.forward, Quaternion.LookRotation(Vector3.forward), HandleUtility.GetHandleSize(center + (Vector3.forward * outSize.z / 2f)), 1f);
            outSize.z = Handles.ScaleSlider(outSize.z, center + (Vector3.back * outSize.z / 2f), Vector3.back, Quaternion.LookRotation(Vector3.back), HandleUtility.GetHandleSize(center + (Vector3.back * outSize.z / 2f)), 1f);

            outBounds.size = outSize;

            Handles.DrawWireCube(t.TransformPoint(outBounds.center), outBounds.size);

            obj = (outBounds != inBounds) ? (object)outBounds : null;
        }
    }
}