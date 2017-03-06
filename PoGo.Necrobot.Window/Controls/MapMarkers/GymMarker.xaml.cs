﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using GMap.NET.WindowsPresentation;
using System.Diagnostics;
using PoGo.Necrobot.Window.Model;
using System.Threading.Tasks;
using PoGo.NecroBot.Logic.Tasks;
using PoGo.NecroBot.Logic.State;
using POGOProtos.Map.Fort;
using System;

namespace PoGo.Necrobot.Window.Controls.MapMarkers
{
    /// <summary>
    /// Interaction logic for CustomMarkerDemo.xaml
    /// </summary>
    public partial class GymMarker
    {
        GMapMarker Marker;
        MainClientWindow MainWindow;
        private GymViewModel fort;

        public bool IsMarkerOf(string gymId)
        {
            return false;
        }
        public GymMarker(MainClientWindow window, GMapMarker marker)
        {
            this.InitializeComponent();

            this.MainWindow = window;
            this.Marker = marker;

            this.Unloaded += new RoutedEventHandler(CustomMarkerDemo_Unloaded);
            this.Loaded += new RoutedEventHandler(CustomMarkerDemo_Loaded);
            this.SizeChanged += new SizeChangedEventHandler(CustomMarkerDemo_SizeChanged);
            this.MouseEnter += new MouseEventHandler(MarkerControl_MouseEnter);
            this.MouseLeave += new MouseEventHandler(MarkerControl_MouseLeave);
            this.MouseMove += new MouseEventHandler(CustomMarkerDemo_MouseMove);
            this.MouseLeftButtonUp += new MouseButtonEventHandler(CustomMarkerDemo_MouseLeftButtonUp);
            this.MouseLeftButtonDown += new MouseButtonEventHandler(CustomMarkerDemo_MouseLeftButtonDown);
        }

        public GymMarker(MainClientWindow window, GMapMarker marker, FortData item) : this(window, marker)
        {
            this.fort = new GymViewModel(item);
            this.DataContext = this.fort;
        }

        void CustomMarkerDemo_Loaded(object sender, RoutedEventArgs e)
        {
            if (icon.Source.CanFreeze)
            {
                icon.Source.Freeze();
            }
        }

        void CustomMarkerDemo_Unloaded(object sender, RoutedEventArgs e)
        {
            //this.Unloaded -= new RoutedEventHandler(CustomMarkerDemo_Unloaded);
            //this.Loaded -= new RoutedEventHandler(CustomMarkerDemo_Loaded);
            //this.SizeChanged -= new SizeChangedEventHandler(CustomMarkerDemo_SizeChanged);
            //this.MouseEnter -= new MouseEventHandler(MarkerControl_MouseEnter);
            //this.MouseLeave -= new MouseEventHandler(MarkerControl_MouseLeave);
            //this.MouseMove -= new MouseEventHandler(CustomMarkerDemo_MouseMove);
            //this.MouseLeftButtonUp -= new MouseButtonEventHandler(CustomMarkerDemo_MouseLeftButtonUp);
            //this.MouseLeftButtonDown -= new MouseButtonEventHandler(CustomMarkerDemo_MouseLeftButtonDown);

            //Marker.Shape = null;
            //icon.Source = null;
            //icon = null;
        }

        void CustomMarkerDemo_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Marker.Offset = new Point(-e.NewSize.Width / 2, -e.NewSize.Height);
        }

        void CustomMarkerDemo_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && IsMouseCaptured)
            {
                //Point p = e.GetPosition(MainWindow.MainMap);
                // Marker.Position = MainWindow.MainMap.FromLocalToLatLng((int) (p.X), (int) (p.Y));
            }
        }

        void CustomMarkerDemo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsMouseCaptured)
            {
                Mouse.Capture(this);
            }
            //Popup.IsOpen = true;
        }

        void CustomMarkerDemo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (IsMouseCaptured)
            {
                Mouse.Capture(null);
            }
        }

        void MarkerControl_MouseLeave(object sender, MouseEventArgs e)
        {
            Marker.ZIndex -= 10000;
            //Popup.IsOpen = false;
        }

        void MarkerControl_MouseEnter(object sender, MouseEventArgs e)
        {
            Marker.ZIndex += 10000;
            //Popup.IsOpen = true;
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(null);
        }

        private void icon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            popInfo.IsOpen = true;

        }

        private void UserControl_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void btnWalkHere_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(async () =>
            {
                await SetMoveToTargetTask.Execute(fort.Latitude, fort.Longitude, fort.FortId);
            });
            this.Dispatcher.Invoke(() =>
            {
                popInfo.IsOpen = false;
            });
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                popInfo.IsOpen = false;
            });
        }

        internal void UpdateDistance(double lat, double lng)
        {
            fort.UpdateDistance(lat, lng);
        }

    }
}