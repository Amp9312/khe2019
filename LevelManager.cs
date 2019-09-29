using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    /// <summary>
    /// A Parent transform for our map, this will prevent our hierarchy to be flooded with tiles
    /// </summary>
    [SerializeField]
    private Transform map;

    /// <summary>
    /// Map data fall all the map layers grass trees etc.
    /// </summary>
    [SerializeField]
    private Texture2D[] mapData;

    /// <summary>
    /// A map element represents a tile that we can create in our game.
    /// </summary>
    [SerializeField]
    private MapElement[] mapElements;

    /// <summary>
    /// This tile is used for measuring the distance between tiles
    /// </summary>
    [SerializeField]
    private Sprite defaultTile;

    /// <summary>
    /// The position of the bottom left corner of the screen
    /// </summary>
    private Vector3 WorldStartPos
    {
        get
        {
            return Camera.main.ScreenToWorldPoint(new Vector3(0, 0));
        }
    }

	// Use this for initialization
	void Start ()
    {
        GenerateMap();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Generates our map
    /// </summary>
    private void GenerateMap()
    {
        int height = mapData[0].height;
        int width = mapData[0].width;

        for (int i = 0; i < mapData.Length; i++)//Looks through all our map layers
        {
            for (int x = 0; x < mapData[i].width; x++) //Runs through all pixels on the layer 
            {
                for (int y = 0; y < mapData[i].height; y++)
                {
                    Color c = mapData[i].GetPixel(x, y); //Gets the color of the current pixel

                    //Checks if we have a tile that suits the color of the pixel on the map
                    MapElement newElement = Array.Find(mapElements, e => e.MyColor == c);

                    if (newElement != null) //If we found an element with the correct color
                    {
                        //Calculate x and y position of the tile
                        float xPos = WorldStartPos.x + (defaultTile.bounds.size.x * x);
                        float yPos = WorldStartPos.y + (defaultTile.bounds.size.y * y);

                        //Create the tile
                        GameObject go = Instantiate(newElement.MyElementPrefab);

                        //Set the tile's position
                        go.transform.position = new Vector2(xPos, yPos);

                        //Checks if we are placing a tree
                        if (newElement.MyTileTag == "Tree")
                        {
                            //If we are placing a tree then we need to manage the sort order
                            go.GetComponent<SpriteRenderer>().sortingOrder = height*2 - y*2;
                        }

                        //Make the tile a child of map
                        go.transform.parent = map;

                    }

                }
            }
        }
    }
}

[Serializable]
public class MapElement
{
    /// <summary>
    /// This tile tag, this is used to check what tile we are placing
    /// </summary>
    [SerializeField]
    private string tileTag;

    /// <summary>
    /// The color of the tile, this is used to compare the tile with colors on the map layers
    /// </summary>
    [SerializeField]
    private Color color;

    /// <summary>
    /// Prefab that we use to spawn the til in our world
    /// </summary>
    [SerializeField]
    private GameObject elementPrefab;

    /// <summary>
    /// Property for accessing the prefab
    /// </summary>
    public GameObject MyElementPrefab
    {
       get
        {
            return elementPrefab;
        }
    }

    /// <summary>
    /// Property for accessing the color
    /// </summary>
    public Color MyColor
    {
        get
        {
            return color;
        }
    }

    /// <summary>
    /// Property for accessing the tag
    /// </summary>
    public string MyTileTag
    {
        get
        {
            return tileTag;
        }
    }
}
