using FluentValidation;
using WpfDataGridValidation.Models;

namespace WpfDataGridValidation.Validators
{
    public class MyRecordV2Validator : AbstractValidator<MyRecordV2>
    {
        public MyRecordV2Validator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
