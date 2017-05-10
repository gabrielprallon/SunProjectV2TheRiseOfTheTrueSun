using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(SgtTerrainSimplex))]
public class SgtTerrainSimplex_Editor : SgtEditor<SgtTerrainSimplex>
{
	protected override void OnInspector()
	{
		var updateNoise  = false;
		var dirtyTerrain = false;

		BeginError(Any(t => t.Density == 0.0f));
			DrawDefault("Density", ref dirtyTerrain);
		EndError();
		BeginError(Any(t => t.Strength == 0.0f));
			DrawDefault("Strength", ref dirtyTerrain);
		EndError();
		DrawDefault("Seed", ref dirtyTerrain, ref updateNoise);

		if (updateNoise  == true) DirtyEach(t => t.UpdateNoise ());
		if (dirtyTerrain == true) DirtyEach(t => t.DirtyTerrain());
	}
}
#endif

[ExecuteInEditMode]
[AddComponentMenu(SgtHelper.ComponentMenuPrefix + "Terrain Simplex")]
public class SgtTerrainSimplex : SgtTerrainModifier
{
	[Tooltip("The density/frequency/tiling of the displacement")]
	public float Density = 10;

	[Tooltip("The strength of the displacement")]
	[Range(0.0f, 0.5f)]
	public float Strength = 0.5f;

	[Tooltip("The random seed used for the simplex noise")]
	[SgtSeed]
	public int Seed;

	[System.NonSerialized]
	private SgtSimplex simplex;

	public void UpdateNoise()
	{
		if (simplex == null)
		{
			simplex = new SgtSimplex();
		}
		
		simplex.SetSeed(Seed);
	}

	protected override void OnEnable()
	{
		base.OnEnable();

		terrain.OnCalculateDisplacement += OnCalculateHeight;

		UpdateNoise();
	}

	protected override void OnDisable()
	{
		base.OnDisable();

		terrain.OnCalculateDisplacement -= OnCalculateHeight;
	}

	private void OnCalculateHeight(Vector3 localPosition, ref float height)
	{
		localPosition = localPosition.normalized * Density;

		height += simplex.Generate(localPosition.x, localPosition.y, localPosition.z) * Strength;
	}
}