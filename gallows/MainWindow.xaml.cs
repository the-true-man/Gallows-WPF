using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace gallows
{
    public partial class MainWindow : Window
    {
        List<string> countries = new List<string>()
        {
            "Россия",
            "Финляндия",
            "Беларуссия",
            "Казахстан",
            "Грузия",
            "Латвия",
            "Литва",
            "Эстония",
            "Молдавия",
            "Индия",
            "Япония",
            "Швеция",
            "Чехия",
            "Америка",
            "Турция",
            "Испания",
            "Италия",
            "Германия",
            "Франция"
        };
        List<char> alphabet = new List<char> { 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И', 'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Э', 'Ю', 'Я' };
        static List<System.Windows.Controls.Label> labels = new List<System.Windows.Controls.Label>();
        static List<System.Windows.Controls.Label> labels2 = labels;
        
        int numberOfLines;
        public MainWindow()
        {

            InitializeComponent();
            restart_game();

        }
        int true_answer = 0;
        int false_answer = 0;
        private void restart_game()
        {

            numberOfLines = 0;
            true_answer = 0;
            false_answer = 0;
            canvas_1.Visibility = Visibility.Collapsed;
            canvas_2.Visibility = Visibility.Collapsed;
            canvas_3.Visibility = Visibility.Collapsed;
            canvas_4.Visibility = Visibility.Collapsed;
            canvas_5.Visibility = Visibility.Collapsed;
            canvas_6.Visibility = Visibility.Collapsed;
            canvas_7.Visibility = Visibility.Collapsed;
            cann.Children.Clear();
            labels = labels2;
            Path path = new Path();
            path.Stroke = Brushes.Black;
            path.StrokeThickness = 1;

            PathGeometry pathGeometry = new PathGeometry();
            PathFigure pathFigure = new PathFigure();

            pathFigure.StartPoint = new Point(965, -137);

            ArcSegment arcSegment = new ArcSegment();
            arcSegment.Point = new Point(995, -137);
            arcSegment.Size = new Size(100, 100);
            arcSegment.SweepDirection = SweepDirection.Clockwise;

            pathFigure.Segments.Add(arcSegment);
            pathGeometry.Figures.Add(pathFigure);
            path.Data = pathGeometry;

            canvas_2.Children.Add(path);
            Random rnd = new Random();
            string word = countries[rnd.Next(countries.Count)];
            char[] chars_in_word = word.ToCharArray();
            numberOfLines = word.Length;
            double startX = -150;
            double startY = 90;
            double lineSpacing = 90;
            for (int i = 0; i < numberOfLines; i++)
            {
                StackPanel stackPanel = new StackPanel();
                stackPanel.Margin = new Thickness(15, 0, 15, 0);
                stackPanel.Orientation = Orientation.Vertical;
                Line line = new Line();
                line.Stroke = Brushes.Black;
                line.StrokeThickness = 3;
                line.X1 = startX + 50 + lineSpacing;
                line.Y1 = startY;
                line.X2 = startX + 25 + lineSpacing + lineSpacing;
                line.Y2 = startY;
                line.HorizontalAlignment = HorizontalAlignment.Center;
                line.VerticalAlignment = VerticalAlignment.Center;


                System.Windows.Controls.Label label = new System.Windows.Controls.Label();
                label.Content = chars_in_word[i];
                label.Visibility = Visibility.Collapsed;
                label.Foreground = Brushes.Black;
                label.FontSize = 60;
                label.Margin = new Thickness(0, 0, 0, -90);
                labels.Add(label);
                stackPanel.Children.Add(label);
                stackPanel.Children.Add(line);
                
                cann.Children.Add(stackPanel);
                
            }
            labels2 = labels;
        }
        List<ToggleButton> press_button = new List<ToggleButton>();
        private async void  ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            
            ToggleButton toggleButton = sender as ToggleButton;
            press_button.Add(toggleButton);
            toggleButton.IsEnabled = false;
            string char_on_button = toggleButton.Content.ToString().ToLower();
            string word = "";
            foreach (System.Windows.Controls.Label label in labels)
            {
                word += label.Content.ToString().ToLower();
            }
            if (!word.Contains(char_on_button))
            {
                false_answer++;
                if(false_answer == 1)
                {
                    canvas_1.Visibility = Visibility.Visible;
                }
                else if(false_answer == 2)
                {
                    canvas_2.Visibility = Visibility.Visible;  
                }
                else if (false_answer == 3)
                {
                    canvas_3.Visibility = Visibility.Visible;
                }
                else if (false_answer == 4)
                {
                    canvas_4.Visibility = Visibility.Visible;
                }

                else if (false_answer == 5)
                {
                    canvas_5.Visibility = Visibility.Visible;
                }

                else if (false_answer == 6)
                {
                    canvas_6.Visibility = Visibility.Visible;
                }
                else if (false_answer == 7)
                {
                    canvas_7.Visibility = Visibility.Visible;
                    MessageBox.Show("Вы проиграли!");
                    foreach (System.Windows.Controls.Label label1 in labels)
                    {
                        label1.Visibility = Visibility.Visible;

                    }
                    await Task.Delay(1500);
                    labels.Clear();
                    foreach (ToggleButton toggleButton1 in press_button)
                    {
                        toggleButton1.IsEnabled = true;
                        toggleButton1.IsChecked = false;
                    }
                    restart_game();
                    return;
                }
                return;
            }
            foreach (System.Windows.Controls.Label label in labels)
            {
                if(label.Content.ToString().ToLower() == char_on_button.ToLower())
                {
                    label.Visibility = Visibility.Visible;
                    true_answer++;
                    if(true_answer == numberOfLines)
                    {
                        MessageBox.Show("Вы угадали слово!");
                        await Task.Delay(1500);
                        labels.Clear();
                        foreach(ToggleButton toggleButton1 in press_button)
                        {
                            toggleButton1.IsEnabled = true;
                            toggleButton1.IsChecked = false;
                        }    
                        restart_game();
                        
                        break;
                       
                    }
                }
            }
            
        }
    }
}
