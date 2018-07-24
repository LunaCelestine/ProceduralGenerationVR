using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class World : MonoBehaviour {

    [SerializeField] byte[,,] worldData;
    [SerializeField] int xDimension = 128;
    [SerializeField] int yDimension = 128;
    [SerializeField] int zDimension = 128;



	// Use this for initialization
	void Start () {

        worldData = new byte[xDimension, yDimension, zDimension];
        for(int x = 0; x < xDimension; x++) {
            for(int y = 0; y < yDimension; y++) {
                for (int z = 0; z < zDimension; z++) {
                    if (y <= 8) {
                        worldData[x, y, z] = (byte)WorldChunk.TextureType.lightGrid.GetHashCode();
                    }
                }
            }
        }
		
	}

    public byte Block(int x, int y, int z) {
        if(x >= xDimension || x < 0 || y >= yDimension || y < 0 || z < 0)
        {
            return (byte)WorldChunk.TextureType.lightGrid.GetHashCode();
        }
        return worldData[x, y, z];
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
