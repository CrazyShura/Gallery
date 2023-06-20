using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Gallery loader. Call StartUp to initiate.
/// Singleton, not destroyed on load.
/// </summary>
public class Gallery : MonoBehaviour, IEndDragHandler
{
	#region Fields
	public static Gallery Instance;

	[SerializeField]
	RectTransform imageButton;
	[SerializeField]
	RectTransform galleryScroll;

	int currentImageNumber = 1;
	bool generatingMore = true;
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
	}
	private void Start()
	{
		EventManager.NoMoreImagesToLoad.AddListener(NoMoreLoading);
	}
	void NoMoreLoading()
	{
		generatingMore = false;
	}
	public async Task StartUp()
	{
		List<Task> _tasks = new List<Task>();
		for (int _i = 1; _i < 9; _i++)
		{
			GameObject _temp = Instantiate(imageButton, transform.GetChild(0)).gameObject;
			_temp.name = "Image " + currentImageNumber;
			_tasks.Add(_temp.GetComponent<ImageLoader>().LoadImage(currentImageNumber));
			currentImageNumber++;
		}
		await Task.WhenAll(_tasks);
		for (int _i = 1; _i < 9; _i++)
		{
			GameObject _temp = Instantiate(imageButton, transform.GetChild(0)).gameObject;
			_temp.name = "Image " + currentImageNumber;
			_tasks.Add(_temp.GetComponent<ImageLoader>().LoadImage(currentImageNumber));
			currentImageNumber++;
		}
	}
	public void OnEndDrag(PointerEventData eventData)
	{
		if (generatingMore && galleryScroll.rect.height - galleryScroll.localPosition.y < 1800)
		{
			for (int _i = 1; _i < 9; _i++)
			{
				GameObject _temp = Instantiate(imageButton, transform.GetChild(0)).gameObject;
				_temp.name = "Image " + currentImageNumber;
				_temp.GetComponent<ImageLoader>().LoadImage(currentImageNumber);
				currentImageNumber++;
			}
		}
	}
	#endregion
}

