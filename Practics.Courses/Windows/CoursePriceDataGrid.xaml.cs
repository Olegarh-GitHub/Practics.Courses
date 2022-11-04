using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Practics.Courses.Contexts;
using Practics.Courses.Models;
using Practics.Courses.Services;
using ValidationResult = Practics.Courses.Models.ValidationResult;

namespace Practics.Courses.Windows
{
    public partial class CoursePriceDataGrid : Window
    {
        private readonly EntityService<CoursePrice> _coursePriceService;
        private readonly CoursePriceValidator _validator;
        private bool _error;

        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>()
        {
            {"PriceWithTax", "Цена с налогом"},
            {"Price", "Цена"}
        };

        public CoursePriceDataGrid(ApplicationContext context)
        {
            _validator = new CoursePriceValidator();
            _coursePriceService = new EntityService<CoursePrice>(context);

            InitializeComponent();
            FillCoursePrices();

            CoursePricesDataGrid.AutoGeneratingColumn += FilterHeaders;
            CommitButtonChanges.Click += CommitChanges;
        }

        private void FilterHeaders(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            var header = e.Column.Header.ToString();
            if (!Headers.ContainsKey(header))
            {
                e.Cancel = true;
                return;
            }

            e.Column.Header = Headers[header];
        }
        
        private void FillCoursePrices()
        {
            IEnumerable<CoursePrice> coursePrices = _coursePriceService.Read();

            CoursePricesDataGrid.ItemsSource = coursePrices;
        }

        private void CommitChanges(object sender, EventArgs args)
        {
            foreach (CoursePrice price in CoursePricesDataGrid.ItemsSource)
            {
                ValidationResult result = _validator.Validate(price);
                
                if (result.Success)
                    continue;
                
                string error = result.Exception.Message;
                MessageBox.Show(error);
                
                return;
            }

            var errors = _coursePriceService.SaveChanges();
        }
    }
}