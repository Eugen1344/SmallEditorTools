﻿using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SmallEditorTools.FavouriteList
{
    public class MainWindow : EditorWindow, IHasCustomMenu
    {
        [NonSerialized] public ItemsManager ItemsManager;

        private MainWindowList _list;
        private EditorPrefsSaveManager _saveManager;

        [MenuItem("Tools/Favourite List")]
        private static void ShowWindow()
        {
            MainWindow mainWindow = GetWindow<MainWindow>("Favourite List");
            mainWindow.Show();
            mainWindow.Focus();
        }

        private void OnHierarchyChange()
        {
            ItemsManager?.TryCacheAllObjects();
        }

        private void OnFocus()
        {
            ItemsManager?.TryCacheAllObjects();
        }

        private void OnEnable()
        {
            _saveManager = new EditorPrefsSaveManager();

            ItemsManager = _saveManager.Load() ?? new ItemsManager();
            ItemsManager.TryCacheAllObjects();
            ItemsManager.OnChanged += Save;

            _list = new MainWindowList(ItemsManager);
        }

        private void OnDisable()
        {
            ItemsManager.OnChanged -= Save;
        }

        public void Save()
        {
            _saveManager.Save(ItemsManager);
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginVertical();
            {
                _list.Render();

                EditorGUILayout.Space(1);
                RenderNewField();
                EditorGUILayout.Space(1);
            }

            EditorGUILayout.EndVertical();
        }

        private void RenderNewField()
        {
            Color prevColor = GUI.color;
            GUI.color = Color.cyan;

            EditorGUILayout.BeginHorizontal();
            {
                Object favouritedObject = EditorGUILayout.ObjectField(null, typeof(Object), true);

                if (favouritedObject)
                {
                    ItemsManager.Add(favouritedObject);
                }
            }
            EditorGUILayout.EndHorizontal();

            GUI.color = prevColor;
        }

        public void AddItemsToMenu(GenericMenu menu)
        {
            GUIContent content = new GUIContent("Clear");
            menu.AddItem(content, false, ClearList);
        }

        private void ClearList()
        {
            ItemsManager.Clear();
        }
    }
}