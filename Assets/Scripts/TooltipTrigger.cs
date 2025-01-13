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
        // ���� �������� ���ٸ� �������� ����
        if (tooltipPrefab == null)
        {
            CreateTooltipPrefab();
        }
    }

    private void CreateTooltipPrefab()
    {
        // ���� UI ���� ����
        tooltipPrefab = new GameObject("TooltipPrefab");
        tooltipPrefab.SetActive(false);

        // ��� �̹���
        var image = tooltipPrefab.AddComponent<UnityEngine.UI.Image>();
        image.color = new Color(0.1f, 0.1f, 0.1f, 0.9f);

        // �ؽ�Ʈ ������Ʈ
        var textObj = new GameObject("Text");
        textObj.transform.SetParent(tooltipPrefab.transform);
        var text = textObj.AddComponent<TextMeshProUGUI>();
        text.color = Color.white;
        text.fontSize = 12;
        text.alignment = TextAlignmentOptions.Center;

        // ���̾ƿ� ����
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
        // ���콺�� �ö��� �� ���� ǥ��
        if (currentTooltip == null)
        {
            currentTooltip = Instantiate(tooltipPrefab, transform.parent);
            currentTooltip.SetActive(true);

            var text = currentTooltip.GetComponentInChildren<TextMeshProUGUI>();
            text.text = tooltipText;

            // ���콺 ��ġ�� ���� ��ġ
            currentTooltip.transform.position = eventData.position + new Vector2(10, 10);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // ���콺�� ����� ���� ����
        if (currentTooltip != null)
        {
            Destroy(currentTooltip);
            currentTooltip = null;
        }
    }
}