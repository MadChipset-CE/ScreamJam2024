using System.Collections.Generic;
using UnityEngine;

public static class MadUtils
{
    public static bool isBetweenTwoPointsFloat(float value, float pointA, float pointB) { return value > pointA && value < pointB; }

    public static List<Transform> getVisibleTransforms(Camera pointOfView, List<Transform> candidatedForVisibility, float RangeFromCenterUpToOne) {
        List<Transform> visibleTransforms = new List<Transform>();

        foreach(Transform transform in candidatedForVisibility) {
            Renderer transformRenderer = transform.GetComponent<Renderer>();
            if(!transformRenderer.isVisible) continue;

            Vector3 transformViewportPosition = pointOfView.WorldToViewportPoint(transform.position);
            bool isVisible =    isBetweenTwoPointsFloat(transformViewportPosition.x, 1 - RangeFromCenterUpToOne, RangeFromCenterUpToOne) &&
                                isBetweenTwoPointsFloat(transformViewportPosition.y, 1 - RangeFromCenterUpToOne, RangeFromCenterUpToOne) &&
                                transformViewportPosition.z > 0;

            if(!isVisible) continue;

            visibleTransforms.Add(transform);
        }

        return visibleTransforms;
    }

}
