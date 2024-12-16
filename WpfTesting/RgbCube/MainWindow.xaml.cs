using System;
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

namespace RgbCube
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private enum ColorScheme
        {
            RGB,
            RBG,
            GRB,
            GBR,
            BRG,
            BGR
        }

        public MainWindow()
        {
            InitializeComponent();

            DrawRgbLayers();
            //DrawRgbLayers(ColorScheme.RGB);
            //DrawRgbLayers(ColorScheme.RBG);
            //DrawRgbLayers(ColorScheme.GRB);
            //DrawRgbLayers(ColorScheme.GBR);
            //DrawRgbLayers(ColorScheme.BRG);
            //DrawRgbLayers(ColorScheme.BGR);
        }

        /// <summary>
        /// Produces 256 images of 256x256 pixels of RGB. Red on y-axis, green on x-axis, blue on z-axis (subsequent images).
        /// <para>(255,0,0) max red = lower left, every image</para>
        /// <para>(0,255,0) max green = upper right, every image</para>
        /// <para>(0,0,255) max blue = upper left, last image</para>
        /// <para>(0,0,0) black = upper left, first image</para>
        /// <para>(255,255,255) white = lower right, last image</para>
        /// </summary>
        private void DrawRgbLayers()
        {
            DrawRgbLayers(ColorScheme.RGB);
        }

        /// <summary>
        /// Produces 256 images of 256x256 pixels of color combinations. Color 1 is on y-axis, color 2 on x-axis, color 3 is added on subsequent images simulating z-axis. Default combination is RGB.
        /// <para>(255,0,0) max color 1 = lower left, every image</para>
        /// <para>(0,255,0) max color 2 = upper right, every image</para>
        /// <para>(0,0,255) max color 3 = upper left, last image</para>
        /// <para>(0,0,0) black = upper left, first image</para>
        /// <para>(255,255,255) white = lower right, last image</para>
        /// </summary>
        /// <param name="scheme"></param>
        private void DrawRgbLayers(ColorScheme scheme)
        {
            double size = 1;
            double horizontalOffset = 0;
            double verticalOffset = 0;
            string filename = string.Empty;

            List<byte> colorRange = new List<byte>();
            for (int i = 0; i < 256; i++)
            {
                colorRange.Add((byte)i);
            }

            DrawColorGrid(size, horizontalOffset, verticalOffset, filename, colorRange, scheme);
        }

        private void DrawColorGrid(double size, double horizontalOffset, double verticalOffset, string filename, List<byte> colorRange, ColorScheme scheme)
        {
            switch (scheme)
            {
                case ColorScheme.RGB:
                    foreach (var b in colorRange)
                    {
                        foreach (var r in colorRange)
                        {
                            foreach (var g in colorRange)
                            {
                                DrawCell(size, horizontalOffset, verticalOffset, r, g, b);
                                horizontalOffset += size;
                            }
                            verticalOffset += size;
                            horizontalOffset = 0;
                        }
                        filename = $"colorCube{scheme}-{b}.png";
                        SaveCanvasToFile(this, myCanvas, 96, filename);
                        verticalOffset = 0;

                        //clears canvas, but not the memory - it grows and grows until app crashes
                        myCanvas.Children.Clear();

                        // works
                        myCanvas.UpdateLayout();
                    }
                    break;
                case ColorScheme.RBG:
                    foreach (var g in colorRange)
                    {
                        foreach (var r in colorRange)
                        {
                            foreach (var b in colorRange)
                            {
                                DrawCell(size, horizontalOffset, verticalOffset, r, g, b);
                                horizontalOffset += size;
                            }
                            verticalOffset += size;
                            horizontalOffset = 0;
                        }
                        filename = $"colorCube{scheme}-{g}.png";
                        SaveCanvasToFile(this, myCanvas, 96, filename);
                        verticalOffset = 0;

                        myCanvas.Children.Clear();
                        myCanvas.UpdateLayout();
                    }
                    break;
                case ColorScheme.GRB:
                    foreach (var b in colorRange)
                    {
                        foreach (var g in colorRange)
                        {
                            foreach (var r in colorRange)
                            {
                                DrawCell(size, horizontalOffset, verticalOffset, r, g, b);
                                horizontalOffset += size;
                            }
                            verticalOffset += size;
                            horizontalOffset = 0;
                        }
                        filename = $"colorCube{scheme}-{b}.png";
                        SaveCanvasToFile(this, myCanvas, 96, filename);
                        verticalOffset = 0;

                        myCanvas.Children.Clear();
                        myCanvas.UpdateLayout();
                    }
                    break;
                case ColorScheme.GBR:
                    foreach (var r in colorRange)
                    {
                        foreach (var g in colorRange)
                        {
                            foreach (var b in colorRange)
                            {
                                DrawCell(size, horizontalOffset, verticalOffset, r, g, b);
                                horizontalOffset += size;
                            }
                            verticalOffset += size;
                            horizontalOffset = 0;
                        }
                        filename = $"colorCube{scheme}-{r}.png";
                        SaveCanvasToFile(this, myCanvas, 96, filename);
                        verticalOffset = 0;

                        myCanvas.Children.Clear();
                        myCanvas.UpdateLayout();
                    }
                    break;
                case ColorScheme.BRG:
                    foreach (var g in colorRange)
                    {
                        foreach (var b in colorRange)
                        {
                            foreach (var r in colorRange)
                            {
                                DrawCell(size, horizontalOffset, verticalOffset, r, g, b);
                                horizontalOffset += size;
                            }
                            verticalOffset += size;
                            horizontalOffset = 0;
                        }
                        filename = $"colorCube{scheme}-{g}.png";
                        SaveCanvasToFile(this, myCanvas, 96, filename);
                        verticalOffset = 0;

                        myCanvas.Children.Clear();
                        myCanvas.UpdateLayout();
                    }
                    break;
                case ColorScheme.BGR:
                    foreach (var r in colorRange)
                    {
                        foreach (var b in colorRange)
                        {
                            foreach (var g in colorRange)
                            {
                                DrawCell(size, horizontalOffset, verticalOffset, r, g, b);
                                horizontalOffset += size;
                            }
                            verticalOffset += size;
                            horizontalOffset = 0;
                        }
                        filename = $"colorCube{scheme}-{r}.png";
                        SaveCanvasToFile(this, myCanvas, 96, filename);
                        verticalOffset = 0;

                        myCanvas.Children.Clear();
                        myCanvas.UpdateLayout();
                    }
                    break;
                default:
                    foreach (var b in colorRange)
                    {
                        foreach (var r in colorRange)
                        {
                            foreach (var g in colorRange)
                            {
                                DrawCell(size, horizontalOffset, verticalOffset, r, g, b);
                                horizontalOffset += size;
                            }
                            verticalOffset += size;
                            horizontalOffset = 0;
                        }
                        filename = $"rgbCube{b}.png";
                        SaveCanvasToFile(this, myCanvas, 96, filename);
                        verticalOffset = 0;

                        myCanvas.Children.Clear();
                        myCanvas.UpdateLayout();
                    }
                    break;
            }

        }

        private void DrawCell(double size, double horizontalOffset, double verticalOffset, byte r, byte g, byte b)
        {
            Rectangle cell = new Rectangle();
            cell.Width = size;
            cell.Height = size;
            cell.Fill = new SolidColorBrush(Color.FromRgb(r, g, b));
            Canvas.SetLeft(cell, horizontalOffset);
            Canvas.SetTop(cell, verticalOffset);
            myCanvas.Children.Add(cell);
        }

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
    }
}
