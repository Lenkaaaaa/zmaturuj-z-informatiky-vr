using System.Linq;
using UnityEngine;

public class DrawboardEraser : MonoBehaviour
{
    [SerializeField] private Transform tip;
    [SerializeField] private int eraserSize;

    private Renderer r;
    private Color[] eraserColors;
    private float tipHeight;

    private RaycastHit touch;
    private Drawboard drawboard;
    private Vector2 touchPos, lastTouchPos;
    private bool touchedLastFrame;
    private Quaternion lastTouchRot;

    void Start()
    {
        r = tip.GetComponent<Renderer>();
        eraserColors = Enumerable.Repeat(Color.white, eraserSize * eraserSize).ToArray(); 
        tipHeight = tip.localScale.y;
    }

    void Update()
    {
        Erase();
    }

    private void Erase()
    {
        if (Physics.Raycast(tip.position, transform.up, out touch, tipHeight))
        {
            if (touch.transform.CompareTag("Drawboard"))
            {
                if (drawboard == null)
                {
                    drawboard = touch.transform.GetComponent<Drawboard>();
                }

                touchPos = new Vector2(touch.textureCoord.x, touch.textureCoord.y);

                var x = (int)(touchPos.x * drawboard.textureSize.x - (eraserSize / 2));
                var y = (int)(touchPos.y * drawboard.textureSize.y - (eraserSize / 2));

                if (y < 0 || y > drawboard.textureSize.y || x < 0 || x > drawboard.textureSize.x) return;

                if (touchedLastFrame)
                {
                    for (int i = 0; i < eraserColors.Length; i++)
                    {
                        eraserColors[i] = drawboard.originalColor;
                    }

                    drawboard.texture.SetPixels(x, y, eraserSize, eraserSize, eraserColors);

                    for (float f = 0.01f; f < 1.00f; f += 0.01f)
                    {
                        var lerpX = (int)Mathf.Lerp(lastTouchPos.x, x, f);
                        var lerpY = (int)Mathf.Lerp(lastTouchPos.y, y, f);
                        drawboard.texture.SetPixels(lerpX, lerpY, eraserSize, eraserSize, eraserColors);
                    }

                    transform.rotation = lastTouchRot;

                    drawboard.texture.Apply();
                }

                lastTouchPos = new Vector2(x, y);
                lastTouchRot = transform.rotation;
                touchedLastFrame = true;
                return;
            }
        }

        drawboard = null;
        touchedLastFrame = false;
    }
}
