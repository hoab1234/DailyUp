// TooltipTrigger.cs
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector]
    public string tooltipText;

    private static GameObject tooltipPrefab;
    private GameObject currentTooltip;

    private void Start()
    {
        // 툴팁 프리팹이 없다면 동적으로 생성
        if (tooltipPrefab == null)
        {
            CreateTooltipPrefab();
        }
    }

    private void CreateTooltipPrefab()
    {
        // 툴팁 UI 동적 생성
        tooltipPrefab = new GameObject("TooltipPrefab");
        tooltipPrefab.SetActive(false);

        // 배경 이미지
        var image = tooltipPrefab.AddComponent<UnityEngine.UI.Image>();
        image.color = new Color(0.1f, 0.1f, 0.1f, 0.9f);

        // 텍스트 컴포넌트
        var textObj = new GameObject("Text");
        textObj.transform.SetParent(tooltipPrefab.transform);
        var text = textObj.AddComponent<TextMeshProUGUI>();
        text.color = Color.white;
        text.fontSize = 12;
        text.alignment = TextAlignmentOptions.Center;

        // 레이아웃 설정
        var rect = tooltipPrefab.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(120, 30);

        var textRect = textObj.GetComponent<RectTransform>();
        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.sizeDelta = Vector2.zero;
        textRect.offsetMin = new Vector2(5, 5);
        textRect.offsetMax = new Vector2(-5, -5);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // 마우스가 올라갔을 때 툴팁 표시
        if (currentTooltip == null)
        {
            currentTooltip = Instantiate(tooltipPrefab, transform.parent);
            currentTooltip.SetActive(true);

            var text = currentTooltip.GetComponentInChildren<TextMeshProUGUI>();
            text.text = tooltipText;

            // 마우스 위치에 툴팁 배치
            currentTooltip.transform.position = eventData.position + new Vector2(10, 10);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 마우스가 벗어나면 툴팁 제거
        if (currentTooltip != null)
        {
            Destroy(currentTooltip);
            currentTooltip = null;
        }
    }
}