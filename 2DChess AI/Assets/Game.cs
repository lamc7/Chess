﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {
	string[,] chessboard = {
		// first letter = color, second letter = piece
		// R = rook, N = knight, B = bishop, Q = queen, K = king, P = pawn
		{"BR","BN","BB","BQ","BK","BB","BN","BR"},
		{"BP","BP","BP","BP","BP","BP","BP","BP"},
		{"  ","  ","  ","  ","  ","  ","  ","  "},
		{"  ","  ","  ","  ","  ","  ","  ","  "},
		{"  ","  ","  ","  ","  ","  ","  ","  "},
		{"  ","  ","  ","  ","  ","  ","  ","  "},
		{"WP","WP","WP","WP","WP","WP","WP","WP"},
		{"WR","WN","WB","WQ","WK","WB","WN","WR"}
	};
	// for castling
	bool WKingMoved = false;
	bool BKingMoved = false;
	bool BLRookMoved = false;
	bool BRRookMoved = false;
	bool WLRookMoved = false;
	bool WRRookMoved = false;
	string currentPlayer = "W";
	bool whiteChecked = false;
	bool blackChecked = false;
	bool Stalemate = false;
	// whiteCheckmate = black"s win
	bool whiteCheckmate = false;
	// blackCheckmate = white"s win
	bool blackCheckmate = false;
	public void update(){
		if (!chessboard [0,0].Equals ("BR"))
			BLRookMoved = true;
		else if (!chessboard [0,7].Equals ("BR"))
			BRRookMoved = true;
		else if (!chessboard [7,0].Equals ("WR"))
			WLRookMoved = true;
		else if (!chessboard [7,7].Equals ("WR"))
			WRRookMoved = true;
		if (!chessboard[0,4].Equals("BK"))
			BKingMoved = true;
		else if (!chessboard[7,4].Equals("WK"))
			WKingMoved = true;
		if (currentPlayer.Equals( "W")) {
			whiteChecked = isChecked ("W");
			bool sm = isStalemate("W");
			if (sm) {
				if (isStalemate("W")) {
					whiteCheckmate = true;
				} else
					Stalemate = true;
			}
		} else {
			blackChecked = isChecked ("B");
			bool sm = isStalemate("B");
			if (sm) {
				if (isStalemate("B")) {
					blackCheckmate = true;
				} else
					Stalemate = true;
			}
		}
	}
	public void move (int rFrom, int cFrom, int rTo, int cTo){
		if (chessboard[rFrom,cFrom].StartsWith(currentPlayer) && !chessboard[rTo,cTo].StartsWith(currentPlayer)&& isValidMove(rFrom,cFrom,rTo,cTo)){
			string to = chessboard[rTo,cTo];
			chessboard[rTo,cTo] = chessboard[rFrom,cFrom];
			chessboard[rFrom,cFrom] = to;
			if (chessboard[rFrom,cFrom].StartsWith(currentPlayer)){
				chessboard[rFrom,cFrom]= "  ";
			}
			if (isChecked(currentPlayer)){
				chessboard[rFrom,cFrom] = chessboard[rTo,cTo];
				chessboard[rTo,cTo] = to;
			}
			else{
				chessboard[rFrom,cFrom] = "  ";
				if (currentPlayer.Equals("W"))
					currentPlayer = "B";
				else
					currentPlayer = "W";
				update();
			}
		}
	}
	public bool isValidMove(int rFrom, int cFrom, int rTo, int cTo){
		//check if coordinates are valid
		if (!(rFrom == rTo && cFrom == cTo) && 
			rFrom >= 0 && cFrom >= 0 && rFrom < 8 && cFrom < 8 
			&& rTo >= 0 && cTo >= 0 && rTo < 8 && cTo < 8
			&& !chessboard [rFrom, cFrom].Equals ("  ")) {
			if (chessboard [rFrom, cFrom].Equals ("WP") && cFrom == cTo) {
				//check for white pawn moving (not taking)
				if (chessboard [rTo, cTo].Equals ("  ")) {
					if (rTo == rFrom + 1)
						return true;
					else if (rFrom == 6 && rTo == rFrom + 2)
						return true;
				}
				//check for white taking pieces
				else if (rTo == rFrom + 1 && cTo == cFrom + 1 || cTo == cFrom - 1 && !chessboard [rTo, cTo].StartsWith("W")) {
					return true;
				}
			} else if (chessboard [rFrom, cFrom].Equals ("BP") && cFrom == cTo) {
				//check for black pawn moving (not taking)
				if (chessboard [rTo, cTo].Equals ("  ")) {
					if (rTo == rFrom - 1)
						return true;
					else if (rFrom == 6 && rTo == rFrom - 2)
						return true;
				}
				//check for black taking pieces
				else if (rTo == rFrom - 1 && cTo == cFrom + 1 || cTo == cFrom - 1 && !chessboard [rTo, cTo].StartsWith("W")) {
					return true;
				}
			} else if (chessboard [rFrom, cFrom].Equals ("WR") || chessboard [rFrom, cFrom].Equals ("BR") || chessboard [rFrom, cFrom].Equals ("WQ") || chessboard [rFrom, cFrom].Equals ("BQ")) {
				if (rFrom == rTo || cFrom == cTo) {
					int left = 0;
					int right = 7;
					int up = 0;
					int down = 7;
					for (int r = 0; r < rFrom; r++) {
						if (chessboard [r, cFrom].ToCharArray()[0] == chessboard [rFrom, cFrom].ToCharArray() [0]) {
							up = r + 1;
						} else {
							up = r;
						}
					}
					for (int r = 7; r > rFrom; r--) {
						if (chessboard [r, cFrom].ToCharArray()[0] == chessboard [rFrom, cFrom].ToCharArray() [0]) {
							down = r - 1;
						} else {
							down = r;
						}
					}
					for (int c = 0; c < cFrom; c++) {
						if (chessboard [rFrom, c].ToCharArray()[0] ==(chessboard [rFrom, cFrom].ToCharArray()[0])) {
							left = c + 1;
						} else {
							left = c;
						}
					}
					for (int c = 7; c > cFrom; c--) {
						if (chessboard [rFrom, c].ToCharArray()[0] ==(chessboard [rFrom, cFrom].ToCharArray()[0])) {
							right = c - 1;
						} else {
							right = c;
						}
					}
					if (rFrom == rTo) {
						if (rTo >= down && rTo <= up)
							return true;
					} else if (cFrom == cTo) {
						if (cTo >= left && cTo <= right)
							return true;
					}
				} else if (chessboard [rFrom, cFrom].Equals ("WN") || chessboard [rFrom, cFrom].Equals ("BN")) {
					if (chessboard [rTo, cTo].ToCharArray()[0] !=chessboard [rFrom, cFrom].ToCharArray()[0]) {
						if ((Mathf.Abs (rFrom - rTo) == 2 && Mathf.Abs (cFrom - cTo) == 1) 
							|| (Mathf.Abs (cFrom - cTo) == 2 && Mathf.Abs (rFrom - rTo) == 1))
							return true;
					}
				} else if (chessboard [rFrom, cFrom].Equals ("WB") || chessboard [rFrom, cFrom].Equals ("BB")) {
					if (Mathf.Abs (rFrom - rTo) == Mathf.Abs (cFrom - cTo)) {
						bool valid = true;
						if (rFrom < rTo && cFrom < cTo) {
							for (int d = 0; d < Mathf.Abs(rFrom-rTo); d++) {
								if (!chessboard[rFrom+d,cFrom+d].Equals("  "))
									valid = false;
							}
						}
						else if (rFrom > rTo && cFrom < cTo) {
							for (int d = 0; d < Mathf.Abs(rFrom-rTo); d++) {
								if (!chessboard[rFrom-d,cFrom+d].Equals("  "))
									valid = false;
							}
						}
						else if (rFrom > rTo && cFrom > cTo) {
							for (int d = 0; d < Mathf.Abs(rFrom-rTo); d++) {
								if (!chessboard[rFrom-d,cFrom-d].Equals("  "))
									valid = false;
							}
						}
						else if (rFrom < rTo && cFrom > cTo) {
							for (int d = 0; d < Mathf.Abs(rFrom-rTo); d++) {
								if (!chessboard[rFrom+d,cFrom-d].Equals("  "))
									valid = false;
							}
						}
						if (valid)
							return true;
					}
				} else if (chessboard [rFrom, cFrom].Equals ("WK") || chessboard [rFrom, cFrom].Equals ("BK")) {
					// check if the destination results in a check
					if (chessboard[rTo,cTo].ToCharArray()[0]!= chessboard[rFrom,cFrom].ToCharArray()[0]){
						for (int r = 0; r < 8; r++){
							for (int c = 0 ; c < 8; c++){
								if (!chessboard[r,c].Equals("  ") && chessboard[r,c].ToCharArray()[0] != chessboard[rFrom,cFrom].ToCharArray()[0]){
									if( isValidMove(r,c,rTo,cTo)){
										return false;
									}
								}
							}
						}
					}
					// king adjacent movement
					else if (Mathf.Abs(rTo-rFrom)<1 && Mathf.Abs(cTo-cFrom)<1){
						return true;
					}
					// king castling
					else if (Mathf.Abs(cFrom- cTo)== 2){
						if (chessboard [rFrom, cFrom].StartsWith("W")&& !whiteChecked && !WKingMoved){
							if (cFrom < cTo && !WRRookMoved)
								return true;
							else if (cFrom > cTo && !WLRookMoved)
								return true;
						}
						else if (chessboard [rFrom, cFrom].StartsWith("B") && !blackChecked && !BKingMoved){
							if (cFrom < cTo && !BRRookMoved)
								return true;
							else if (cFrom > cTo && !BLRookMoved)
								return true;
						}
					}
				}
			}
			if (chessboard [rFrom, cFrom].Equals ("WQ") || chessboard [rFrom, cFrom].Equals ("BQ")) {
				if (Mathf.Abs (rFrom - rTo) == Mathf.Abs (cFrom - cTo)) {
					bool valid = true;
					if (rFrom < rTo && cFrom < cTo) {
						for (int d = 0; d < Mathf.Abs(rFrom-rTo); d++) {
							if (!chessboard[rFrom+d,cFrom+d].Equals("  "))
								valid = false;
						}
					}
					else if (rFrom > rTo && cFrom < cTo) {
						for (int d = 0; d < Mathf.Abs(rFrom-rTo); d++) {
							if (!chessboard[rFrom-d,cFrom+d].Equals("  "))
								valid = false;
						}
					}
					else if (rFrom > rTo && cFrom > cTo) {
						for (int d = 0; d < Mathf.Abs(rFrom-rTo); d++) {
							if (!chessboard[rFrom-d,cFrom-d].Equals("  "))
								valid = false;
						}
					}
					else if (rFrom < rTo && cFrom > cTo) {
						for (int d = 0; d < Mathf.Abs(rFrom-rTo); d++) {
							if (!chessboard[rFrom+d,cFrom-d].Equals("  "))
								valid = false;
						}
					}
					if (valid)
						return true;
				}
			} 
		}
		return false;
	}
	// checks if player (W or B) is checked
	public bool isChecked(string player){
		int rKing = 0;
		int cKing = 4;
		for (int r = 0; r < 8; r++) {
			for (int c = 0; c < 8; c++){
				if (chessboard[r,c].Equals(player+"K")){
					rKing = r;
					cKing = c;
					break;
				}
			}
		}

		for (int r = 0; r < 8; r++) {
			for (int c = 0 ; c < 8; c++){
				if (!chessboard[r,c].StartsWith(player)&& !chessboard[r,c].Equals("  ")){
					if (isValidMove(r,c,rKing,cKing))
						return true;
				}
			}
		}
		return false;
	}
	public bool isStalemate(string player){
		int rKing = 0;
		int cKing = 4;
		for (int r = 0; r < 8; r++) {
			for (int c = 0; c < 8; c++){
				if (chessboard[r,c].Equals(player+"K")){
					rKing = r;
					cKing = c;
					break;
				}
			}
		}
		if (isValidMove (rKing, cKing, rKing - 1, cKing - 1) || isValidMove (rKing, cKing, rKing, cKing - 1) || isValidMove (rKing, cKing, rKing + 1, cKing - 1)
			|| isValidMove (rKing, cKing, rKing - 1, cKing) || isValidMove (rKing, cKing, rKing, cKing + 1)
			|| isValidMove (rKing, cKing, rKing - 1, cKing + 1) || isValidMove (rKing, cKing, rKing, cKing + 1) || isValidMove (rKing, cKing, rKing + 1, cKing + 1)
			|| isValidMove (rKing, cKing, rKing, cKing - 2) || isValidMove (rKing, cKing, rKing, cKing + 2)
			)
			return false;
		return true;

	}
}
