using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    [SerializeField] private Texture2D terrainTexture;
    [SerializeField] private float placementLightnessThreshold = 0.5f;
    [SerializeField] private float spawnHeightMultiplier = 3.0f;
    [SerializeField] private bool groupObjects = true;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public void GeneratePlatforms(Texture2D spawningTexture, GameObject platformPrefab, GameObject objectHolder)
    {
        Color[] texturePixels = spawningTexture.GetPixels();
        for (int x = 0; x < spawningTexture.width; x += 2)
        {
            for (int z = 0; z < spawningTexture.height; z += 2)
            {
                float lightnessValue = texturePixels[z * spawningTexture.width + x].grayscale;
                if (lightnessValue > placementLightnessThreshold)
                {
                    Vector3 spawnPosition = new Vector3(x, (lightnessValue*spawnHeightMultiplier), z);
                    GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
                    if (groupObjects && objectHolder != null)
                    {
                        newPlatform.transform.SetParent(objectHolder.transform);
                    }
                    newPlatform.name = "Platform (" + x + ", " + z + ")";
                }
            }
        }
    }
}
