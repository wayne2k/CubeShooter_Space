﻿using UnityEngine;
using System.Collections;

namespace RollRoti.CubeShooter_Space
{
	[RequireComponent (typeof (HealthController))]
	public class EnemyController : MonoBehaviour 
	{
		public int scoreToGive = 1;
		public int damageToGive = 1;
		public bool destroyOnDeath = false;
		public GameObject explosionVFX;
		public GameObject damageVFX;

		HealthController _health;
		public bool _handledDeath = false;

		void Awake ()
		{
			_health = GetComponent <HealthController> ();
		}

		void Update ()
		{
			if (_handledDeath == false && _health.IsDead) 
			{
				_handledDeath = true;
				OnNoHealth ();
			}
		}

		void OnTriggerEnter(Collider other)
		{
//			if (other.tag == "Player")
			{
				HealthController player = other.GetComponentInParent <HealthController> ();
	//			Debug.Log ("Player is null: " + player == null);

				if (player != null) 
				{
	//				Debug.Log ("Damage to give: " + damageToGive);
				
					player.TakeDamage (damageToGive);			
				
					DeathVFX ();
					Destroy (gameObject);
	//				Debug.Log (gameObject.name + " Destroy on Contact with " + other.gameObject.name);
				}
			}
		}

		void OnNoHealth ()
		{
			GiveScore ();
			DeathVFX ();
		
			Destroy (gameObject);
		}

		void GiveScore ()
		{
			if (ScoreManager.Instance != null)
				ScoreManager.Instance.score += scoreToGive;
		}

		void DeathVFX ()
		{
			if (explosionVFX != null)
			{
				GameObject go = Instantiate (explosionVFX, transform.position, transform.rotation) as GameObject;
				go.transform.parent = (GameManager.Instance != null) ? GameManager.Instance.ExplosionsHolder_T : null;
			}
		}
		
		void DamageVFX ()
		{
			if (damageVFX != null)
			{
				GameObject go = Instantiate (damageVFX, transform.position, transform.rotation) as GameObject;
				go.transform.parent = (GameManager.Instance != null) ? GameManager.Instance.ExplosionsHolder_T : null;
			}
		}

		void TookDamage ()
		{
			DamageVFX ();
		}
	}
}