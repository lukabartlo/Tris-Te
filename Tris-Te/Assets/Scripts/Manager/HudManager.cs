using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class HudManager : MonoBehaviour
{
    [SerializeField] private UIDocument uiDoc;
    private VisualElement root;
    private Label scoreLabel;
    private Image previewImage;

    private void Start()
    {
        root = uiDoc.rootVisualElement;
        scoreLabel = root.Q<Label>("score");
        previewImage = root.Q<Image>("preview");
    }

    public void UpdateScoreDisplay(int scoreToDisplay)
    {
        scoreLabel.text = scoreToDisplay.ToString();
    }

    public void UpdatePreviewImage(BlockGroup block)
    {
        previewImage.sprite = block.blockSprite;
    }
}
