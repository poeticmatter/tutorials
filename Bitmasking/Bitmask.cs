using UnityEngine;
using System.Collections;

public class Bitmask {

	private int [] lookupTable = new int[] {
		//WATER
		3,
		7,	//Q
		6,	//E
		-1,	//QE
		11, //A
		4,	//QA
		-1,	//EA
		-1,	//QEA
		10,	//D
		-1,	//QD
		5,	//ED
		-1,	//QED
		14,	//AD Missing, but good enough
		14, //QAD Missing, but good enough
		14,	//EAD Missing, but good enough
		14,	//QEAD
		13,	//W
		13,	//QW
		13,	//EW
		13,	//QEW
		8,	//AW
		8,	//QAW
		8,	//EAW
		8,	//QEAW
		9,	//DW
		9,	//QDW
		9,	//EDW
		9, 	//QEDW
		15,	//ADW
		15,	//QADW
		15,	//EADW
		15, //QEADW

		//Ground
		2,
		0,	//Q
		1,	//E
		-1,	//QE
		12, //A
		0,	//QA
		-1,	//EA
		-1,	//QEA
		12,	//D
		0,	//QD
		1,	//ED
		-1,	//QED
		12,	//AD
		0,	//QAD
		1,	//EAD
		-1,	//QEAD
		2,	//W
		2,	//QW
		2,	//EW
		2,	//QEW
		2,	//AW
		2,	//QAW
		2,	//EAW
		2,	//QEAW
		2,	//DW
		2,	//QDW
		2,	//EDW
		2, 	//QEDW
		2,	//ADW
		2,	//QADW
		2,	//EADW
		2,	//QEADW

	};
	
	private int [,] map;
	private int x;
	private int y;

	// Directions
	// Q W E
	// A S D

	private const int Q = 1;
	private const int E = 2;
	private const int A = 4;
	private const int D = 8;
	private const int W = 16;
	private const int S = 32;



	public Bitmask (int [,] map) {
		this.map = map;
	}
	
	public int [,] convertMap() {
		int [,] atlasIndices = new int[map.GetLength(0),map.GetLength(1)];
		for (y = 0; y < map.GetLength(0); y++ ){
			for (x = 0; x < map.GetLength(1); x++ ){
				atlasIndices[y,x] = translateTile();
			}
		}
		return atlasIndices;
	}

	
	private int translateTile(){
		int bitmask = getBitmask();
		return lookupTable[bitmask];
	}
	
	//General
	private int getBitmask () {
		int bitmask = 0;
		bitmask += getBitmask(x  , y  , S);
		bitmask += getBitmask(x  , y+1, W);
		bitmask += getBitmask(x+1, y  , D);
		bitmask += getBitmask(x-1, y  , A);
		bitmask += getBitmask(x+1, y+1, E);
		bitmask += getBitmask(x-1, y+1, Q);
		return bitmask;
	}
	

	private int getBitmask(int x, int y, int multiplier){
		if (x < 0 || y < 0 || y >= map.GetLength(0) || x >= map.GetLength(1)){
			return 0;
		}
		return map[y,x]*multiplier;
	}
}
