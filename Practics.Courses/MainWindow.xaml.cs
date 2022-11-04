using System.Windows;
using Practics.Courses.Contexts;
using Practics.Courses.Services;
using Practics.Courses.Windows;

namespace Practics.Courses
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly ApplicationContext _context;
        private readonly PdfService _pdfService = new PdfService();
        
        public MainWindow()
        {
            _context = new ApplicationContext();
            
            InitializeComponent();
        }

        private void PdfButton_OnClick(object sender, RoutedEventArgs e)
        {
            _pdfService.ProcessPdf();
        }

        private void PersonButton_OnClick(object sender, RoutedEventArgs e)
        {
            var window = new PersonDataGrid(_context);

            window.Show();
        }

        private void PriceButton_OnClick(object sender, RoutedEventArgs e)
        {
            var window = new CoursePriceDataGrid(_context);
            
            window.Show();
        }
    }
}