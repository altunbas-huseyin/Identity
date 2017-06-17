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
        public Object Data { get; set; }
        public List<ValidationFailure> ErrorList { get; set; }
        public Result()
        {
            ErrorList = new List<ValidationFailure>();
        }
        public Result(Object data, bool status)
        {
            Data = data;
            Status = status;
        }
        public void AddError(string error)
        {
            ErrorList.Add(new ValidationFailure("", error));
        }
    }
}
