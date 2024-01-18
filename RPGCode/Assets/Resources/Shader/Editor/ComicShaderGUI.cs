using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ComicShaderGUI : ShaderGUI
{
    // Setting
    MaterialProperty VRChatMode;
    MaterialProperty ShadowPriority;

    // Main
    MaterialProperty MainTex;
    MaterialProperty Color;
    // Decal
    MaterialProperty UseDecal;
    MaterialProperty DecalColorPriority;
    MaterialProperty DecalTex;
    MaterialProperty DecalMask;
    MaterialProperty DecalMaskPower;
    // DecalEdge
    MaterialProperty UseDecalEdge;
    MaterialProperty DecalEdgeColor;
    MaterialProperty DecalEdgePower;
    MaterialProperty DecalEdgeRange;
    MaterialProperty DecalEdgeBlur;
    // DecalEmission
    MaterialProperty UseDecalEmission;
    MaterialProperty DecalEmissionHDRColor;
    MaterialProperty DecalEmissionPower;
    // DecalFlicker
    MaterialProperty DecalFlicker;
    MaterialProperty DecalFlickerSpeed;

    // Light
    MaterialProperty DirectionalLightDir;
    MaterialProperty DirectionalLightPower;
    MaterialProperty PointLightPower;

    // Shading
    MaterialProperty ShadowMask;
    MaterialProperty ShadowPower;

    // Line
    MaterialProperty UseLine;
    MaterialProperty LineAmount;
    MaterialProperty LineAngle;

    // Toon
    MaterialProperty Cuts;
    MaterialProperty CutsWidth;

    // Normal
    MaterialProperty BumpMap;
    MaterialProperty BumpScale;

    // Outline
    MaterialProperty UseOutline;
    MaterialProperty OutlineMaskTex;
    MaterialProperty OutlineColor;
    MaterialProperty OutlineWidth;

    // RimLight
    MaterialProperty UseRimLight;
    MaterialProperty RimMask;
    MaterialProperty RimColor;
    MaterialProperty RimPower;

    // Specular
    MaterialProperty UseSpecular;
    MaterialProperty SpecularMask;
    MaterialProperty SpecularColor;
    MaterialProperty SpecularPower;
    MaterialProperty Smoothness;

    // Metallic
    MaterialProperty UseMetallic;
    MaterialProperty MetallicMask;
    MaterialProperty Metallic;

    // MatCap
    MaterialProperty UseMatCap;
    MaterialProperty MatCapTex;
    MaterialProperty MatCapColor;
    MaterialProperty MatCapMask;
    MaterialProperty MatCapPower;

    // Emission
    MaterialProperty UseEmission;
    MaterialProperty EmissionTex;
    MaterialProperty EmissionHDRColor;
    MaterialProperty EmissionMask;
    MaterialProperty EmissionPower;
    // Flicker
    MaterialProperty Flicker;
    MaterialProperty FlickerSpeed;
    MaterialProperty UseEmissionAnimation;
    MaterialProperty EmissionAnimationSpeed;
    MaterialProperty EmissionScrollX;
    MaterialProperty EmissionScrollY;

    // OtherSettings
    MaterialProperty Culling;
    MaterialProperty Cutoff;

    private bool _decalSettingOpen = false;
    private bool _otherSettingsOpen = false;

    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] Prop) 
    {
        // Setting
        VRChatMode = FindProperty("_VRChatMode", Prop, false);
        ShadowPriority = FindProperty("_ShadowPriority", Prop, false);

        // Main
        MainTex = FindProperty("_MainTex", Prop, false);
        Color = FindProperty("_Color", Prop, false);
        // Decal
        UseDecal = FindProperty("_UseDecal", Prop, false);
        DecalColorPriority = FindProperty("_DecalColorPriority", Prop, false);
        DecalTex = FindProperty("_DecalTex", Prop, false);
        DecalMask = FindProperty("_DecalMask", Prop, false);
        DecalMaskPower = FindProperty("_DecalMaskPower", Prop, false);
        // DecalEdge
        UseDecalEdge = FindProperty("_UseDecalEdge", Prop, false);
        DecalEdgeColor = FindProperty("_DecalEdgeColor", Prop, false);
        DecalEdgePower = FindProperty("_DecalEdgePower", Prop, false);
        DecalEdgeRange = FindProperty("_DecalEdgeRange", Prop, false);
        DecalEdgeBlur = FindProperty("_DecalEdgeBlur", Prop, false);
        // DecalEmission
        UseDecalEmission = FindProperty("_UseDecalEmission", Prop, false);
        DecalEmissionHDRColor = FindProperty("_DecalEmissionHDRColor", Prop, false);
        DecalEmissionPower = FindProperty("_DecalEmissionPower", Prop, false);
        // DecalFlicker
        DecalFlicker = FindProperty("_DecalFlicker", Prop, false);
        DecalFlickerSpeed = FindProperty("_DecalFlickerSpeed", Prop, false);
        
        // Light
        DirectionalLightDir = FindProperty("_DirectionalLightDir", Prop, false);
        DirectionalLightPower = FindProperty("_DirectionalLightPower", Prop, false);
        PointLightPower = FindProperty("_PointLightPower", Prop, false);
        
        // Shading
        ShadowMask = FindProperty("_ShadowMask", Prop, false);
        ShadowPower = FindProperty("_ShadowPower", Prop, false);
        
        // Line
        UseLine = FindProperty("_UseLine", Prop, false);
        LineAmount = FindProperty("_LineAmount", Prop, false);
        LineAngle = FindProperty("_LineAngle", Prop, false);
        
        // Toon
        Cuts = FindProperty("_Cuts", Prop, false);
        CutsWidth = FindProperty("_CutsWidth", Prop, false);
        
        // Normal
        BumpMap = FindProperty("_BumpMap", Prop, false);
        BumpScale = FindProperty("_BumpScale", Prop, false);

        // Outline
        UseOutline = FindProperty("_UseOutline", Prop, false);
        OutlineMaskTex = FindProperty("_OutlineMaskTex", Prop, false);
        OutlineColor = FindProperty("_OutlineColor", Prop, false);
        OutlineWidth = FindProperty("_OutlineWidth", Prop, false);
        
        // RimLight
        UseRimLight = FindProperty("_UseRimLight", Prop, false);
        RimMask = FindProperty("_RimMask", Prop, false);
        RimColor = FindProperty("_RimColor", Prop, false);
        RimPower = FindProperty("_RimPower", Prop, false);

        // Specular
        UseSpecular = FindProperty("_UseSpecular", Prop, false);
        SpecularMask = FindProperty("_SpecularMask", Prop, false);
        SpecularColor = FindProperty("_SpecularColor", Prop, false);
        SpecularPower = FindProperty("_SpecularPower", Prop, false);
        Smoothness = FindProperty("_Smoothness", Prop, false);
        
        // Metallic
        UseMetallic = FindProperty("_UseMetallic", Prop, false);
        MetallicMask = FindProperty("_MetallicMask", Prop, false);
        Metallic = FindProperty("_Metallic", Prop, false);

        // MatCap
        UseMatCap = FindProperty("_UseMatCap", Prop, false);
        MatCapTex = FindProperty("_MatCapTex", Prop, false);
        MatCapColor = FindProperty("_MatCapColor", Prop, false);
        MatCapMask = FindProperty("_MatCapMask", Prop, false);
        MatCapPower = FindProperty("_MatCapPower", Prop, false);

        // Emission
        UseEmission = FindProperty("_UseEmission", Prop, false);
        EmissionTex = FindProperty("_EmissionTex", Prop, false);
        EmissionHDRColor = FindProperty("_EmissionHDRColor", Prop, false);
        EmissionMask = FindProperty("_EmissionMask", Prop, false);
        EmissionPower = FindProperty("_EmissionPower", Prop, false);
        // Flicker
        Flicker = FindProperty("_Flicker", Prop, false);
        FlickerSpeed = FindProperty("_FlickerSpeed", Prop, false);
        UseEmissionAnimation = FindProperty("_UseEmissionAnimation", Prop, false);
        EmissionAnimationSpeed = FindProperty("_EmissionAnimationSpeed", Prop, false);
        EmissionScrollX = FindProperty("_EmissionScrollX", Prop, false);
        EmissionScrollY = FindProperty("_EmissionScrollY", Prop, false);
        
        // OtherSettings
        Culling = FindProperty("_Culling", Prop, false);
        Cutoff = FindProperty("_Cutoff", Prop, false);

        // ================================================================================================
            // DrawGUI
            // base.OnGUI (materialEditor, Prop);
        // ================================================================================================
        CustomUI.Header("Information");
        using (new EditorGUILayout.VerticalScope("box"))
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.Label("Version");
                GUILayout.Label("Version 1.0.0");
            }

            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.Label("How to use (Japanese)");
                if(GUILayout.Button("How to use (Japanese)"))
                {
                    System.Diagnostics.Process.Start("**");
                }
            }
        }

        // ================================================================================================
        GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
        // ================================================================================================

        CustomUI.Header("Main");
        // MainTex
        using(new EditorGUILayout.VerticalScope("box"))
        {
            materialEditor.TexturePropertySingleLine(new GUIContent("Main Texture"), MainTex, Color);
            CustomUI.TextureScaleOffset(materialEditor, MainTex);
        }

        // NormalMap
        using(new EditorGUILayout.VerticalScope("box"))
        {
            var rect = GUILayoutUtility.GetRect(16.0f, 22.0f, GUIStyle.none);
            Texture bumpMap;
            bumpMap = materialEditor.TexturePropertyMiniThumbnail(new Rect(rect.x, rect.y + 2.0f, rect.width - 20.0f, rect.height), BumpMap, "NoramlMap", "");
            
            if(bumpMap != null)
            {
                CustomUI.TextureScaleOffset(materialEditor, BumpMap);
                materialEditor.ShaderProperty(BumpScale, "BumpScale");
            }
        }

        // Decal
        using(new EditorGUILayout.VerticalScope("box"))
        {
            materialEditor.ShaderProperty(UseDecal, "UseDecal");
            if(UseDecal.floatValue > 0.0f)
            {
                EditorGUI.indentLevel ++;
                // DecalTex
                var rect = GUILayoutUtility.GetRect(16.0f, 22.0f, GUIStyle.none);
                materialEditor.TexturePropertyMiniThumbnail(new Rect(rect.x, rect.y + 2.0f, rect.width - 20.0f, rect.height), DecalTex, "Decal Texture", "");
                // DecalMask
                rect = GUILayoutUtility.GetRect(16.0f, 22.0f, GUIStyle.none);
                materialEditor.TexturePropertyMiniThumbnail(new Rect(rect.x, rect.y + 2.0f, rect.width - 20.0f, rect.height), DecalMask, "Decal Mask Texture", "");
                CustomUI.TextureScaleOffset(materialEditor, DecalMask);

                materialEditor.ShaderProperty(DecalMaskPower, "Decal Mask Power");

                // UseDecalEdge
                materialEditor.ShaderProperty(UseDecalEdge, "Use Decal Edge");
                if(UseDecalEdge.floatValue > 0.0f)
                {
                    EditorGUI.indentLevel ++;
                    materialEditor.ShaderProperty(DecalEdgeColor, "DecalEdge Color");
                    materialEditor.ShaderProperty(DecalEdgePower, "DecalEdge Power");
                    materialEditor.ShaderProperty(DecalEdgeRange, "DecalEdge Range");
                    materialEditor.ShaderProperty(DecalEdgeBlur, "DecalEdge Blur");
                    EditorGUI.indentLevel --;
                }

                // UseDecalEmission
                materialEditor.ShaderProperty(UseDecalEmission, "Use DecalEmission");
                if(UseDecalEmission.floatValue > 0.0f)
                {
                    EditorGUI.indentLevel ++;
                    materialEditor.ShaderProperty(DecalEmissionHDRColor, "DecalEmissionHDRColor");
                    materialEditor.ShaderProperty(DecalEmissionPower, "DecalEmission Power");
                    materialEditor.ShaderProperty(DecalFlicker, "Decal Flicker");
                    if(DecalFlicker.floatValue > 0.0)
                    {
                        materialEditor.ShaderProperty(DecalFlickerSpeed, "Decal Flicker Speed");
                    }
                    EditorGUI.indentLevel --;
                }

                // DecalSetting
                bool decalSettingOpen = EditorGUILayout.Foldout(_decalSettingOpen, "DecalSetting");
                if(_decalSettingOpen != decalSettingOpen)
                {
                    _decalSettingOpen = decalSettingOpen;
                }

                if(decalSettingOpen == true)
                {
                    materialEditor.ShaderProperty(DecalColorPriority, "Decal Color Priority");
                }
                EditorGUI.indentLevel --;
            }
        }
        // ================================================================================================
        GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
        // ================================================================================================
    
        CustomUI.Header("Shading");
        using(new EditorGUILayout.VerticalScope("box"))
        {
            var rect = GUILayoutUtility.GetRect(16.0f, 22.0f, GUIStyle.none);
            materialEditor.TexturePropertyMiniThumbnail(new Rect(rect.x, rect.y + 2.0f, rect.width - 20.0f, rect.height), ShadowMask, "Shadow Mask", "");
            materialEditor.ShaderProperty(ShadowPower, "Shadow Power");
        }

        // ================================================================================================
        GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
        // ================================================================================================
        
        CustomUI.Header("Line");  
        using(new EditorGUILayout.VerticalScope("box"))
        {
            materialEditor.ShaderProperty(UseLine, "Use Line");
            if(UseLine.floatValue > 0.0f)
            {
                EditorGUI.indentLevel ++;
                materialEditor.ShaderProperty(LineAmount, "Line Amount");
                materialEditor.ShaderProperty(LineAngle, "Line Angle");
                EditorGUI.indentLevel --;
            }
        }  

        // ================================================================================================
        GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
        // ================================================================================================
    
        CustomUI.Header("Toon");
        using(new EditorGUILayout.VerticalScope("box"))
        {
            materialEditor.ShaderProperty(Cuts, "Toon Cuts");
            materialEditor.ShaderProperty(CutsWidth, "Cut Width");
        }

        // ================================================================================================
        GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
        // ================================================================================================

        CustomUI.Header("Outline");
        using(new EditorGUILayout.VerticalScope("box"))
        {
            materialEditor.ShaderProperty(UseOutline, "Use Outline");
            if(UseOutline.floatValue > 0.0f)
            {
                EditorGUI.indentLevel ++;
                var rect = GUILayoutUtility.GetRect(16.0f, 22.0f, GUIStyle.none);
                materialEditor.TexturePropertyMiniThumbnail(new Rect(rect.x, rect.y + 2.0f, rect.width - 20.0f, rect.height), OutlineMaskTex, "OutLine Mask Texture", "");
                materialEditor.ShaderProperty(OutlineColor, "Outline Color");
                materialEditor.ShaderProperty(OutlineWidth, "Outline Width");
                EditorGUI.indentLevel --;
            }
        }

        // ================================================================================================
        GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
        // ================================================================================================
    
        CustomUI.Header("RimLight");
        using(new EditorGUILayout.VerticalScope("box"))
        {
            materialEditor.ShaderProperty(UseRimLight, "Use RimLight");
            if(UseRimLight.floatValue > 0.0)
            {
                EditorGUI.indentLevel ++;
                var rect = GUILayoutUtility.GetRect(16.0f, 22.0f, GUIStyle.none);
                materialEditor.TexturePropertyMiniThumbnail(new Rect(rect.x, rect.y + 2.0f, rect.width - 20.0f, rect.height), RimMask, "RimLight Mask Texture", "");
                materialEditor.ShaderProperty(RimColor, "RimLight Color");
                materialEditor.ShaderProperty(RimPower, "RimLight Power");
                EditorGUI.indentLevel --;
            }
        }

        // ================================================================================================
        GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
        // ================================================================================================
    
        CustomUI.Header("Specular");
        using(new EditorGUILayout.VerticalScope("box"))
        {
            materialEditor.ShaderProperty(UseSpecular, "Use Specular");
            if(UseSpecular.floatValue > 0.0)
            {
                EditorGUI.indentLevel ++;
                var rect = GUILayoutUtility.GetRect(16.0f, 22.0f, GUIStyle.none);
                materialEditor.TexturePropertyMiniThumbnail(new Rect(rect.x, rect.y + 2.0f, rect.width - 20.0f, rect.height), SpecularMask, "Specular Mask Texture", "");
                materialEditor.ShaderProperty(SpecularColor, "Specular Color");
                materialEditor.ShaderProperty(SpecularPower, "Specular Power");
                materialEditor.ShaderProperty(Smoothness, "Smoothness");
                EditorGUI.indentLevel --;
            }
        }

        // ================================================================================================
        GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
        // ================================================================================================

        CustomUI.Header("Metallic");
        using(new EditorGUILayout.VerticalScope("box"))
        {
            materialEditor.ShaderProperty(UseMetallic, "Use Metallic");
            if(UseMetallic.floatValue > 0.0)
            {
                EditorGUI.indentLevel ++;
                var rect = GUILayoutUtility.GetRect(16.0f, 22.0f, GUIStyle.none);
                materialEditor.TexturePropertyMiniThumbnail(new Rect(rect.x, rect.y + 2.0f, rect.width - 20.0f, rect.height), MetallicMask, "Metallic Mask Texture", "");
                materialEditor.ShaderProperty(Metallic, "Metallic");
                EditorGUI.indentLevel --;
            }
        }

        // ================================================================================================
        GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
        // ================================================================================================

        CustomUI.Header("MatCap");
        using(new EditorGUILayout.VerticalScope("box"))
        {
            materialEditor.ShaderProperty(UseMatCap, "Use MatCap");
            if(UseMatCap.floatValue > 0.0)
            {
                EditorGUI.indentLevel ++;
                var rect = GUILayoutUtility.GetRect(16.0f, 22.0f, GUIStyle.none);
                materialEditor.TexturePropertyMiniThumbnail(new Rect(rect.x, rect.y + 2.0f, rect.width - 20.0f, rect.height), MatCapTex, "MatCap Texture", "");

                rect = GUILayoutUtility.GetRect(16.0f, 22.0f, GUIStyle.none);
                materialEditor.TexturePropertyMiniThumbnail(new Rect(rect.x, rect.y + 2.0f, rect.width - 20.0f, rect.height), MatCapMask, "MatCap Mask Texture", "");
                materialEditor.ShaderProperty(MatCapColor, "MatCap Color");
                materialEditor.ShaderProperty(MatCapPower, "MatCap Power");
                EditorGUI.indentLevel --;
            }
        }

        // ================================================================================================
        GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
        // ================================================================================================

        CustomUI.Header("Emission");
        using(new EditorGUILayout.VerticalScope("box"))
        {
            materialEditor.ShaderProperty(UseEmission, "Use Emission");
            if(UseEmission.floatValue > 0.0)
            {
                EditorGUI.indentLevel ++;
                materialEditor.TexturePropertySingleLine(new GUIContent("Emission Texture"), EmissionTex, EmissionHDRColor);
                CustomUI.TextureScaleOffset(materialEditor, EmissionTex);

                var rect = GUILayoutUtility.GetRect(16.0f, 22.0f, GUIStyle.none);
                materialEditor.TexturePropertyMiniThumbnail(new Rect(rect.x, rect.y + 2.0f, rect.width - 20.0f, rect.height), EmissionMask, "Emission Mask Texture", "");
                materialEditor.ShaderProperty(EmissionPower, "Emission Power");
                materialEditor.ShaderProperty(Flicker, "Emission Flicker");
                if(Flicker.floatValue > 0.0f)
                {
                    materialEditor.ShaderProperty(FlickerSpeed, "Flicker Speed");
                }
                materialEditor.ShaderProperty(UseEmissionAnimation, "Use EmissionAnimation");
                if(UseEmissionAnimation.floatValue > 0.0f)
                {
                    EditorGUI.indentLevel ++;
                    materialEditor.ShaderProperty(EmissionAnimationSpeed, "EmissionAnimation Speed");
                    materialEditor.ShaderProperty(EmissionScrollX, "Emission ScrollX");
                    materialEditor.ShaderProperty(EmissionScrollY, "Emission ScrollY");
                    EditorGUI.indentLevel --;
                }
                EditorGUI.indentLevel --;
            }
        }

        // ================================================================================================
        GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
        // ================================================================================================

        CustomUI.Header("OtherSettings");
        using(new EditorGUILayout.VerticalScope("box"))
        {
            EditorGUI.indentLevel ++;
            bool otherSettingsOpen = EditorGUILayout.Foldout(_otherSettingsOpen, "OtherSettings");
            if(_otherSettingsOpen != otherSettingsOpen)
            {
                _otherSettingsOpen = otherSettingsOpen;
            }

            if(otherSettingsOpen == true)
            {
                GUILayout.Label("VRChat");
                materialEditor.ShaderProperty(VRChatMode, "VRChatMode");

                GUILayout.Label("Shadow");
                materialEditor.ShaderProperty(ShadowPriority, "Shadow Priority");

                GUILayout.Label("LightSetting");
                materialEditor.ShaderProperty(DirectionalLightDir, "DirectionalLight Dir");
                materialEditor.ShaderProperty(DirectionalLightPower, "Directional Light Power");
                materialEditor.ShaderProperty(PointLightPower, "Point Light Power");

                if(Cutoff != null)
                {
                    GUILayout.Label("Cutoff");
                    materialEditor.ShaderProperty(Cutoff, "Cutoff");
                }

                GUILayout.Label("Culling");
                materialEditor.ShaderProperty(Culling, "Culling");
            }
            EditorGUI.indentLevel --;
        }
        
    }
}

static class CustomUI
{
    private static Rect ShurikenUI(string title)
    {
        var style = new GUIStyle("ShurikenModuleTitle");
        style.margin = new RectOffset(0, 0, 8, 0);
        style.font = new GUIStyle(EditorStyles.boldLabel).font;
        style.border = new RectOffset(15, 7, 4, 4);
        style.fixedHeight = 22;
        style.contentOffset = new Vector2(20f, -2f);
        var rect = GUILayoutUtility.GetRect(16f, 22f, style);
        GUI.Box(rect, title, style);

        return rect;
    }

    public static void Header(string title)
    {
        ShurikenUI(title);
    }

    public static void TextureScaleOffset(MaterialEditor editor, MaterialProperty prop)
    {
        EditorGUI.indentLevel ++;
        editor.TextureScaleOffsetProperty(prop);
        EditorGUI.indentLevel --;
    }
}
