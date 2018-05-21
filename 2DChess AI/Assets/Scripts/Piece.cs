using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {
	// example: p, k, n
	public string type;
	//white: true
	public bool color;
	public Piece(string type, bool color)
	{
		this.type = type;
		this.color = color;
	}

	public void setType(string t)
	{
		type = t;
	}
	public string getName(){
		return name;
	}
	public bool getColor(){
		return color;
	}
}
