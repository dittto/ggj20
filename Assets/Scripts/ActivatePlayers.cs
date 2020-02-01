using System;
using UnityEngine;

public class ActivatePlayers : MonoBehaviour
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
			UIEventForwarder.OnForwardedEvent += ActivatePlayersEventHandler;
		}
	}

    void Update()
    {
    }

	private void ActivatePlayersEventHandler(UIEventArgsBase args)
	{
		if (args != null)
		{
			Type targetType = args.target;
			if (targetType == this.GetType())
			{
				player1.SetActive(true);
				player2.SetActive(true);
			}
		}
	}
}
