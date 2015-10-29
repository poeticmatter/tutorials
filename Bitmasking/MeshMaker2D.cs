using UnityEngine;
using System.Collections;

public class MeshMaker2D {
	private int width;
	private int height;
	private int [,] atlasIndices;
	private int atlasWidth;
	private int atlasHeight;

	private Mesh mesh;
	private Vector3 origin;
	private Vector3 [] vertices;
	private int [] triangles;
	private Vector2 [] uvMap;
	private Vector3 [] normals;
	private float buffer;

	public MeshMaker2D(int width, int height, bool centered) {
		this.width = width;
		this.height = height;
		origin = centered ? new Vector3(-width/2f, -height/2f, 0) : Vector3.zero;
		atlasIndices = new int[,] {{0}};
		atlasWidth = 1;
		atlasHeight = 1;
		makeQuads();
	}

	public Mesh getMesh(){
		mesh = new Mesh();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		makeNormals();
		mesh.normals = normals;
		return mesh;
	}

	private void makeQuads() {
		vertices = new Vector3 [width*height*4];
		triangles = new int [width*height*6];
		int i = 0;
		for (int y = 0; y < height; y++) {
			for (int x = 0; x < width; x++) {
				makeVertices(x,y,i);
				makeTriangles(i);
				i++;
			}
		}
	}

	private void makeVertices(int x, int y, int index) {
		int vertexIndex = index * 4;
		vertices[vertexIndex++] = origin + new Vector3(x,  y,   0);
		vertices[vertexIndex++] = origin + new Vector3(x,  y+1, 0);
		vertices[vertexIndex++] = origin + new Vector3(x+1,y+1, 0);
		vertices[vertexIndex++] = origin + new Vector3(x+1,y,   0);
	}

	private void makeTriangles(int index) {
		int vertexIndex = index * 4;
		int triangleIndex = index * 6;
		triangles[triangleIndex++] = vertexIndex;
		triangles[triangleIndex++] = vertexIndex+1;
		triangles[triangleIndex++] = vertexIndex+2;
		triangles[triangleIndex++] = vertexIndex+2;
		triangles[triangleIndex++] = vertexIndex+3;
		triangles[triangleIndex++] = vertexIndex;
	}

	public Vector2 [] getUVmap(int atlasWidth, int atlasHeight, int [,] atlasIndices, float buffer) {
		this.atlasIndices = atlasIndices;
		this.atlasWidth = atlasWidth;
		this.atlasHeight = atlasHeight;
		this.buffer = buffer;
		vertexIndex = 0;
		uvMap = new Vector2[width*height*4];
		for (int y = 0; y < height; y++) {
			for (int x = 0; x < width; x++) {
				makeUVmap(x,y);
			}
		}
		return uvMap;
	}
	private int vertexIndex;
	private void makeUVmap(int x, int y) {
		int atlastIndex = atlasIndices[y,x];
		float xMultiplier = 1f/atlasWidth;
		float yMultiplier = 1f/atlasHeight;
		int xAtlas = atlastIndex % atlasWidth;
		int yAtlas = (atlastIndex / atlasWidth);
		uvMap[vertexIndex++] = new Vector2(xAtlas * xMultiplier + buffer, yAtlas * yMultiplier + buffer);
		uvMap[vertexIndex++] = new Vector2(xAtlas * xMultiplier + buffer, (yAtlas+1) * yMultiplier - buffer);
		uvMap[vertexIndex++] = new Vector2((xAtlas+1) * xMultiplier - buffer, (yAtlas+1) * yMultiplier - buffer);
		uvMap[vertexIndex++] = new Vector2((xAtlas+1) * xMultiplier, yAtlas * yMultiplier + buffer);
	}

	private void makeNormals() {
		normals = new Vector3[vertices.Length];
		for (int i = 0; i < height; i++) {
			normals[i] = Vector3.back;
		}
	}
}
