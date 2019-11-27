using FluentValidation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using WpfDataGridValidation.Models;
using WpfDataGridValidation.Validators;

namespace WpfDataGridValidation
{
    public class MainWindowViewModel : PropertyChangedBase
    {
        public MainWindowViewModel()
        {
            RecordsV1 = new ObservableCollection<MyRecordV1>(Enumerable.Repeat(new MyRecordV1(new MyRecordV1Validator()), 1));
            RecordsV2 = new ObservableCollection<MyRecordV2>();
            for (int i = 0; i < 100; i++)
            {
                RecordsV2.Add(new MyRecordV2(new MyRecordV2Validator()));
            }
            
        }

        public ObservableCollection<MyRecordV1> RecordsV1 { get; set; }
        public ObservableCollection<MyRecordV2> RecordsV2 { get; set; }
    }
}
