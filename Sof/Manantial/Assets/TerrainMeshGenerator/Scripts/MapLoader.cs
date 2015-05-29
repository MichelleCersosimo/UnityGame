using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MapLoader {

	string FilePath;
	Texture2D loadedImage;
	int size_x;
	int size_y;

	float grayscale_zero;
	int level_zero;
	int level_count;
	float level_difference;
	int[,] Levels;

	public void openFile(int size_x, int size_y, string path) {

		initSizes (size_x, size_y);

		FilePath = Application.dataPath + path;
		if (System.IO.File.Exists (FilePath)) {
			byte[] bytes = System.IO.File.ReadAllBytes (FilePath);
			loadedImage = new Texture2D (1, 1);
			loadedImage.LoadImage (bytes);
		} 
		else {
			Debug.LogError("Unable to open file: "+path);
		}

	}

	public List<TileType[,]> getTileTypeMapList (int chunk_side_length) {
	
		List<TileType[,]> tile_types = new List<TileType[,]> ();
		
		if (size_x % chunk_side_length != 0 || size_x != size_y) {
			Debug.LogError("Error 1A");
			return tile_types;
		}
		
		int side_chunks = size_x / chunk_side_length;
		TileType[,] chTypes = getTileTypes ();

		for(int i = 0; i < side_chunks; ++i) {
			for(int j = 0; j < side_chunks; ++j) {
				
				TileType[,] chunk_types = new TileType[chunk_side_length, chunk_side_length];
				for(int m = 0; m < chunk_side_length; ++m) {
					for(int n = 0; n < chunk_side_length; ++n) {
						
						chunk_types[m, n] = chTypes[i * chunk_side_length + m, j * chunk_side_length + n];
						
					}
				}
				tile_types.Add (chunk_types);
				
			}
		}
		
		return tile_types;

	}

	private TileType[,] getTileTypes () {
	
		Color[] pix = loadedImage.GetPixels();
		TileType[,] types = new TileType[size_y, size_x];
		for (int i = 0; i < size_y; ++i) {
			for(int j = 0; j < size_x; ++j) {
				types[i, j] = getTypeFromColor(pix[i * size_y + j]);
			}
		}

		return types;

	}

	private TileType getTypeFromColor(Color color) {

		Vector3 rgb = convertColorToStandard (new Vector3 (color.r, color.g, color.b));
	
		if(rgb.x == 186 && rgb.y == 77 && rgb.z == 187) {
			// Pink
		}

		if(rgb.x == 0 && rgb.y == 136 && rgb.z == 45) {
			// Bluish green
			return TileType.high_zone;
		}

		if(rgb.x == 129 && rgb.y == 187 && rgb.z == 77) {
			// Yellowish green
			return TileType.medium_zone;
		}

		if(rgb.x == 145 && rgb.y == 110 && rgb.z == 74) {
			// Brown
			return TileType.dirt;
		}

		if(rgb.x == 89 && rgb.y == 105 && rgb.z == 114) {
			// Brown
			return TileType.rocky;
		}

		return TileType.high_zone;
	
	}

	private Vector3 convertColorFromStandard(Vector3 color_standard) {
		return new Vector3(color_standard.x/255.0f, color_standard.y/255.0f, color_standard.z/255.0f);
	}

	private Vector3 convertColorToStandard(Vector3 color_norm) {
		return new Vector3( (int) (color_norm.x*255.0f), (int) (color_norm.y*255.0f), (int) (color_norm.z*255.0f) );
	}

	public List<int[,]> getLevelMapList (int level_count, float grayscale_zero, int chunk_side_length, out int chunks_x, out int chunks_y) {

		setLevels(level_count, grayscale_zero);
		List<int[,]> result = new List<int[,]> ();

		if (size_x % chunk_side_length != 0 || size_x != size_y) {
			Debug.LogError("Error 1A");
			chunks_x = 0;
			chunks_y = 0;
			return result;
		}

		int side_chunks = size_x / chunk_side_length;

		for(int i = 0; i < side_chunks; ++i) {
			for(int j = 0; j < side_chunks; ++j) {

				int[,] chunk_levels = new int[chunk_side_length, chunk_side_length];
				for(int m = 0; m < chunk_side_length; ++m) {
					for(int n = 0; n < chunk_side_length; ++n) {
						
						chunk_levels[m, n] = Levels[i * chunk_side_length + m, j * chunk_side_length + n];
						
					}
				}
				result.Add (chunk_levels);

			}
		}

		chunks_x = side_chunks;
		chunks_y = side_chunks;

		return result;

	}

	private void setLevels(int level_count, float grayscale_zero) {

		initAttributes (level_count, grayscale_zero);

		Color[] pix = loadedImage.GetPixels();
		Levels = new int[size_y, size_x];
		for (int i = 0; i < size_y; ++i) {
			for(int j = 0; j < size_x; ++j) {
				Levels[i, j] = getLevelFromGrayscale(pix[i * size_y + j].grayscale);
			}
		}

	}

	private int getLevelFromGrayscale(float grayscale_value) {
		int absolute_level = (int) (grayscale_value * level_count); 
		return absolute_level - level_zero;
	}


	private void initSizes(int size_x, int size_y) {
		this.size_x = size_x;
		this.size_y = size_y;
	}

	private void initAttributes(int level_count, float grayscale_zero) {
		this.level_count = level_count;
		this.grayscale_zero = grayscale_zero;
		this.level_zero = (int)(grayscale_zero * level_count);
		this.level_difference = 1f / level_count;
	}



}
