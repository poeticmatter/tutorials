using UnityEngine;
using System.Collections;

public class MeshMakerTest : MonoBehaviour {
	public Vector2 atlasSize;
	public enum Alignment {Centered, TopLeft};
	public Alignment alightment;
	public Material material;
	public float buffer;

	void Awake () {
		MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
		MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
		meshRenderer.material = material;
		MeshMaker2D meshMaker = new MeshMaker2D(Map2D.getWidth(), Map2D.getHeight(), alightment == Alignment.Centered);
		Mesh mesh = meshMaker.getMesh();
		Bitmask mapConverter = new Bitmask(Map2D.map);
		mesh.uv = meshMaker.getUVmap((int)atlasSize.x, (int)atlasSize.y, mapConverter.convertMap(), buffer);
		meshFilter.mesh = mesh;
	}
}
