﻿// Main Author - Christopher Bowles
//	Alterations by -
//
// Date last worked on 12/10/18

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikerCatchupState : BaseState
{
	private BikerAI m_BikerAI;
	private BikerMovement m_Movement;

	protected override void Setup()
	{
		// Check if ParentFSM isnt BikerAI
		if (m_ParentFSM.GetType() != typeof(BikerAI))
		{
			// Chastise Designers
			Debug.LogError(this.name + " should only be attached to a biker along with BikerAI. Make it so #1");
		}

		m_BikerAI = (BikerAI)m_ParentFSM;
		m_Movement = GetComponent<BikerMovement>();
	}

	public override void OnEnd()
	{

	}

	public override void UpdateState()
	{
		float fRange = Vector3.Magnitude(m_BikerAI.m_Player.transform.position - m_ParentObject.transform.position);
		// IF in range of player
		if (fRange < m_BikerAI.m_fChaseRange)
		{
			m_BikerAI.ChangeState("BikerChaseState");
			return;
		}

		m_Movement.GetPathToPlayer();
		m_Movement.MoveChase();
	}
}
