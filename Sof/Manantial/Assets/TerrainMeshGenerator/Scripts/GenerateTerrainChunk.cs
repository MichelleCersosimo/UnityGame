﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GenerateTerrainChunk : MonoBehaviour {
	
	[HideInInspector]
	public Vector3 origin;

	public int index_x = -1;
	public int index_y = -1;

	private int textures_x;
	private int textures_y;

	private Vector3 center;
	private Mesh mesh;
	private Bounds bounds;

	private int side_tile_count;
	private float slope_height;
	private float tile_size;

	private int terrain_chunks_x;
	private int terrain_chunks_y;
	private LevelMap[,] WorldLevelMaps;
	private TileTypeMap[,] WorldTileTypeMaps;

	private MeshFilter mesh_filter;
	private MeshCollider mesh_collider;

	private Mesh ws_mesh;
	private MeshRenderer ws_mesh_renderer;



	private Vector2[] ws_uv_offset = new Vector2[8];
	private Vector2[] ws_uv_animRate = new Vector2[8];


	public TerrainChunk tc;


	public bool isValid = true;

	// generates all terrain chunk data
	public void generate(int index_x, int index_y, float tile_size, float slope_height, int side_tile_count, Vector3 chunk_origin, Vector3 chunk_center, ref LevelMap[,] WorldLevelMaps, TileTypeMap[,] WorldTileTypeMaps, int terrain_chunks_x, int terrain_chunks_y, int textures_x, int textures_y, ref List<int[]> neighbor_level_maps, MeshFilter m_filter) {

		this.index_x = index_x;
		this.index_y = index_y;
		this.tile_size = tile_size;
		this.slope_height = slope_height;
		this.side_tile_count = side_tile_count;
		this.terrain_chunks_x = terrain_chunks_x;
		this.terrain_chunks_y = terrain_chunks_y;
		this.origin = chunk_origin;
		this.center = chunk_center;
		this.textures_x = textures_x;
		this.textures_y = textures_y;
		this.WorldLevelMaps = WorldLevelMaps;
		this.WorldTileTypeMaps = WorldTileTypeMaps;
		int zona = 0;
		gameObject.transform.position = origin;

		mesh_filter = m_filter;
		mesh_collider = gameObject.GetComponent<MeshCollider> ();
		// llamar a metodo terrain chunk en la pos indicada para zona rural igual para ciudad. 
		if (index_x == 0 && index_y == 14) {  // 1 9
			// zona rural = 1
			zona = 1; 
			tc = new TerrainChunk (origin, side_tile_count, tile_size, slope_height, ref WorldLevelMaps[index_y, index_x].levels, WorldTileTypeMaps[index_y, index_x].ttypes, textures_x, textures_y, ref neighbor_level_maps, mesh_filter, zona);
		} else if (index_x == 11 && index_y == 0) { //2 15
			// zona ciudad = 2
			zona = 2;
			tc = new TerrainChunk (origin, side_tile_count, tile_size, slope_height, ref WorldLevelMaps[index_y, index_x].levels, WorldTileTypeMaps[index_y, index_x].ttypes, textures_x, textures_y, ref neighbor_level_maps, mesh_filter, zona);
		} else {
			// default no zone 0 
			tc = new TerrainChunk (origin, side_tile_count, tile_size, slope_height, ref WorldLevelMaps[index_y, index_x].levels, WorldTileTypeMaps[index_y, index_x].ttypes, textures_x, textures_y, ref neighbor_level_maps, mesh_filter, zona);
		}
		mesh = tc.getMesh ();
		bounds = mesh.bounds;
		mesh_filter.mesh = mesh;
		mesh_filter.sharedMesh = mesh;
		mesh_collider.sharedMesh = mesh;

		//GetComponent<MeshRenderer> ().enabled = false;

	}

	public void getTileIndexesFromPosition(Vector3 position, out int tile_index_x, out int tile_index_z) {
		tile_index_x = (int) Mathf.Abs(position.x / tile_size);
		tile_index_z = (int) Mathf.Abs(position.z / tile_size);
	}

	// get center position of chunk
	public Vector3 GetCenter() {
		return center;
	}


	public int[,] getSceneryMap() {
		return tc.getSceneryMap ();
	}

	public PathType[,] getPathMap() {
		return tc.getPathMap ();
	}

	// generate water surface mesh
	public void generateWaterSurface (GameObject ws_go) {

		ws_mesh = tc.getWSMesh ();
		ws_go.GetComponent<MeshFilter> ().mesh = ws_mesh;
		ws_go.GetComponent<MeshCollider> ().sharedMesh = tc.getWSMesh ();

		ws_mesh_renderer = ws_go.GetComponent<MeshRenderer> ();
		/*float alpha = 0.3f;
		Color oldColor = mrenderer.material.color;
		Color newColor = new Color(oldColor.r, oldColor.b, oldColor.g, alpha);          
		mrenderer.material.SetColor("_Color", newColor); */
		//Debug.Log (ws_mesh_renderer.materials[0].name);






		ws_uv_offset[(int) WaterFlowDirection.N] = Vector2.zero;
		ws_uv_offset[(int) WaterFlowDirection.NE] = Vector2.zero;
		ws_uv_offset[(int) WaterFlowDirection.E] = Vector2.zero;
		ws_uv_offset[(int) WaterFlowDirection.SE] = Vector2.zero;
		ws_uv_offset[(int) WaterFlowDirection.S] = Vector2.zero;
		ws_uv_offset[(int) WaterFlowDirection.SW] = Vector2.zero;
		ws_uv_offset[(int) WaterFlowDirection.W] = Vector2.zero;
		ws_uv_offset[(int) WaterFlowDirection.NW] = Vector2.zero;

		ws_uv_animRate[(int) WaterFlowDirection.N] = new Vector2( 0f, 1.0f );
		ws_uv_animRate[(int) WaterFlowDirection.NE] = new Vector2( 1.0f, 1.0f );
		ws_uv_animRate[(int) WaterFlowDirection.E] = new Vector2( 1.0f, 0f );
		ws_uv_animRate[(int) WaterFlowDirection.SE] = new Vector2( 1.0f, -1.0f );
		ws_uv_animRate[(int) WaterFlowDirection.S] = new Vector2( 0f, -1.0f );
		ws_uv_animRate[(int) WaterFlowDirection.SW] = new Vector2( -1.0f, -1.0f );
		ws_uv_animRate[(int) WaterFlowDirection.W] = new Vector2( -1.0f, 0f );
		ws_uv_animRate[(int) WaterFlowDirection.NW] = new Vector2( -1.0f, 1.0f );

	}

	// load chunk scenery objects
	public void loadScenery(ref Stack<GameObject> scenery_obj_pool) {
		tc.renderScenery (index_x, index_y, ref scenery_obj_pool);
	}

	// unload chunk scenery objects
	public void unloadScenery(ref Stack<GameObject> scenery_obj_pool) {
		tc.unloadScenery (ref scenery_obj_pool);
	}

	// load chunk path objects
	public void loadPaths(ref Stack<GameObject> path_obj_pool) {
		tc.renderPaths (index_x, index_y, ref path_obj_pool);
	}
	
	// unload chunk path objects
	public void unloadPaths(ref Stack<GameObject> path_obj_pool) {
		tc.unloadPaths (ref path_obj_pool);
	}

	// destroy a scenery object on this chunk
	public void destroySceneryObject(GameObject obj, ref Stack<GameObject> scenery_obj_pool) {
		tc.unloadSceneryObject (obj, ref scenery_obj_pool);
		tc.destroySceneryObject (obj);
	}

	// destroy a scenery object on this chunk
	public void destroyPathObject(int lmap_index_x, int lmap_index_y) {
		tc.getPathMap () [lmap_index_x, lmap_index_y] = PathType.empty;
	}

	// returns true if the tile contains a path object
	public bool tileContainsPath(int lmap_index_x, int lmap_index_y) {
		return tc.getPathMap () [lmap_index_x, lmap_index_y] != PathType.empty;
	}

	private void calcUvOffsets() {
		for (int i = 0; i < 8; ++i) {
			ws_uv_offset [i] += ( ws_uv_animRate [i] * Time.deltaTime );
		}
	}

	// displace material texture
	private void displaceMat(ref MeshRenderer renderer, int mat_index, Vector2 uv_offset) {
		renderer.materials[mat_index].SetTextureOffset("_MainTex", uv_offset);
	}

	public bool tileIsSuitableForScenery(int lmap_index_x, int lmap_index_y) {
		bool freeSpace = tc.getSceneryMap () [lmap_index_x, lmap_index_y] == GameController.gameController.spriteMapper.getEmpty();
		freeSpace = freeSpace && tc.getPathMap () [lmap_index_x, lmap_index_y] == PathType.empty;
		return freeSpace && tc.isSuitableForScenery (lmap_index_y, lmap_index_x);
	}

	public bool tileIsSuitableForBridge(int lmap_index_x, int lmap_index_y) {
		bool freeSpace = tc.getPathMap () [lmap_index_x, lmap_index_y] == PathType.empty;
		return freeSpace && tc.isSuitableForBridge (lmap_index_y, lmap_index_x);
	}

	void LateUpdate() {

		if (isValid) {
			//texture displace
			calcUvOffsets ();
			
			// displace material textures every frame to simulate water flow
			displaceMat (ref ws_mesh_renderer, (int)WaterFlowDirection.N, ws_uv_offset [(int)WaterFlowDirection.N]);
			displaceMat (ref ws_mesh_renderer, (int)WaterFlowDirection.NE, ws_uv_offset [(int)WaterFlowDirection.NE]);
			displaceMat (ref ws_mesh_renderer, (int)WaterFlowDirection.E, ws_uv_offset [(int)WaterFlowDirection.E]);
			displaceMat (ref ws_mesh_renderer, (int)WaterFlowDirection.SE, ws_uv_offset [(int)WaterFlowDirection.SE]);
			displaceMat (ref ws_mesh_renderer, (int)WaterFlowDirection.S, ws_uv_offset [(int)WaterFlowDirection.S]);
			displaceMat (ref ws_mesh_renderer, (int)WaterFlowDirection.SW, ws_uv_offset [(int)WaterFlowDirection.SW]);
			displaceMat (ref ws_mesh_renderer, (int)WaterFlowDirection.W, ws_uv_offset [(int)WaterFlowDirection.W]);
			displaceMat (ref ws_mesh_renderer, (int)WaterFlowDirection.NW, ws_uv_offset [(int)WaterFlowDirection.NW]);
		}

	}

}
