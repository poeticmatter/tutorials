using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AStarNode2D : AStarNode {
	private float _x;
	public float x { get { return _x;} }

	private float _y;
	public float y { get { return _y;} }

	public AStarNode2D(AStarNode parent,AStarNode goalNode,float cost,float x, float y) : base(parent, goalNode, cost){
		_x = x;
		_y = y;
	}

	private void addSuccessor(List<AStarNode> successors,float x,float y, bool diagonal) {
		float currentCost = Map2D.getCost(x,y,_x,_y);
		if(currentCost == -1) {
			return;
		}
		AStarNode2D newNode = new AStarNode2D(this,goalNode, cost + currentCost, x, y);
		if(newNode.isSameState(parent)) {
			return;
		}
		successors.Add(newNode);
	}

	public override bool isSameState(AStarNode node) {
		if (node == null) {
			return false;
		}
		AStarNode2D node2d = (AStarNode2D) node;
		return node2d.x == _x && node2d.y == _y;
	}
	
	public override float calculateGoalEstimate() {
		if(goalNode != null) {
			AStarNode2D node2d = (AStarNode2D) goalNode;
			float xd = _x - node2d.x;
			float yd = _y - node2d.y;
			// "Euclidean distance" - Used when search can move at any angle.
			return Mathf.Sqrt((xd*xd) + (yd*yd));
			// "Manhattan Distance" - Used when search can only move vertically and 
			// horizontally.
			//GoalEstimate = Mathf.Abs(xd) + Mathf.Abs(yd); 
			// "Diagonal Distance" - Used when the search can move in 8 directions.
			//return Mathf.Max(Mathf.Abs(xd),Mathf.Abs(yd))*10;
		} else {
			Debug.LogError("No goal node");
			return 0;
		}
	}

	public override List<AStarNode> getSuccessors() {
		List<AStarNode> successors = new List<AStarNode>();
		float i = SpaceConstants.GRID_INCREMENT;
		addSuccessor(successors,_x-i,_y  , false);
		addSuccessor(successors,_x-i,_y-i, true);
		addSuccessor(successors,_x  ,_y-i, false);
		addSuccessor(successors,_x+i,_y-i, true);
		addSuccessor(successors,_x+i,_y  , false);
		addSuccessor(successors,_x+i,_y+i, true);
		addSuccessor(successors,_x  ,_y+i, false);
		addSuccessor(successors,_x-i,_y+i, true);
		return successors;
	}	

	public override void printNodeInfo() {
		Debug.Log(ToString());
	}

	public override string ToString() {
		return string.Format("X:\t{0}\tY:\t{1}\tCost:\t{2}\tEst:\t{3}\tTotal:\t{4}",_x,_y,cost,goalEstimate,totalCost);
	}
}
