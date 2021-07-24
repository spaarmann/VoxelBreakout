using Unity.Mathematics;
using UnityEngine;

public class Level : MonoBehaviour {
	public Voxel voxelPrefab;
	public int3 levelSize;

	public float hollowHeightAdjustment = 4f;

	private Voxel[] voxels;

	private int VoxelIdx(int3 v) => v.x + levelSize.x * (v.z + levelSize.z * v.y);

	public Voxel this[int3 v] {
		get => voxels[VoxelIdx(v)];
		set => voxels[VoxelIdx(v)] = value;
	}
	public Voxel this[int x, int y, int z] {
		get => voxels[VoxelIdx(new int3(x, y, z))];
		set => voxels[VoxelIdx(new int3(x, y, z))] = value;
	}

	public void GenerateLevel() {
		if (voxels != null) {
			foreach (Voxel voxel in voxels) {
				if (voxel != null)
					DestroyImmediate(voxel.gameObject);
			}
		}

		voxels = new Voxel[levelSize.x * levelSize.y * levelSize.z];

		BoxCollider voxelCollider = voxelPrefab.GetComponentInChildren<BoxCollider>();
		float3 voxelSize = voxelCollider.size * (float3)voxelCollider.transform.lossyScale;

		for (int y = 0; y < levelSize.y; y++) {
			for (int z = 0; z < levelSize.z; z++) {
				for (int x = 0; x < levelSize.x; x++) {
					float3 spawnPosition = new float3(x, y, z) * voxelSize;

					float normX = x / (float)levelSize.x;
					float normY = y / (float)levelSize.y;
					float normZ = z / (float)levelSize.z;

					if (normY > EvaluateHeightmap(normX, normZ))
						continue;

					// turns the structure hollow
					if ((y + hollowHeightAdjustment) / (float)levelSize.y < EvaluateHeightmap(normX, normZ))
						continue;

					Voxel spawnedVoxel = Instantiate(voxelPrefab, transform);
					spawnedVoxel.gameObject.name = $"Voxel {x},{y},{z}";
					spawnedVoxel.transform.localPosition = spawnPosition;
					spawnedVoxel.transform.localRotation = Quaternion.identity;

					this[x, y, z] = spawnedVoxel;
				}
			}
		}
	}

	private float EvaluateHeightmap(float normX, float normZ) =>
		1f - (Mathf.Pow(2f * normX - 1f, 2f) + Mathf.Pow(2f * normZ - 1f, 2f));
}