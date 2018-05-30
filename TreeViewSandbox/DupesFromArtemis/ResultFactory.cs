using System;

namespace TreeViewSandbox.DupesFromArtemis
{
    public class ResultFactory
    {
        public IResult Ok()
        {
            return new Result() { Success = true };
        }

        public IResult OkMessage(string message)
        {
            return new Result() { Success = true, Message = message };
        }

        public IResult ErrorMessage(string message)
        {
            return new Result() { Success = false, Message = message };
        }

        public IResult NotImplemented(string message = "")
        {
            string fullMessage = string.Format("This feature has not yet been implemented. {0}", message);
            return new Result() { Success = false, Message = fullMessage };
        }

        public IResult Error(Exception ex)
        {
            return new Result() { Success = false, Message = ex.Message };
        }
    }


    public class ResultFactory<T>
    {
        public IResult<T> Ok(T outcome)
        {
            return new Result<T>() { Success = true, Outcome = outcome };
        }

        public IResult<T> OkMessage(string message, T outcome)
        {
            return new Result<T>() { Success = true, Message = message, Outcome = outcome };
        }

        public IResult<T> ErrorMessage(string message)
        {
            return new Result<T>() { Success = false, Message = message };
        }

        public IResult<T> NotImplemented(string message = "")
        {
            string fullMessage = string.Format("This feature has not yet been implemented. {0}", message);
            return new Result<T>() { Success = false, Message = fullMessage };
        }

        public IResult<T> Error(Exception ex)
        {
            return new Result<T>() { Success = false, Message = ex.Message };
        }

        private T GetDefaultInstanceOfT()
        {
            return (T)Activator.CreateInstance(typeof(T));
        }
    }
}
