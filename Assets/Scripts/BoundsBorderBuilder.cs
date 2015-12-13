using UnityEngine;
using System.Collections;

public class BoundsBorderBuilder : MonoBehaviour {

    GameObject northWall;
    GameObject southWall;
    GameObject westWall;
    GameObject eastWall;

    // Use this for initialization
    void Start () {
        CameraBounds bounds = Camera.main.GetComponent<CameraBounds>();
        if(bounds == null)
        {
            Debug.Log("Main Camera must have CameraBounds!");
            return;
        }

        Vector3 minCorner = new Vector3(bounds.minX, bounds.minY, 0);
        Vector3 maxCorner = new Vector3(bounds.maxX, bounds.maxY, 0);
        float width = bounds.maxX - bounds.minX;
        float height = bounds.maxY - bounds.minY;

        northWall = new GameObject("northWall");
        BoxCollider2D bCol = northWall.AddComponent<BoxCollider2D>();
        bCol.sharedMaterial = Resources.Load("Materials/BouncyMat") as PhysicsMaterial2D;
        bCol.size = new Vector2(width, 1);
        northWall.transform.position = minCorner + new Vector3(width / 2, -bCol.bounds.extents.y, 0);

        southWall = new GameObject("southWall");
        bCol = southWall.AddComponent<BoxCollider2D>();
        bCol.sharedMaterial = Resources.Load("Materials/BouncyMat") as PhysicsMaterial2D;
        bCol.size = new Vector2(width, 1);
        southWall.transform.position = maxCorner + new Vector3(-width / 2, bCol.bounds.extents.y, 0);

        westWall = new GameObject("westWall");
        bCol = westWall.AddComponent<BoxCollider2D>();
        bCol.sharedMaterial = Resources.Load("Materials/BouncyMat") as PhysicsMaterial2D;
        bCol.size = new Vector2(1, height);
        westWall.transform.position = minCorner + new Vector3(-bCol.bounds.extents.x, height / 2, 0);

        eastWall = new GameObject("eastWall");
        bCol = eastWall.AddComponent<BoxCollider2D>();
        bCol.sharedMaterial = Resources.Load("Materials/BouncyMat") as PhysicsMaterial2D;
        bCol.size = new Vector2(1, height);
        eastWall.transform.position = maxCorner + new Vector3(bCol.bounds.extents.x, -height / 2, 0);
    }
}
