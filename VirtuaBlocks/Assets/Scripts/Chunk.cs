using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public enum textureType {

    air, lightGrid

}

public class Chunk : MonoBehaviour {

    // List of all vertices in the chunk
    private List<Vector3> newVertices = new List<Vector3>();
    // List of triangles in the chunk
    private List<int> newTriangles = new List<int>();
    private List<Vector2> newUV = new List<Vector2>();

    private Mesh mesh;
    private MeshCollider chunkCollider;

    // Fractional width and height of the texture square in the atlas
    private float textureWidth = 0.083f;
    private int faceCount;
    private int chunkSize;
    private int chunkX;
    private int chunkY;
    private int chunkZ;
    private World world;
    private GameObject worldGO;

    // The coordinates of the textures in the texture atlas
    private Vector2 lightGrid = new Vector2(1, 11);
    
    public int ChunkSize {
        get
        {
            return chunkSize;
        }
        set
        {
            chunkSize = value;
        }
    }

    public int ChunkX {
        get {
            return chunkX;
        }
        set {
            chunkX = value;
        }
    }

    public int ChunkY
    {
        get {
            return chunkY;
        }
        set {
            chunkY = value;
        }
    }

    public int ChunkZ
    {
        get {
            return chunkZ;
        }
        set {
            chunkZ = value;
        }
    }

    public GameObject WorldGO {
        get {
            return worldGO;
        }
        set {
            worldGO = value;
        }

    }


