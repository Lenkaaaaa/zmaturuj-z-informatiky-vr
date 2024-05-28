using UnityEngine;

public class Drawboard : MonoBehaviour
{
    public Texture2D texture;
    public Vector2 textureSize = new Vector2(2048, 2048);
    public Color originalColor;

    void Start()
    {
        var rend = GetComponent<Renderer>();
        texture = new Texture2D((int)textureSize.x, (int)textureSize.y);
        rend.material.mainTexture = texture;
        originalColor = rend.material.color;
    }
}
