using System;
using UnityEngine;

public class DeactivatePlayers : MonoBehaviour
{
	[SerializeField]
	private GameObject player1;

	[SerializeField]
	private GameObject player2;

	[SerializeField]
	private GameObject eventUtilsPrefab;

	[SerializeField]
	private UIEventForwarder eventForwarder;

	void Start()
	{
		if (eventUtilsPrefab)
		{
			eventForwarder = eventUtilsPrefab.GetComponent<UIEventForwarder>();
		}

		if (eventForwarder)
		{
			UIEventForwarder.OnForwardedEvent += DeactivatePlayersEventHandler;
		}
	}

	void Update()
	{
	}

	private void DeactivatePlayersEventHandler(UIEventArgsBase args)
	{
		if (args != null)
		{
			Type targetType = args.target;
			if (targetType == this.GetType())
			{
				player1.SetActive(false);
				player2.SetActive(false);
			}
		}
	}
}
