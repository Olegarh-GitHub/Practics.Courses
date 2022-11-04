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
    public partial class PersonDataGrid : Window
    {
        private readonly EntityService<Person> _personService;
        private readonly PersonValidator _validator;
        private bool _error;
        
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>()
        {
            { "LastName", "Фамилия" },
            { "FirstName", "Имя" },
            { "MiddleName", "Отчество" },
            { "Birthday", "День рождения" },
            { "IsMale", "Является мужчиной" },
        };
        
        public PersonDataGrid(ApplicationContext context)
        {
            _validator = new PersonValidator();
            _personService = new EntityService<Person>(context);
            
            InitializeComponent();
            FillPersons();

            PersonsDataGrid.AutoGeneratingColumn += FilterHeaders;
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
        
        private void FillPersons()
        {
            IEnumerable<Person> persons = _personService.Read();

            PersonsDataGrid.ItemsSource = persons;
        }

        private void CommitChanges(object sender, EventArgs args)
        {
            foreach (Person price in PersonsDataGrid.ItemsSource)
            {
                ValidationResult result = _validator.Validate(price);
                
                if (result.Success)
                    continue;
                
                string error = result.Exception.Message;
                MessageBox.Show(error);
                
                return;
            }

            var errors = _personService.SaveChanges();
        }
    }
}