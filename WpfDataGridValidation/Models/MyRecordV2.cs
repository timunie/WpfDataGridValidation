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
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(Name)));
                NotifyOfPropertyChange(nameof(Name));
                NotifyOfPropertyChange(nameof(HasErrors));
            }
        }

        public bool HasErrors => _errors.Any();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName) || !_errors.TryGetValue(propertyName, out List<string> value))
                return null;
            return value;
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
        }
    }
}
