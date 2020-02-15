using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Panel : MonoBehaviour
{
    private bool gameEnded = false;
	private AudioSource _source;

	[SerializeField]
	private GameObject EndGameRef;

	[SerializeField]
	private GameObject DisablePlayersObject;


	[SerializeField]
	private EndGame EndGameObject;

	[SerializeField]
	private List<AudioClip> turningOnClips;

	[SerializeField]
	private List<AudioClip> excitementClips;

	[SerializeField]
	private int healthToTurnOn;

	[SerializeField]
	private int currentHealth;

	// Sprite
	[SerializeField]
	private Sprite endGameSprite;

	// Event stuff
	[SerializeField]
	private GameObject eventUtilsPrefab;

	[SerializeField]
	private UIEventForwarder eventForwarder;


	private void Start()
	{
		_source = GetComponent<AudioSource>();

		currentHealth = 0;

		if (eventUtilsPrefab)
		{
			eventForwarder = eventUtilsPrefab.GetComponent<UIEventForwarder>();
		}

		if(EndGameRef)
		{
			EndGameObject = EndGameRef.GetComponent<EndGame>();
		}
	}

	private void Update()
	{
		if(currentHealth >= healthToTurnOn && !gameEnded)
		{
			// Change sprite
			if ( endGameSprite )
			{
				GetComponentInParent<SpriteRenderer>().sprite = endGameSprite;
			}

            EndGameObject.StartEnd();
            gameEnded = true;

            // End game
            //	- Deactivate players
            //	- Play UIAnimation
            //	- Music?
            if (eventForwarder != null)
			{
				//eventForwarder.EnqueueEvent(new UIEventArgsBase(typeof(EndGame), this.GetType()));
				//eventForwarder.EnqueueEvent(new UIEventArgsBase(typeof(DeactivatePlayers), this.GetType()));
			}
		}
	}

	//public void OnCollisionEnter(Collision other)
	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Tool"))
		{
			if (turningOnClips.Count > 0)
			{
				System.Random rng = new System.Random();
				int clipIndex = rng.Next(0, turningOnClips.Count - 1);
				_source.PlayOneShot(turningOnClips[clipIndex]);
			}

			currentHealth++;
            Destroy(other.gameObject);
		}
	}
}
