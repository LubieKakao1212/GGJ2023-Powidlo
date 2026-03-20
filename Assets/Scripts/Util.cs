using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static Vector3 XYToXZ(this Vector2 v)
    {
        return new Vector3(v.x, 0, v.y);
    }

    public static Vector2 XZToXY(this Vector3 v)
    {
        return new Vector2(v.x, v.z);
    }
    
    public static void FillViewMesh(this Mesh mesh, Vector3 origin, float maxDistance, float paddingRadius, LayerMask layerMask, int resolution = 1024, float phase = 0) {
        Vector3[] verts = new Vector3[resolution + 1];
        
        verts[0] = origin;

        var ray = new Ray();
        ray.origin = origin;
        for (int i = 0; i < resolution; i++) {
            var ratio = (i + phase) / resolution;
            var theta = ratio * Mathf.PI * 2f;
            var dir = new Vector3(Mathf.Cos(theta), 0, Mathf.Sin(theta));
            ray.direction = dir;
            verts[i + 1] = CastDir(ray, maxDistance, paddingRadius, layerMask);
        }
        
        mesh.SetVertices(verts);
        mesh.RecalculateBounds();
    }

    public static void FillViewMeshIndices(this Mesh mesh, int resolution = 1024) {
        var inds = new int[resolution * 3];


        for (int i = 0; i < resolution; i++) {
            var ii = i * 3;
            inds[ii    ] = i + 1;
            inds[ii + 1] = 0;
            inds[ii + 2] = ((i + 1) % resolution) + 1;
        }

        var verts = new Vector3[resolution + 1];
        mesh.SetVertices(verts); // Set to provide vertex count
        
        mesh.SetIndices(inds, MeshTopology.Triangles, 0);
    }
    
    public static Vector3 CastDir(Ray ray, float maxDist, float padding, LayerMask layerMask) {
        if (Physics.SphereCast(ray, padding, out var moveHit, maxDist, layerMask)) {
            return ray.GetPoint(moveHit.distance);
        }
        return ray.GetPoint(maxDist);
    }
}
