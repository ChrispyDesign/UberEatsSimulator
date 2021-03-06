﻿// Main Author - Christopher Bowles
//	Alterations by -
//
// Date last worked on 29/11/18

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour {

	public bool m_bKillMe = false;
	public bool m_bAllowCheckStuck = false;

    public AudioSource m_SeppukuSound;
	public GameObject[] m_DeathParticlePrefabs;

	public List<DeathScript> m_Related = new List<DeathScript>();
	public List<GameObject> m_RelatedObjects = new List<GameObject>();

	public float m_fCheckStuckRange;
	public float m_fCheckStuckTime;

	private float m_fStuckTime = float.PositiveInfinity;

	private Vector3 m_OldPos = Vector3.zero;

	private float m_fKillAfter = float.PositiveInfinity;
	
	
	// Update is called once per frame
	void Update ()
	{
        m_SeppukuSound.playOnAwake = false;
		// IF time has passed kill after time
		if (Time.time > m_fKillAfter)
		{
			// it should die
			m_bKillMe = true;
		}

		if (m_bAllowCheckStuck)
		{
			CheckStuck();
		}

		// IF it should die
		if (m_bKillMe)
		{
			// Commit Seppuku
			Kill();
		}
	}

	//--------------------------------------------------------------
	//	CheckStuck
	//		Checks if stuck in place, Sets killMe to true if stuck
	//--------------------------------------------------------------
	public void CheckStuck()
	{
		float fRange = Vector3.Magnitude(transform.position - m_OldPos);

		if (fRange < m_fCheckStuckRange)
		{
			m_bKillMe = Time.time > m_fStuckTime;
		}
		else
		{
			m_OldPos = transform.position;
			m_fStuckTime = Time.time + m_fCheckStuckTime;
		}

		if (m_bKillMe)
		{
			Debug.LogWarning("Killing Stuck " + gameObject.name, gameObject);
		}
	}

	//--------------------------------------------------------------
	//	KillAfter
	//		Sets kill after time
	//
	//	var
	//		float time
	//			time in seconds until death
	//--------------------------------------------------------------
	public void KillAfter(float fSeconds)
	{
		m_fKillAfter = Time.time + fSeconds;
	}

	//--------------------------------------------------------------
	//	Kill
	//		Kills the owner and their family
	//--------------------------------------------------------------
	void Kill()
	{
		// Kill their family
		foreach (DeathScript deadMan in m_Related)
		{
			deadMan.m_bKillMe = true;
		}

		// Kill their inlaws
		foreach (GameObject deadMan in m_RelatedObjects)
		{
			Destroy(deadMan);
		}

		// Create Particles if able
		if (m_DeathParticlePrefabs != null)
		{
            m_SeppukuSound.Play();
			DeathParticles();
		}

		// Then Kill them
		Destroy(gameObject);
	}

	void DeathParticles()
	{
		// Create particles at the position
		foreach (GameObject prefab in m_DeathParticlePrefabs)
		{
			Instantiate(prefab, transform.position, prefab.transform.rotation);
		}
	}
}
