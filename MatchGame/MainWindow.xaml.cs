using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MatchGame;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    DispatcherTimer timer = new DispatcherTimer();
    int tenthsOfSecondsElapsed;
    int matchesFound;
    List<float> gameResults = new List<float>();
    // последний нажатый блок текста
    TextBlock lastTextBlockClicked;
    // флаг отслеживающий совпадения 
    bool findingMatch;

    public MainWindow()
    {
        InitializeComponent();
        timer.Interval = TimeSpan.FromSeconds(.1);
        timer.Tick += Timer_Tick;
        SetUpGame();
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        tenthsOfSecondsElapsed--;
        TimeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
        if (matchesFound == 8 || tenthsOfSecondsElapsed == 0)
        {
            timer.Stop();
            gameResults.Add(tenthsOfSecondsElapsed);

            TimeTextBlock.Text = TimeTextBlock.Text + " - Play again? \nmax time: " + gameResults.Max()/10;
        }
    }

    private void SetUpGame()
    {
        List<string> animalEmoji = new List<string>()
        {
            "🦒", "🦒",
            "🐍", "🐍",
            "🐘", "🐘",
            "🐖", "🐖",
            "🦧", "🦧",
            "🦂", "🦂",
            "🐕", "🐕",
            "🦜", "🦜",

        };

        Random rand = new Random();

        foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
        {
            if (textBlock.Name == "TimeTextBlock")
            {
                continue;
            }
            textBlock.Visibility = Visibility.Visible;
            int index = rand.Next(animalEmoji.Count);
            textBlock.Text = animalEmoji[index];
            animalEmoji.RemoveAt(index);
        }
        timer.Start();
        tenthsOfSecondsElapsed = 100;
        matchesFound = 0;
    }

    private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
    {
        TextBlock textBlock = sender as TextBlock;
        if (findingMatch == false)
        {
            textBlock.Visibility = Visibility.Hidden;
            lastTextBlockClicked = textBlock;
            findingMatch = true;
        }
        else if (textBlock.Text == lastTextBlockClicked.Text)
        {
            lastTextBlockClicked.Visibility = Visibility.Hidden;
            // let all the couples disappear
            matchesFound++;
            textBlock.Visibility = Visibility.Hidden;
            findingMatch = false;
        }
        else
        {
            lastTextBlockClicked.Visibility = Visibility.Visible;
            findingMatch = false;
        }
    }

    private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (matchesFound == 8 || tenthsOfSecondsElapsed == 0)
        {
            SetUpGame();
        }
    }
}