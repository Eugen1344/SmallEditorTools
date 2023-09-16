using UnityEditor;
using UnityEngine;

namespace TimeScaleSlider
{
    public class TimeScaleSliderWindow : EditorWindow
    {
        private float _timeScale;

        [MenuItem("Tools/Time Scale Slider")]
        private static void Open()
        {
            GetWindow<TimeScaleSliderWindow>("Time Scale");
        }

        private void OnEnable()
        {
            _timeScale = Time.timeScale;
        }

        private void OnGUI()
        {
            GUILayout.BeginHorizontal();
            {
                float newTimeScale = GUILayout.HorizontalSlider(_timeScale, 0, 1);

                if (newTimeScale != _timeScale)
                    UpdateTimeScale(newTimeScale);
            }
            GUILayout.EndHorizontal();
        }

        private void UpdateTimeScale(float newTimeScale)
        {
            Time.timeScale = newTimeScale;
            
            _timeScale = newTimeScale;
        }
    }
}