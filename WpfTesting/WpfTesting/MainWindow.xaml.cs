﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfTesting
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //DrawLine();
            //DrawRectangle();

            //legend vals
            //TODO - box size should be dependant on text size
            double legendBoxSize = 20;
            double legendTextOffset = legendBoxSize / 10;
            double legendTextFontSize = legendBoxSize * 0.66;
            double legendMargin = 100;
            //TODO origin for legend and origin for room diagram, base on total legend offset

            //test grid for rooms
            double width = 30;

            double originX = 10;
            double originY = 10;

            double horizontalOffset = originX;
            double verticalOffset = originY;
            double roomSpacing = 5;
            double floorSpacing = 5;

            RoomModel room1 = new RoomModel
            {
                Id = 1,
                RoomName = "Entry hall",
                RoomArea = 1000,
                RoomFloor = 0,
                RoomColor = Color.FromRgb(155, 55, 55)
            };
            RoomModel room2 = new RoomModel
            {
                Id = 2,
                RoomName = "Kitchen",
                RoomArea = 2000,
                RoomFloor = 0,
                RoomColor = Color.FromRgb(200, 100, 0)
            };
            RoomModel room3 = new RoomModel
            {
                Id = 3,
                RoomName = "Bedroom",
                RoomArea = 3000,
                RoomFloor = 1,
                RoomColor = Color.FromRgb(50, 50, 200)
            };
            RoomModel room4 = new RoomModel
            {
                Id = 4,
                RoomName = "Garage",
                RoomArea = 5000,
                RoomFloor = 0,
                RoomColor = Color.FromRgb(150, 50, 200)
            };

            List<RoomModel> house = new List<RoomModel> { room1, room2, room3, room4 };

            int currentFloor = house.OrderBy(x => x.RoomFloor).First().RoomFloor;
            List<int> floors = house.OrderBy(x => x.RoomFloor).Select(x => x.RoomFloor).Distinct().ToList();

            foreach (var f in floors)
            {
                currentFloor = f;

                foreach (var r in house.Where(x => x.RoomFloor == currentFloor))
                {
                    DrawRoom(r, width, horizontalOffset, verticalOffset);
                    verticalOffset += roomSpacing + r.RoomArea / width;
                }

                verticalOffset = originY;
                horizontalOffset += roomSpacing + width;
            }

            //draw legend on the right
            horizontalOffset += legendMargin;

            foreach (var r in house)
            {
                DrawLegend(r, legendTextFontSize, legendBoxSize, legendTextOffset, horizontalOffset, verticalOffset);
                verticalOffset += legendBoxSize + roomSpacing;
            }


            //copy-paste code to force measure of canvas
            //save to image finally works
            //TODO - if window size is smaller than image, it gets cropped - even if set to resizeable
            //canvas size - whether set or not, or even if set larger than window size, is irrelevant
            //the second line is necessary to save not blank, but apparently saves the window size no matter what is third and fourth argument - it can be even new Rect(0,0,0,0)

            //was also redundant, because canvas.Measure() and canvas.Arrange was included in SaveDialogue() method chain, but Arrange() was commented out, so result was blank...

            //myCanvas.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            //myCanvas.Arrange(new Rect(0, 0, myCanvas.DesiredSize.Width, myCanvas.DesiredSize.Height));

            //this is from same stack article, version for explicit window size, works same here
            //myCanvas.Measure(new Size(Width, Height));
            //myCanvas.Arrange(new Rect(0, 0, myCanvas.DesiredSize.Width, myCanvas.DesiredSize.Height));



            //TODO - draw legend on the left + toggle for left/right legend placement

            //save to png as is
            SaveDialogue(this, myCanvas);

            //TODO - make png not shit

        }

        public void DrawLegend(RoomModel model, double legendTextFontSize, double legendBoxSize, double legendTextOffset, double horizontalOffset, double verticalOffset)
        {
            //legend box
            var legendBox = new Rectangle();
            SolidColorBrush legendColor = new SolidColorBrush { Color = model.RoomColor };
            legendBox.Fill = legendColor;
            legendBox.Width = legendBoxSize;
            legendBox.Height = legendBoxSize;
            legendBox.Stroke = Brushes.Black;
            legendBox.StrokeThickness = 1;
            Canvas.SetLeft(legendBox, horizontalOffset);
            Canvas.SetTop(legendBox, verticalOffset);
            myCanvas.Children.Add(legendBox);

            //legend text
            TextBlock legendText = new TextBlock();
            legendText.Text = model.RoomName;
            legendText.FontSize = legendTextFontSize;
            Canvas.SetLeft(legendText, horizontalOffset + legendBoxSize + legendTextOffset);
            Canvas.SetTop(legendText, verticalOffset);
            myCanvas.Children.Add(legendText);

        }

        public void DrawRoom(RoomModel model, double width, double offsetX, double offsetY)
        {
            var room = new Rectangle();
            SolidColorBrush roomColor = new SolidColorBrush
            {
                Color = model.RoomColor
            };
            room.Fill = roomColor;
            room.Stroke = Brushes.Black;
            room.StrokeThickness = 1;
            room.Width = width;
            room.Height = model.RoomArea / width;
            Canvas.SetLeft(room, offsetX);
            Canvas.SetTop(room, offsetY);
            myCanvas.Children.Add(room);
        }


        //copy-paste code - save canvas to png, take 3...
        //saves empty canvas, but at least is saves AN image without throwing errors
        public static void SaveDialogue(Window window, Canvas canvas)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Image"; // Default file name
            dlg.DefaultExt = ".png"; // Default file extension
            dlg.Filter = "PNG File (.png)|*.png"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;
                SaveCanvasToFile(window, canvas, 96, filename);
            }
        }

        public static void SaveCanvasToFile(Window window, Canvas canvas, int dpi, string filename)
        {
            Size size = new Size(window.Width, window.Height);
            canvas.Measure(size);
            canvas.Arrange(new Rect(size));

            var rtb = new RenderTargetBitmap(
                //TODO - custom size would go here, so would need to pass totalX, totalY as parameters and put them here
                (int)window.Width, //width
                (int)window.Height, //height
                dpi, //dpi x
                dpi, //dpi y
                PixelFormats.Pbgra32 // pixelformat
                );
            rtb.Render(canvas);

            SaveRTBAsPNGBMP(rtb, filename);
        }

        public static void SaveWindowToFile(Window window, int dpi, string filename)
        {
            var rtb = new RenderTargetBitmap(
                (int)window.Width, //width
                (int)window.Width, //height
                dpi, //dpi x
                dpi, //dpi y
                PixelFormats.Pbgra32 // pixelformat
                );
            rtb.Render(window);

            SaveRTBAsPNGBMP(rtb, filename);
        }

        private static void SaveRTBAsPNGBMP(RenderTargetBitmap bmp, string filename)
        {
            var enc = new System.Windows.Media.Imaging.PngBitmapEncoder();
            enc.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(bmp));

            using (var stm = System.IO.File.Create(filename))
            {
                enc.Save(stm);
            }
        }


        //copy-paste example
        public void DrawRectangle()
        {
            // Add a Rectangle Element
            var myRect = new System.Windows.Shapes.Rectangle();
            myRect.Stroke = System.Windows.Media.Brushes.Black;
            myRect.Fill = System.Windows.Media.Brushes.SkyBlue;
            myRect.HorizontalAlignment = HorizontalAlignment.Left;
            myRect.VerticalAlignment = VerticalAlignment.Center;
            Canvas.SetLeft(myRect, 100);
            myRect.Height = 50;
            myRect.Width = 50;
            myCanvas.Children.Add(myRect);
        }

        //copy-paste example
        public void DrawLine()
        {
            // Add a Line Element
            var myLine = new Line();
            myLine.Stroke = System.Windows.Media.Brushes.LightSteelBlue;
            myLine.X1 = 1;
            myLine.X2 = 50;
            myLine.Y1 = 1;
            myLine.Y2 = 50;
            myLine.HorizontalAlignment = HorizontalAlignment.Left;
            myLine.VerticalAlignment = VerticalAlignment.Center;
            myLine.StrokeThickness = 2;
            myCanvas.Children.Add(myLine);
        }
    }
}