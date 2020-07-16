﻿using AAPTForNet;
using QuickLook.Common.Plugin;
using System.Windows;

namespace QuickLook.Plugin.ApkViewer {
    public class Plugin : IViewer {
        public int Priority => 0;

        public void Init() => AAPTool.Init();

        public bool CanHandle(string path) => path.ToLower().EndsWith(".apk");

        public void Prepare(string path, ContextObject context) {
            context.Theme = Themes.Dark;
            context.PreferredSize = new Size { Width = 650, Height = 200 };
            context.Title = path;
            context.TitlebarOverlap = false;
            context.TitlebarBlurVisibility = false;
            context.TitlebarColourVisibility = false;
            context.FullWindowDragging = true;
        }

        public void View(string path, ContextObject context) {
            var apk = AAPTool.Decompile(path);
            if (apk.isEmpty) {
                context.ViewerContent = new System.Windows.Controls.Label() {
                    Content = "Can't not load package. Please change the file name and try again.",
                    Foreground = System.Windows.Media.Brushes.White,
                    FontSize = 16,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                };
            }
            else {
                context.ViewerContent = new ViewerPane(context) {
                    ApkInfo = apk
                };
            }

            context.IsBusy = false;
        }

        public void Cleanup() { }
    }
}