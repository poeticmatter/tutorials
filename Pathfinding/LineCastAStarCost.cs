using UnityEngine;
using System.Collections;

public class LineCastAStarCost : AStarCost {

	private LayerMask _blockingLayer;

	public LineCastAStarCost (LayerMask blockingLayer)
	{
	_blockingLayer = blockingLayer;
	}

	public override float getCost(int toX, int toY, int fromX, int fromY)
	{

		if (toX != fromX && toY != fromY)
		{
			//Diagonal, so check if can move in both orthogonal directions.
			if (isPassable(toX,fromY,fromX,fromY) && isPassable(fromX, toY, fromX, fromY))
			{
				return SpaceConstants.GRID_DIAG;
			}
		}
		else if (isPassable(toX,toY,fromX,fromY))
		{
			return 1;
		}
		return -1;
	}

	private bool isPassable(int toX, int toY, int fromX, int fromY)
	{
		Vector2 start = new Vector2(fromX, fromY);
		Vector2 end = new Vector2(toX, toY);
		RaycastHit2D hit = Physics2D.Linecast(start, end, _blockingLayer);
		return hit.transform == null;
	}
}
