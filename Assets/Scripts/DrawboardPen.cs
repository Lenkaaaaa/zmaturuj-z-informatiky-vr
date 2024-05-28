using System.Linq;
using UnityEngine;

public class DrawboardPen : MonoBehaviour
{
    [SerializeField] private Transform penTip;
    [SerializeField] private int penSize;

    private Renderer rend;
    private Color[] colors;
    private float penTipHeight;

    private RaycastHit touch;
    private Drawboard drawboard;
    private Vector2 touchPos, lastTouchPos;
    private bool touchedLastFrame;
    private Quaternion lastTouchRot;

    void Start()
    {
        rend = penTip.GetComponent<Renderer>();
        colors = Enumerable.Repeat(rend.material.color, penSize * penSize).ToArray();
        penTipHeight = penTip.localScale.y;
    }

    void Update()
    {
        Draw();
    }

    private void Draw()
    {
        if (Physics.Raycast(penTip.position, transform.up, out touch, penTipHeight))
        {
            if (touch.transform.CompareTag("Drawboard"))
            {
                if (drawboard == null)
                {
                    drawboard = touch.transform.GetComponent<Drawboard>();
                }

                touchPos = new Vector2(touch.textureCoord.x, touch.textureCoord.y);

                var x = (int)(touchPos.x * drawboard.textureSize.x - (penSize / 2));
                var y = (int)(touchPos.y * drawboard.textureSize.y - (penSize / 2));

                if (y < 0 || y > drawboard.textureSize.y || x < 0 || x > drawboard.textureSize.x) return;

                if (touchedLastFrame)
                {
                    drawboard.texture.SetPixels(x, y, penSize, penSize, colors);

                    for (float f = 0.01f; f < 1.00f; f += 0.01f)
                    {
                        var lerpX = (int)Mathf.Lerp(lastTouchPos.x, x, f);
                        var lerpY = (int)Mathf.Lerp(lastTouchPos.y, y, f);
                        drawboard.texture.SetPixels(lerpX, lerpY, penSize, penSize, colors);
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
