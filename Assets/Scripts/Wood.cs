using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Wood : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] TextMeshProUGUI score;
	[SerializeField] GameObject winText;
	[SerializeField] GameObject flyingLog;
	[SerializeField] Animator flyingLogAnimation;
	public double currentScore;
	static string[] format = { "", "K", "M" };
	private float randomScore;
	private Vector3 logPos;
	void Start()
	{
		randomScore = Random.Range(1000, 10000);
		logPos = flyingLog.transform.position;
	}
	public void OnPointerClick(PointerEventData eventData)
	{
		Invoke("AddScore", .3f);
		InstantiateLog();
		Win();
	}
	private void InstantiateLog()
	{
		var log = Instantiate(flyingLog, logPos, Quaternion.identity);
		Destroy(log, .35f);
		flyingLogAnimation.Play("fly");
	}

	private void Win()
	{
		if (currentScore >= 10000000)
		{
			winText.SetActive(true);
			Time.timeScale = 0f;
		}
	}
	public void AddScore()
	{
		currentScore += (int)randomScore;
		score.SetText(ChangeFormat(currentScore));
	}
	private string ChangeFormat(double scores)
	{
		scores = Mathf.Round((float)scores);

		int i = 0;
		while (i + 1 < format.Length && scores >= 1000)
		{
			scores /= 1000;
			i++;
		}
		return scores.ToString("#.##") + format[i];
	}
}
