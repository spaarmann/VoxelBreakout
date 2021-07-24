using UnityEngine;

public class Level : MonoBehaviour {
	public GameObject voxelPrefab;
	public Vector3 levelSize;

	public void GenerateLevel() {
        int childCount = transform.childCount;

        for (int i = 0; i < childCount; i++) {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }

        BoxCollider voxelCollider = voxelPrefab.GetComponentInChildren<BoxCollider>();
        Vector3 voxelSize = Vector3.Scale(voxelCollider.size, voxelCollider.transform.lossyScale);

        for (int y = 0; y < levelSize.y; y++) {
	        for (int z = 0; z < levelSize.z; z++) {
		        for (int x = 0; x < levelSize.x; x++) {
			        Vector3 spawnPosition = Vector3.Scale(new Vector3(x, y, z), voxelSize);

			        float normX = x / (float) levelSize.x;
			        float normY = y / (float) levelSize.y;
			        float normZ = z / (float) levelSize.z;

					if (normY > (1f - (Mathf.Pow(2f * normX - 1f, 2f) + Mathf.Pow(2f * normZ - 1f, 2f))))
						continue;
					// turns the structure hollow
					if ((y + 12) / (float)levelSize.y < (1f - (Mathf.Pow(2f * normX - 1f, 2f) + Mathf.Pow(2f * normZ - 1f, 2f))))
						continue;

			        GameObject spawnedVoxel = Instantiate(voxelPrefab, transform);
			        spawnedVoxel.name = $"Voxel {x},{y},{z}";
			        spawnedVoxel.transform.localPosition = spawnPosition;
			        spawnedVoxel.transform.localRotation = Quaternion.identity;
		        }
	        }
        }
    }
}

