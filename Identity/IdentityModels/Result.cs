using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityModels
{
    public class Result
    {
        public bool Status { get; set; }
        public string Text { get; set; }
        public List<ValidationFailure> ErrorList { get; set; }
        public Result()
        {
            ErrorList = new List<ValidationFailure>();
        }
        public void AddError(string error)
        {
            ErrorList.Add(new ValidationFailure("", error));
        }
    }
}
