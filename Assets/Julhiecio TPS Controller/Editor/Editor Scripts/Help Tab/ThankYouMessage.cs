using UnityEngine;
using UnityEditor;

namespace JUTPS.CustomEditors
{
    [InitializeOnLoad]
    public class StartThankyouMessage
    {
        static StartThankyouMessage()
        {
            if (EditorPrefs.HasKey("JUTPS_NeverShowThankYouMessage"))
            {
                if (EditorPrefs.GetBool("JUTPS_NeverShowThankYouMessage") == true) { return; }
            }

            EditorApplication.quitting += OnAppQuit;
            if (EditorPrefs.HasKey("JUTPS_ThankYouMessageWasShown"))
            {
                if (EditorPrefs.GetBool("JUTPS_NeverShowThankYouMessage") == false && EditorPrefs.GetBool("JUTPS_ThankYouMessageWasShown") == false)
                {
                    ThankYouWindow.ShowWindow();
                    EditorPrefs.SetBool("JUTPS_ThankYouMessageWasShown", true);
                }
            }
            else if (EditorPrefs.GetBool("JUTPS_ThankYouMessageWasShown") == false)
            {
                ThankYouWindow.ShowWindow();
                EditorPrefs.SetBool("JUTPS_ThankYouMessageWasShown", true);
            }

        }
        public static void OnAppQuit()
        {
            EditorPrefs.SetBool("JUTPS_ThankYouMessageWasShown", false);
        }
    }

    public class ThankYouWindow : EditorWindow
    {
        [MenuItem("Window/JU TPS/Start Window")]
        public static void ShowWindow()
        {
            GetWindow(typeof(ThankYouWindow));
            GetWindow(typeof(ThankYouWindow)).titleContent.text = "Thanks!";
            const int width = 398;
            const int height = 250;

            var x = (Screen.currentResolution.width - width) / 2;
            var y = (Screen.currentResolution.height - height) / 2;

            GetWindow<ThankYouWindow>().position = new Rect(x, y, width, height);
        }

        private static Texture2D Banner, DocThumb, TutoThumb, SupportThumb, YbThumb;
        public static bool NeverShowThankYouMessage;

        //[MenuItem("Window/JU TPS/Thank you!/clear")]
        public static void ClearThankMessageYouEditorPrefsKey()
        {
            EditorPrefs.DeleteKey("JUTPS_ThankYouMessageWasShown");

            EditorPrefs.DeleteKey("JUTPS_ThankYouMessageWasShown");

            Debug.Log("Cleaned ThankMessage Editor Prefs Key");
        }
        private void OnGUI()
        {
            if (Banner == null || DocThumb == null || SupportThumb == null || TutoThumb == null || YbThumb == null)
            {
                Banner = JUTPSEditor.CustomEditorUtilities.GetImage("JUTPSLOGO");
                DocThumb = JUTPSEditor.CustomEditorUtilities.GetImage("Thumb_Doc");
                SupportThumb = JUTPSEditor.CustomEditorUtilities.GetImage("Thumb_Support");
                TutoThumb = JUTPSEditor.CustomEditorUtilities.GetImage("Thumb_Tutorial");
                YbThumb = JUTPSEditor.CustomEditorUtilities.GetImage("Thumb_Youtube");
                if (Banner == null || DocThumb == null || SupportThumb == null || TutoThumb == null || YbThumb == null)
                {
                    GUILayout.Label("Unable to find the resources images, please, if you deleted it, also delete the ThankYouMessage script in the folder /Editor/Editor Scripts/Help Tab/ThankYouMessage.cs");
                    return;
                }
            }
            if (Banner != null)
            {
                GUILayout.BeginHorizontal();

                JUTPSEditor.CustomEditorUtilities.RenderImageWithResize(Banner, new Vector2(64, 40));
                GUILayout.Label("Thanks!", JUTPSEditor.CustomEditorStyles.Title(16), GUILayout.Height(30));

                GUILayout.EndHorizontal();
            }

            var style = new GUIStyle(EditorStyles.label);
            style.font = JUTPSEditor.CustomEditorStyles.JUTPSEditorFont();
            style.fontSize = 12;
            style.wordWrap = true;

            GUILayout.Label(" Thanks for buying my asset, you don't know how much is helping me!" +
                "\r\n I hope you enjoy my work", style);

            GUILayout.Space(15);

            GUILayout.Label("GET STARTED", JUTPSEditor.CustomEditorStyles.Toolbar());

            GUILayout.BeginHorizontal();
            /*if (GUILayout.Button(TutoThumb, GUILayout.Height(128), GUILayout.Width(256)))
            {
                HelpTabOptions.OpenTutorialPlaylists();
            }*/
            if (GUILayout.Button(DocThumb, GUILayout.Height(64), GUILayout.Width(128)))
            {
                JUTPS.CustomEditors.HelpTabOptions.OpenDocumentation();
            }
            //GUILayout.EndHorizontal();

            //GUILayout.BeginHorizontal();
            if (GUILayout.Button(SupportThumb, GUILayout.Height(64), GUILayout.Width(128)))
            {
                JUTPS.CustomEditors.HelpTabOptions.OpenSupportEmail();
            }
            if (GUILayout.Button(YbThumb, GUILayout.Height(64), GUILayout.Width(128)))
            {
                Application.OpenURL("https://www.youtube.com/c/JulhiecioGameDev");
            }
            GUILayout.EndHorizontal();
            if (GUILayout.Button("Open Discord Community", JUTPSEditor.CustomEditorStyles.MiniToolbar(), GUILayout.Height(30)))
            {
                JUTPS.CustomEditors.HelpTabOptions.OpenCommunity();
            }

            GUILayout.Space(20);

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            NeverShowThankYouMessage = GUILayout.Toggle(NeverShowThankYouMessage, " Never show this window again");
            if (GUILayout.Button("Exit", GUILayout.Width(60)))
            {
                EditorPrefs.SetBool("JUTPS_NeverShowThankYouMessage", NeverShowThankYouMessage);
                GetWindow<ThankYouWindow>().Close();
            }
            GUILayout.EndHorizontal();

        }
    }
}