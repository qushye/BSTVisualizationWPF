using System.Windows;
using System.Windows.Input;

namespace BSTVisualizationWPF
{
    public partial class MainWindow : Window
    {

        public static readonly RoutedUICommand RunCommand =
            new RoutedUICommand("Запуск", "RunCommand", typeof(MainWindow));

        public static readonly RoutedUICommand NextCommand =
            new RoutedUICommand("Вперёд", "NextCommand", typeof(MainWindow));

        public static readonly RoutedUICommand PrevCommand =
            new RoutedUICommand("Назад", "PrevCommand", typeof(MainWindow));

        public static readonly RoutedUICommand AboutCommand =
            new RoutedUICommand("Справка", "AboutCommand", typeof(MainWindow));

        private int _currentStep = 0;
        private int _totalSteps = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void RunCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _totalSteps = 6;
            _currentStep = 1;
            UpdateUI();


            lstTraversal.Items.Clear();
            lstTraversal.Items.Add("Тип обхода: Симметричный (LNR)");
            lstTraversal.Items.Add("─────────────────────────");
            lstTraversal.Items.Add("20 → 40 → 50 → 60 → 70 → 80");

            txtCanvasPlaceholder.Text = "Дерево построено. Шаги активны.";
        }


        private void PrevCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _currentStep--;
            UpdateUI();
        }

        private void PrevCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (_currentStep > 1);
        }

        private void NextCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _currentStep++;
            UpdateUI();
        }

        private void NextCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (_totalSteps > 0 && _currentStep < _totalSteps);
        }

        private void UpdateUI()
        {
            tbStepInfo.Text = $"Шаг: {_currentStep} / {_totalSteps}";
            tbStatus.Text = $"Выполняется шаг {_currentStep}";

            switch (_currentStep)
            {
                case 1: txtStepDescription.Text = "Исходное бинарное дерево построено."; break;
                case 2: txtStepDescription.Text = $"Поиск узла со значением {txtDeleteValue.Text} для удаления..."; break;
                case 3: txtStepDescription.Text = $"Узел {txtDeleteValue.Text} найден. Проверяем наличие потомков."; break;
                case 4: txtStepDescription.Text = $"Узел {txtDeleteValue.Text} успешно удалён. Балансировка дерева."; break;
                case 5: txtStepDescription.Text = "Начало обхода дерева по алгоритму LNR."; break;
                case 6: txtStepDescription.Text = "Обход дерева успешно завершён!"; break;
            }
        }

        private void AboutCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show(
                "Бинарное дерево поиска (BST) — иерархическая структура данных.\n\n" +
                "Свойство BST:\nЛевые потомки < Корень < Правые потомки.",
                "Описание алгоритма", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ExitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void PresetSimple_Click(object sender, RoutedEventArgs e) => txtElements.Text = "50, 30, 70, 20, 40";
        private void PresetBalanced_Click(object sender, RoutedEventArgs e) => txtElements.Text = "50, 25, 75, 10, 30, 60, 90";
        private void PresetDegenerate_Click(object sender, RoutedEventArgs e) => txtElements.Text = "10, 20, 30, 40, 50";
    }
}