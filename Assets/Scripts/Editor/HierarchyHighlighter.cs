using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[InitializeOnLoad]
public class HierarchyHighlighter
{

    static HierarchyHighlighter()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItem_CB;
    }

    private static void HierarchyWindowItem_CB(int selectionID, Rect selectionRect)
    {
        Object o = EditorUtility.InstanceIDToObject(selectionID);
        if (o != null)
        {
            if ((o as GameObject).GetComponent<HierarchyHighlighterComponent>() != null)
            {
                HierarchyHighlighterComponent h = (o as GameObject).GetComponent<HierarchyHighlighterComponent>();
                if (h.highlight)
                {
                    GUIStyle style = new GUIStyle();
                    style.normal.textColor = h.color;
                    style.alignment = TextAnchor.UpperRight;
                    if (Event.current.type == EventType.Repaint)
                    {
                        style.Draw(selectionRect, new GUIContent("LE  "), selectionID);
                        EditorApplication.RepaintHierarchyWindow();
                    }
                }
            }
        }
    }
}
