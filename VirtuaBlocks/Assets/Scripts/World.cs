using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Noise;


public class World : MonoBehaviour {

    [SerializeField] GameObject chunk;
    [SerializeField] int xDimension = 16;
    [SerializeField] int yDimension = 16;
    [SerializeField] int zDimension = 16;
    [SerializeField] int chunkSize = 16;

    private byte[,,] worldData;
    private Chunk[,,] chunks;



    // Use this for initialization
    void Start () {

        worldData = new byte[xDimension, yDimension, zDimension];
        for(int x = 0; x < xDimension; x++) {
            for (int z = 0; z < zDimension; z++) {
                int grid = PerlinNoise(x, 0, z, 10f, 3f, 1.2f);
                grid += PerlinNoise(x, 200, z, 20f, 8f, 0f) + 5;
                for (int y = 0; y < yDimension; y++) {
                    if(y >= grid) {
                        worldData[x, y, z] = (byte)textureType.air.GetHashCode();
                    }
                    else if (y <= grid)
                    {
                        worldData[x, y, z] = (byte)textureType.lightGrid.GetHashCode();
                    }
                    
                }
            }
        }

        chunks = new Chunk[Mathf.FloorToInt(xDimension / chunkSize), Mathf.FloorToInt(yDimension / chunkSize), Mathf.FloorToInt(zDimension / chunkSize)];
		for(int x = 0; x < chunks.GetLength(0); x++) {
            for (int y = 0; y < chunks.GetLength(1); y++) {
                for (int z = 0; z < chunks.GetLength(2); z++) {
                    GameObject newChunk = Instantiate(chunk, new Vector3(x * chunkSize, y * chunkSize, z * chunkSize), new Quaternion(0, 0, 0, 0)) as GameObject;
                    chunks[x, y, z] = newChunk.GetComponent("Chunk") as Chunk;
                    chunks[x, y, z].WorldGO = gameObject;
                    chunks[x, y, z].ChunkSize = chunkSize;
                    chunks[x, y, z].ChunkX = x * chunkSize;
                    chunks[x, y, z].ChunkY = y * chunkSize;
                    chunks[x, y, z].ChunkZ = z * chunkSize;

                }
            }
        }
	}

    public int PerlinNoise(int x, int y, int z, float scale, float height, float power) {
        float perlinValue;
        perlinValue = Noise.Noise.GetNoise((double)x / scale, (double)y / scale, (double)z / scale);
        perlinValue *= height;
        if(power != 0) {
            perlinValue = Mathf.Pow(perlinValue, power);
        }
        return (int)perlinValue;
    }

    public byte Block(int x, int y, int z) {
        if(x >= xDimension || x < 0 || y >= yDimension || y < 0 || z >= zDimension || z < 0)
        {
            return (byte)textureType.lightGrid.GetHashCode();
        }
        return worldData[x, y, z];
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
