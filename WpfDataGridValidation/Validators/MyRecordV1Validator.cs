using FluentValidation;
using WpfDataGridValidation.Models;

namespace WpfDataGridValidation.Validators
{
    public class MyRecordV1Validator : AbstractValidator<MyRecordV1>
    {
        public MyRecordV1Validator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
