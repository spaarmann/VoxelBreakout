using Unity.Mathematics;
using UnityEngine;

public class Level : MonoBehaviour {
	public GameObject voxelPrefab;
	public int3 levelSize;

	public float hollowHeightAdjustment = 4f;

	public void GenerateLevel() {
		int childCount = transform.childCount;

		for (int i = 0; i < childCount; i++) {
			DestroyImmediate(transform.GetChild(0).gameObject);
		}

		BoxCollider voxelCollider = voxelPrefab.GetComponentInChildren<BoxCollider>();
		float3 voxelSize = voxelCollider.size * (float3) voxelCollider.transform.lossyScale;

		for (int y = 0; y < levelSize.y; y++) {
			for (int z = 0; z < levelSize.z; z++) {
				for (int x = 0; x < levelSize.x; x++) {
					float3 spawnPosition = new float3(x, y, z) * voxelSize;

					float normX = x / (float) levelSize.x;
					float normY = y / (float) levelSize.y;
					float normZ = z / (float) levelSize.z;

					if (normY > EvaluateHeightmap(normX, normZ))
						continue;

					// turns the structure hollow
					if ((y + hollowHeightAdjustment) / (float) levelSize.y < EvaluateHeightmap(normX, normZ))
						continue;

					GameObject spawnedVoxel = Instantiate(voxelPrefab, transform);
					spawnedVoxel.name = $"Voxel {x},{y},{z}";
					spawnedVoxel.transform.localPosition = spawnPosition;
					spawnedVoxel.transform.localRotation = Quaternion.identity;
				}
			}
		}
	}

	private float EvaluateHeightmap(float normX, float normZ) {
		return 1f - (Mathf.Pow(2f * normX - 1f, 2f) + Mathf.Pow(2f * normZ - 1f, 2f));
	}
}

