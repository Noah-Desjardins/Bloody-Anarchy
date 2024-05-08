using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCursor : MonoBehaviour
{
    float xspot;
    float yspot;
    public void setCursor(Texture2D texture)
    {
        xspot = texture.width / 2;
        yspot = texture.height / 2;
        Cursor.SetCursor(texture, new Vector2(xspot,yspot), CursorMode.ForceSoftware);
    }
}
