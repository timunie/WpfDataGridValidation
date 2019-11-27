using FluentValidation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDataGridValidation.Models
{
    public class MyRecordV2 : PropertyChangedBase, INotifyDataErrorInfo
    {
        private readonly AbstractValidator<MyRecordV2> _validator;
        private string _name = string.Empty;
        private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        public MyRecordV2(FluentValidation.AbstractValidator<MyRecordV2> validator)
        {
            _validator = validator;
            Validate();
            this.PropertyChanged += (s, a) => Console.WriteLine($"MyRecordV2 - PropertyChanged - {a.PropertyName}");
            this.ErrorsChanged += (s, a) => Console.WriteLine($"MyRecordV2 - ErrorsChanged - {a.PropertyName}");
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                Validate();
                NotifyOfPropertyChange(nameof(Name));
            }
        }


         int _age;
        public int Age
        {
            get => _age;
            set
            {
                _age = value;
                Validate();
                NotifyOfPropertyChange(nameof(Age));
            }
        }

        public bool HasErrors => _errors.Any();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            // This is to return all Errors
            if (propertyName == string.Empty)
            {
                return _errors.Values.SelectMany(x => x);
            }
            
            // This is to select an Error by Property
            if (propertyName == null || !_errors.TryGetValue(propertyName, out List<string> value))
            {
                return null;
            }
            else
            {
                return value;
            }    
            
        }

        private void Validate()
        {
            if (_validator == null)
                return;
            var previousKeys = _errors.Select(x => x.Key).ToList();
            _errors = _validator.Validate(this)
                .Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(x => x.Key, x => x.Select(y => y.ErrorMessage).ToList());

            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(Name)));
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(Age)));

            NotifyOfPropertyChange(nameof(HasErrors));
        }

        // Returns a List of all the errors
        public IEnumerable Errors => GetErrors(string.Empty); 
    }
}
