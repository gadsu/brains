using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour 
{
	PathTo m_pathTo;
	DetectPlayer m_DetectPlayer;

	Transform m_target;

	private void Awake()
	{
		m_pathTo = GetComponent<PathTo>();
		m_DetectPlayer = GetComponent<DetectPlayer>();
	}

	private void Start()
	{
		m_target = GameObject.Find("Player").GetComponent<Transform>();
	}

	private void Update()
	{
		if(m_DetectPlayer.IsInView(m_target.position))
		{
			if(m_DetectPlayer.IsVisible())
			{
				m_pathTo.SetVisible(true);
			}
		}
    }
}
