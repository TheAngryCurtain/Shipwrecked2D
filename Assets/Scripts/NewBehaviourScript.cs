using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour
{
    //For this, your GameObject this script is attached to would have a
    //Transform Component, a Mesh Filter Component, and a Mesh Renderer
    //component. You will also need to assign your texture atlas / material
    //to it. 

    [SerializeField] private MeshFilter filter;

    public int mapSizeX;
    public int mapSizeY;

    private Mesh mesh;
    private Vector2[] uvArray;

    void Start()
    {
        BuildMesh();
    }

    public void BuildMesh()
    {
        int numTiles = mapSizeX * mapSizeY;
        int numTriangles = numTiles * 6;
        int numVerts = numTiles * 4;

        Vector3[] vertices = new Vector3[numVerts];
        uvArray = new Vector2[numVerts];

        int x, y, iVertCount = 0;
        for (x = 0; x < mapSizeX; x++)
        {
            for (y = 0; y < mapSizeY; y++)
            {
                vertices[iVertCount + 0] = new Vector3(x, y, 0);
                vertices[iVertCount + 1] = new Vector3(x + 1, y, 0);
                vertices[iVertCount + 2] = new Vector3(x + 1, y + 1, 0);
                vertices[iVertCount + 3] = new Vector3(x, y + 1, 0);
                iVertCount += 4;
            }
        }

        int[] triangles = new int[numTriangles];

        int iIndexCount = 0; iVertCount = 0;
        for (int i = 0; i < numTiles; i++)
        {
            triangles[iIndexCount + 0] += (iVertCount + 0);
            triangles[iIndexCount + 1] += (iVertCount + 1);
            triangles[iIndexCount + 2] += (iVertCount + 2);
            triangles[iIndexCount + 3] += (iVertCount + 0);
            triangles[iIndexCount + 4] += (iVertCount + 2);
            triangles[iIndexCount + 5] += (iVertCount + 3);

            iVertCount += 4; iIndexCount += 6;
        }

        mesh = new Mesh();
        //mesh.MarkDynamic(); //if you intend to change the vertices a lot, this will help.
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        filter.mesh = mesh;

        //UpdateMesh(); //I put this in a separate method for my own purposes.
    }


    //Note, the example UV entries I have are assuming a tile atlas 
    //with 16 total tiles in a 4x4 grid.

    public void UpdateMesh()
    {
        int iVertCount = 0;

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                uvArray[iVertCount + 0] = new Vector2(0, 0); //Top left of tile in atlas
                uvArray[iVertCount + 1] = new Vector2(.25f, 0); //Top right of tile in atlas
                uvArray[iVertCount + 2] = new Vector2(.25f, .25f); //Bottom right of tile in atlas
                uvArray[iVertCount + 3] = new Vector2(0, .25f); //Bottom left of tile in atlas
                iVertCount += 4;
            }
        }

        filter.mesh.uv = uvArray;
    }
}
