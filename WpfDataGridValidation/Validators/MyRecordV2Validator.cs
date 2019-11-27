using FluentValidation;
using WpfDataGridValidation.Models;

namespace WpfDataGridValidation.Validators
{
    public class MyRecordV2Validator : AbstractValidator<MyRecordV2>
    {
        public MyRecordV2Validator()
        {
            RuleFor(x => x.Name).Length(5, 10);
            RuleFor(x => x.Age).ExclusiveBetween(0, 150);
            RuleFor(x => x.Name).Matches("[a-zA-Z]+");
        }
    }
}
