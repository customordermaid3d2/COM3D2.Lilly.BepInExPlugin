using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour
{
    GUIEx.DropdownState state1_ = new GUIEx.DropdownState();
    GUIEx.DropdownState state2_ = new GUIEx.DropdownState();
    GUIEx.DropdownState state3_ = new GUIEx.DropdownState();

    private void OnGUI()
    {
        state1_ = GUIEx.Dropdown(new Rect(30, 30, 100, 30), new[] { "A", "B", "C" }, state1_);

        var styles = new GUIEx.DropdownStyles("button", "box", Color.white, 24, 8);
        state2_ = GUIEx.Dropdown(new Rect(150, 30, 150, 30), new[] { "X", "Y", "Z", "W" }, state2_, styles);

        state3_ = GUIEx.Dropdown(
            new Rect(10, Screen.height - 50, Screen.width - 20, 40), 
            new[] {
                "1920 x 1080 (Recommend)",
                "1600 x 1024",
                "1280 x 960",
                "1280 x 800", }, 
            state3_);
    }


    private float scrollPos;
    private int displayCount;
    private Rect optionRect;
    private Rect buttonRect;
    private float height;
    private float steps;

    /*
    void test()
    {

        GUILayout.BeginHorizontal();
        {
            //Items
            GUILayout.BeginVertical();
            {
                for (int j = (int)scrollPos; j < ((int)scrollPos + displayCount); j++)
                {
                    if (GUILayout.Button(options[j]))
                    {
                        val = options[j];
                        open = !open;

                        //Changing the value can change with width of the main button, so redraw all the dropdown windows
                        foreach (KeyValuePair<int, IMGUIHelper> kvp in IMGUIHelper.widgets)
                        {
                            if (kvp.Value.GetType() == typeof(GUIDropDown))
                            {
                                ((GUIDropDown)kvp.Value).buttonRect = new Rect();
                            }
                        }
                    }

                    //Update select item height calculations
                    if (Event.current.type == EventType.Repaint && optionRect.width == 0 && optionRect.height == 0)
                    {
                        Rect lastRect = GUILayoutUtility.GetLastRect();
                        optionRect = new Rect(lastRect.x, lastRect.y, lastRect.width, lastRect.height);
                        buttonRect = new Rect(buttonRect.x, buttonRect.y, buttonRect.width, lastRect.height);
                    }
                }
            }
            GUILayout.EndVertical();

            //Scrollbar
            //int top = (options.Length / displayCount + ((options.Length % displayCount != 0) ? 1 : 0));
            //top = (options.Length - displayCount) + ((((options.Length - displayCount) % displayCount) != 0) ? 1 : 0);
            scrollPos = GUILayout.VerticalScrollbar(scrollPos, 1f, 0f, steps, GUILayout.Height(height));
        }
        GUILayout.EndHorizontal();

        //Scroll event
        if (Event.current.isScrollWheel)
        {
            //Up
            if (Event.current.delta.y < 0)
            {
                scrollPos = System.Math.Max(0, scrollPos - 1);
            }
            //Down
            else if (Event.current.delta.y > 0)
            {
                scrollPos = System.Math.Min(steps - 1, scrollPos + 1);
            }
        }
    }
    */
}