	// Use this for initialization
	void Start () {

        world = WorldGO.GetComponent("World") as World;
        //private GameObject obj = GameObject.Find("World");
        //World = object.world;
        //world = worldObject.GetComponent<World>() as World;
        mesh = GetComponent<MeshFilter>().mesh;
        chunkCollider = GetComponent<MeshCollider>();

        //CubeTop(0, 0, 0, (byte) TextureType.lightGrid.GetHashCode());
        //CubeNorth(0, 0, 0, (byte)TextureType.lightGrid.GetHashCode());
        //CubeEast(0, 0, 0, (byte)TextureType.lightGrid.GetHashCode());
        //CubeSouth(0, 0, 0, (byte)TextureType.lightGrid.GetHashCode());
        //CubeWest(0, 0, 0, (byte)TextureType.lightGrid.GetHashCode());
        //CubeBottom(0, 0, 0, (byte)TextureType.lightGrid.GetHashCode());
        //UpdateMesh();
        GenerateMesh();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void GenerateMesh() {
        for(int x = 0; x < chunkSize; x++) {
            for(int y = 0; y < chunkSize; y++) {
                for(int z = 0; z < chunkSize; z++) {
                    if(Block(x, y, z) != (byte)textureType.air.GetHashCode()) {
                        //Check if the surrounding cubes are air
                        if(Block(x, y + 1, z) == (byte)textureType.air.GetHashCode()) {
                            CubeTop(x, y, z, Block(x, y, z));
                        }
                        if (Block(x, y - 1, z) == (byte)textureType.air.GetHashCode())
                        {
                            CubeBottom(x, y, z, Block(x, y, z));
                        }
                        if (Block(x + 1, y, z) == (byte)textureType.air.GetHashCode())
                        {
                            CubeEast(x, y, z, Block(x, y, z));
                        }
                        if (Block(x - 1, y, z) == (byte)textureType.air.GetHashCode())
                        {
                            CubeWest(x, y, z, Block(x, y, z));
                        }
                        if (Block(x, y, z + 1) == (byte)textureType.air.GetHashCode())
                        {
                            CubeNorth(x, y, z, Block(x, y, z));
                        }
                        if (Block(x, y, z - 1) == (byte)textureType.air.GetHashCode())
                        {
                            CubeSouth(x, y, z, Block(x, y, z));
                        }

                    }
                }

            }
        }
        UpdateMesh();
    }

    void CubeTop(int x, int y, int z, byte block) {

        // Add the vertices of the cubes top surface
        newVertices.Add(new Vector3(x, y, z + 1));
        newVertices.Add(new Vector3(x + 1, y, z + 1));
        newVertices.Add(new Vector3(x + 1, y, z));
        newVertices.Add(new Vector3(x, y, z));

        Vector2 texturePosition = new Vector2(0, 0);

        if (block == (byte)textureType.lightGrid.GetHashCode()) {
            texturePosition = lightGrid;
        }

        Cube(texturePosition);
    }

    void CubeNorth(int x, int y, int z, byte block)
    {

        // Add the vertices of the cubes top surface
        newVertices.Add(new Vector3(x + 1, y - 1, z + 1));
        newVertices.Add(new Vector3(x + 1, y, z + 1));
        newVertices.Add(new Vector3(x, y, z + 1));
        newVertices.Add(new Vector3(x, y - 1, z + 1));

        Vector2 texturePosition = SetSideTextures(x, y, z, block);

        Cube(texturePosition);
    }

    void CubeEast(int x, int y, int z, byte block)
    {

        // Add the vertices of the cubes top surface
        newVertices.Add(new Vector3(x + 1, y - 1, z));
        newVertices.Add(new Vector3(x + 1, y, z));
        newVertices.Add(new Vector3(x + 1, y, z + 1));
        newVertices.Add(new Vector3(x + 1, y - 1, z + 1));

        Vector2 texturePosition = SetSideTextures(x, y, z, block);

        Cube(texturePosition);
    }

    void CubeSouth(int x, int y, int z, byte block)
    {

        // Add the vertices of the cubes top surface
        newVertices.Add(new Vector3(x, y - 1, z));
        newVertices.Add(new Vector3(x, y, z));
        newVertices.Add(new Vector3(x + 1, y, z));
        newVertices.Add(new Vector3(x + 1, y - 1, z));

        Vector2 texturePosition = SetSideTextures(x, y, z, block);

        Cube(texturePosition);
    }

    void CubeWest(int x, int y, int z, byte block)
    {

        // Add the vertices of the cubes top surface
        newVertices.Add(new Vector3(x, y - 1, z + 1));
        newVertices.Add(new Vector3(x, y, z + 1));
        newVertices.Add(new Vector3(x, y, z));
        newVertices.Add(new Vector3(x, y - 1, z));

        Vector2 texturePosition = SetSideTextures(x, y, z, block);

        Cube(texturePosition);
    }

    void CubeBottom(int x, int y, int z, byte block)
    {

        // Add the vertices of the cubes top surface
        newVertices.Add(new Vector3(x, y - 1, z));
        newVertices.Add(new Vector3(x + 1, y - 1, z));
        newVertices.Add(new Vector3(x + 1, y - 1, z + 1));
        newVertices.Add(new Vector3(x, y - 1, z + 1));

        Vector2 texturePosition = SetSideTextures(x, y, z, block);

        Cube(texturePosition);
    }

    public Vector2 SetSideTextures(int x, int y, int z, byte block) {
        //Sets the side textures according to the texture of CubeTop

        Vector2 texturePosition = new Vector2(0, 0);
        if(block == (byte)textureType.lightGrid.GetHashCode())
        {
            texturePosition = lightGrid;
        }
        return texturePosition;
    }

    void Cube(Vector2 texturePosition) {
        // Add the the six vectors of the two triangles that the surface is comprised of
        newTriangles.Add(faceCount * 4); // Triangle 1
        newTriangles.Add(faceCount * 4 + 1); // Triangle 1
        newTriangles.Add(faceCount * 4 + 2); // Triangle 1

        newTriangles.Add(faceCount * 4); // Triangle 2
        newTriangles.Add(faceCount * 4 + 2); // Triangle 2
        newTriangles.Add(faceCount * 4 + 3); // Triangle 2

        // Add the coordinates of the four corners of the texture in the texture atlas
        newUV.Add(new Vector2(textureWidth * texturePosition.x + textureWidth, textureWidth * texturePosition.y)); // Bottom right
        newUV.Add(new Vector2(textureWidth * texturePosition.x + textureWidth, textureWidth * texturePosition.y + textureWidth)); // Top right
        newUV.Add(new Vector2(textureWidth * texturePosition.x, textureWidth * texturePosition.y + textureWidth)); // Top left
        newUV.Add(new Vector2(textureWidth * texturePosition.x, textureWidth * texturePosition.y)); // Bottom left

        faceCount++;
    }

    byte Block(int x, int y, int z) {
        return world.Block(x + chunkX, y + chunkY, z + chunkZ);
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
        //MeshUtility.Optimize(mesh);
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
