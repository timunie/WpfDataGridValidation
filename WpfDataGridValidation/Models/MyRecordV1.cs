using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace WpfDataGridValidation.Models
{
    public class MyRecordV1 : PropertyChangedBase, IDataErrorInfo
    {
        private readonly AbstractValidator<MyRecordV1> _validator;
        private string _name = string.Empty;
        private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        public MyRecordV1(FluentValidation.AbstractValidator<MyRecordV1> validator)
        {
            _validator = validator;
            Validate();
            this.PropertyChanged += (s, a) => Console.WriteLine($"MyRecordV1 - PropertyChanged - {a.PropertyName}");
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                Validate();
                NotifyOfPropertyChange(nameof(Name));
                NotifyOfPropertyChange(nameof(Error));
            }
        }

        private void Validate()
        {
            if (_validator == null)
                return;
            _errors = _validator.Validate(this)
                .Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(x => x.Key, x => x.Select(y => y.ErrorMessage).ToList());
        }

        public string this[string columnName]
        {
            get
            {
                if (string.IsNullOrEmpty(columnName) || !_errors.TryGetValue(columnName, out List<string> value))
                    return null;
                return value.FirstOrDefault();
            }
        }

        public string Error => _errors.Any() ? _errors.First().Value.FirstOrDefault() : null;
    }
}
