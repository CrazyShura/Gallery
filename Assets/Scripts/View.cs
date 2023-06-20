using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

/// <summary>
/// Manages image for a view screen.Call LoadImageForView to load properly.
/// Singleton
/// </summary>
public class View : MonoBehaviour
{
	#region Fields
	public static View Instance;

	[SerializeField]
	RawImage image;
	#endregion

	#region Properties

	#endregion

	#region Methods
	private void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}
		Instance = this;
	}
	private void FixedUpdate()
	{
		if(Screen.orientation == ScreenOrientation.Portrait)
		{
			image.rectTransform.sizeDelta = new Vector2(800, 800);
		}
		else
		{
			image.rectTransform.sizeDelta = new Vector2(450, 450);
		}
	}
	public async Task LoadImageForView(string url)
	{
		using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url))
		{
			UnityWebRequestAsyncOperation operation = webRequest.SendWebRequest();
			while (!operation.isDone)
			{
				await Task.Yield();
			}

			if (webRequest.result == UnityWebRequest.Result.Success)
			{
				DownloadHandlerTexture downloader = webRequest.downloadHandler as DownloadHandlerTexture;
				image.texture = downloader.texture;
			}
			else
			{
				Debug.Log(webRequest.error);
			}
		}
	}
	#endregion
}

