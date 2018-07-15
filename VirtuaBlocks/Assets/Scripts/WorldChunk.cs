using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WorldChunk : MonoBehaviour {

    // List of all vertices in the chunk
    private List<Vector3> newVertices = new List<Vector3>();
    // List of triangles in the chunk
    private List<int> newTriangles = new List<int>();
    private List<Vector2> newUV = new List<Vector2>();

    private Mesh mesh;
    private MeshCollider chunkCollider;
    private int faceCount;
    

    // Fractional width and height of the texture square in the atlas
    private float textureWidth = 0.083f;
    // The coordinates of the textures in the texture atlas
    private Vector2 grassTop = new Vector2(1, 11);
    private Vector2 grassSide = new Vector2(0, 10);
    private Vector2 rock = new Vector2(7, 8);


    public enum TextureType {

        air, grass, rock

    }


	// Use this for initialization
	void Start () {
        mesh = GetComponent<MeshFilter>().mesh;
        chunkCollider = GetComponent<MeshCollider>();

        CubeTop(0, 0, 0, (byte) TextureType.grass.GetHashCode());
        UpdateMesh();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void CubeTop(int x, int y, int z, byte block) {

        // Add the vertices of the cubes top surface
        newVertices.Add(new Vector3(x, y, z + 1));
        newVertices.Add(new Vector3(x + 1, y, z + 1));
        newVertices.Add(new Vector3(x + 1, y, z));
        newVertices.Add(new Vector3(x, y, z));

        // Add the the six vectors of the two triangles that the surface is comprised of
        newTriangles.Add(faceCount * 4); // Triangle 1
        newTriangles.Add(faceCount * 4 + 1); // Triangle 1
        newTriangles.Add(faceCount * 4 + 2); // Triangle 1

        newTriangles.Add(faceCount * 4); // Triangle 2
        newTriangles.Add(faceCount * 4 + 2); // Triangle 2
        newTriangles.Add(faceCount * 4 + 3); // Triangle 2

        Vector2 texturePosition;
        texturePosition = grassTop;

        // Add the coordinates of the four corners of the texture in the texture atlas
        newUV.Add(new Vector2(textureWidth * texturePosition.x + textureWidth, textureWidth * texturePosition.y)); // Bottom right
        newUV.Add(new Vector2(textureWidth * texturePosition.x + textureWidth, textureWidth * texturePosition.y + textureWidth)); // Top right
        newUV.Add(new Vector2(textureWidth * texturePosition.x, textureWidth * texturePosition.y + textureWidth)); // Top left
        newUV.Add(new Vector2(textureWidth * texturePosition.x, textureWidth * texturePosition.y)); // Bottom left

    }

    void UpdateMesh() {

        
        // Clear old mesh
        mesh.Clear();

        // Convert mesh lists to arrays, assign to mesh attirbutes
        mesh.vertices = newVertices.ToArray();
        mesh.uv = newUV.ToArray();
        mesh.triangles = newTriangles.ToArray();
        mesh.triangles = newTriangles.ToArray();

        // Optimize mesh for the gpu
        MeshUtility.Optimize(mesh);
        mesh.RecalculateNormals();

        chunkCollider.sharedMesh = null;
        chunkCollider.sharedMesh = mesh;

        // Clear old lists
        newVertices.Clear();
        newUV.Clear();
        newTriangles.Clear();
        faceCount = 0;


    }
}
