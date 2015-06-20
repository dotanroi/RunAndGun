using UnityEngine;
using System.Collections;

public static class  CameraExtensions {

    public static bool IsVisible(this Bounds bounds,Camera camera){
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);

        if (GeometryUtility.TestPlanesAABB(planes , bounds))
            return true;
        else
            return false;
    }

    public static bool IsVisible(this Bounds bounds){
        for (int i = 0; i < Camera.allCameras.Length; i++) {
            if(bounds.IsVisible(Camera.allCameras[i]))
                return true;
        }
        return false;
    }

}
