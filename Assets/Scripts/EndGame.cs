using System;
using System.Collections;
using UnityEngine;

public class EndGame : MonoBehaviour
{
	[SerializeField]
	private GameObject EndGameCanvas;
 
	[SerializeField]
	private GameObject eventUtilsPrefab;

	[SerializeField]
	private UIEventForwarder eventForwarder;

	// Note:
	// Literally so we just get the exact length of the animation;
	// There is another way of getting a specific clip from the animator
	// but it requires iterating over all member clips. 
	// Pending me finding some solution; Perhaps I eventually integrate this in the base class :D
	[SerializeField]
	private AnimationClip clip;

	[SerializeField]
	private Animator animator;

	void Start()
	{
		if (eventUtilsPrefab)
		{
			eventForwarder = eventUtilsPrefab.GetComponent<UIEventForwarder>();
		}

		if (eventForwarder)
		{
			UIEventForwarder.OnForwardedEvent += EndGameEventHandler;
		}
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }


    private void EndGameEventHandler(UIEventArgsBase args)
	{
		if (args != null)
		{
			Type targetType = args.target;
			if (targetType == this.GetType())
			{
				EndGameCanvas.SetActive(true);
			}
		}
	}

	public void StartEnd()
	{
		EndGameCanvas.SetActive(true);
        FindObjectOfType<AudioManager>().Play("Outro");
    }
}
