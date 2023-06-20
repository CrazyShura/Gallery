using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Manger for scene loading, transition and etc...
/// Singleton , Not Destroyed On Load.
/// </summary>
public class TransitionManager : MonoBehaviour
{
	#region Fields

	public static TransitionManager Instance;


	[SerializeField]
	Animator animator;
	[SerializeField]
	TMP_Text loadingText;
	[SerializeField]
	Slider loadBar;
	Controls inputs;
	int currentScene = 0;
	bool inTransition = false;
	#endregion

	#region Properties

	#endregion

	#region Methods
	private void Awake()
	{
		if (Instance != null)
		{
			Destroy(transform.parent.gameObject);
			return;
		}
		Instance = this;
		DontDestroyOnLoad(transform.parent);
		inputs = new Controls();
		inputs.AppControls.Enable();
		inputs.AppControls.Back.performed += context => HandleInput();
	}
	void HandleInput()
	{
		Debug.Log("boop");
		switch (currentScene)
		{
			case 0:
				{
					Application.Quit();
					break;
				}
			case 1:
				{
					TransitionToMenue();
					break;
				}
			case 2:
				{
					TransitionToGallery();
					break;
				}
			default:
				break;
		}
	}
	public async void TransitionToMenue()
	{
		if (!inTransition && currentScene != 0)
		{
			inTransition = true;
			loadingText.gameObject.SetActive(true);
			loadBar.gameObject.SetActive(true);
			animator.SetTrigger("StartTransition");
			await Task.Delay(1000);

			AsyncOperation loading = SceneManager.LoadSceneAsync(0);
			while (!loading.isDone)
			{
				loadBar.value = loading.progress / .9f;
				await Task.Yield();
			}
			animator.SetTrigger("EndTransition");
			currentScene = 0;
			inTransition = false;
			await Task.Delay(1000);
			loadBar.value = 0;
			loadingText.gameObject.SetActive(false);
			loadBar.gameObject.SetActive(false);
		}
		Screen.orientation = ScreenOrientation.Portrait;
	}
	public async void TransitionToGallery()
	{
		if (!inTransition && currentScene != 1)
		{
			inTransition = true;
			loadingText.gameObject.SetActive(true);
			loadBar.gameObject.SetActive(true);
			animator.SetTrigger("StartTransition");

			await Task.Delay(1000);

			AsyncOperation loading = SceneManager.LoadSceneAsync(1);
			while (!loading.isDone)
			{
				loadBar.value = loading.progress / 1f;
				await Task.Yield();
			}

			while (Gallery.Instance == null)
			{
				await Task.Yield();
			}
			await Gallery.Instance.StartUp();
			loadBar.value = 1f;
			animator.SetTrigger("EndTransition");
			currentScene = 1;
			inTransition = false;

			await Task.Delay(1000);

			loadBar.value = 0;
			loadingText.gameObject.SetActive(false);
			loadBar.gameObject.SetActive(false);
		}
		Screen.orientation = ScreenOrientation.Portrait;
	}
	public async void TransitionToView(string url)
	{
		if (!inTransition && currentScene != 2)
		{
			inTransition = true;
			loadingText.gameObject.SetActive(true);
			loadBar.gameObject.SetActive(true);
			animator.SetTrigger("StartTransition");

			await Task.Delay(1000);

			AsyncOperation loading = SceneManager.LoadSceneAsync(2);
			while (!loading.isDone)
			{
				loadBar.value = loading.progress / 1f;
				await Task.Yield();
			}

			while (View.Instance == null)
			{
				await Task.Yield();
			}
			await View.Instance.LoadImageForView(url);
			loadBar.value = 1f;
			animator.SetTrigger("EndTransition");
			currentScene = 2;
			inTransition = false;

			await Task.Delay(1000);

			loadBar.value = 0;
			loadingText.gameObject.SetActive(false);
			loadBar.gameObject.SetActive(false);
		}
		Screen.orientation = ScreenOrientation.AutoRotation;
	}
	#endregion
}

