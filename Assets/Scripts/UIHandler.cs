using UnityEngine;

public class UIHandler : MonoBehaviour
{
	#region Fields

	#endregion

	#region Properties

	#endregion

	#region Methods
	public void TransitionToMenue()
	{
		TransitionManager.Instance.TransitionToMenue();
	}
	public void TransitionToGallery()
	{
		TransitionManager.Instance.TransitionToGallery();
	}
	#endregion
}

