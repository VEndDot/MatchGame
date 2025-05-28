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

namespace MatchGame;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private int a = 12;
    public MainWindow()
    {
        InitializeComponent();

        SetUpGame();
        
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
            int index = rand.Next(animalEmoji.Count);
            textBlock.Text = animalEmoji[index];
            animalEmoji.RemoveAt(index);

        }
    }
}