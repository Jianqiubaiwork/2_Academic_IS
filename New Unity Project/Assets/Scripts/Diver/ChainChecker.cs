﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainChecker : MonoBehaviour
{
	private int BOARD_SIZE;
	private int SHIFTING_OFFSET;
	private int Mask;
	private int sum;
	private int X;
	private int Y;
	private int[,] boardSituation;
	private bool isFiveInChain;

	public static ChainChecker chainCheckerInstance { set; get; }
		
	private void Start()
	{
		BOARD_SIZE = BoardManager.BOARD_SIZE;
		chainCheckerInstance = this;
	}

	private void Update()
	{
		X = BoardManager.boardManagerInstance.X;
		Y = BoardManager.boardManagerInstance.Y;
		boardSituation = BoardManager.boardManagerInstance.boardSituation;
	}

	public bool IsFiveInChain()
	{
		SHIFTING_OFFSET = 4;
		Mask = 31; // 11111
		if (CheckLeftToRightDiagonal ()) 
		{
			isFiveInChain = true;
		}
		if (CheckVertical ()) 
		{
			isFiveInChain = true;
		}
		if (CheckRightToLeftDiagonal ()) 
		{
			isFiveInChain = true;
		}
		if (CheckHorizontal ()) 
		{
			isFiveInChain = true;
		}
		return isFiveInChain;
	}

	private bool CheckLeftToRightDiagonal()
	{
		sum = 0;
		for (int i = -SHIFTING_OFFSET; i <= SHIFTING_OFFSET; i++) 
		{
			if (((X + i) >= 0) && ((Y - i) >= 0) && ((X + i) < BOARD_SIZE) && ((Y - i) < BOARD_SIZE))
			{
				if (boardSituation [X + i, Y - i] == boardSituation [X, Y]) 
				{
					sum = sum | (1 << (4 - i));
				}
			}
		}
		return CheckSum (sum);
	}

	private bool CheckVertical()
	{
		sum = 0;
		for (int i = -SHIFTING_OFFSET; i <= SHIFTING_OFFSET; i++) 
		{
			if (((Y + i) >= 0) && ((Y + i) < BOARD_SIZE))
			{
				if (boardSituation [X, Y + i] == boardSituation [X, Y]) 
				{
					sum = sum | (1 << (4 - i));
				}
			}
		}
		return CheckSum (sum);
	}

	private bool CheckRightToLeftDiagonal()
	{
		sum = 0;
		for (int i = -SHIFTING_OFFSET; i <= SHIFTING_OFFSET; i++) 
		{
			if (((X - i) >= 0) && ((Y - i) >= 0) && ((X - i) < BOARD_SIZE) && ((Y - i) < BOARD_SIZE)) 
			{
				if (boardSituation [X - i, Y - i] == boardSituation [X, Y]) 
				{
					sum = sum | (1 << (4 - i));
				}
			}
		}
		return CheckSum (sum);
	}

	private bool CheckHorizontal()
	{
		sum = 0;
		for (int i = -SHIFTING_OFFSET; i <= SHIFTING_OFFSET; i++) 
		{
			if (((X + i) >= 0) && ((X + i) < BOARD_SIZE)) 
			{
				if (boardSituation [X + i, Y] == boardSituation [X, Y]) 
				{
					sum = sum | (1 << (4 - i));
				}
			}
		}
		return CheckSum (sum);
	}

	private bool CheckSum(int sum)
	{
		int shiftedSum = 0;
		for (int i = 0; i <= SHIFTING_OFFSET; i++) 
		{
			shiftedSum = sum >> i;
			if ((shiftedSum & Mask) == Mask) {
				return true;
			}
		}
		return false;
	}
}