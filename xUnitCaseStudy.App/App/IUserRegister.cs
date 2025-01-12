using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xUnitCaseStudy.App
{
    public interface IUserInput
    {
        string GetInput();
    }

    public interface IUserOutput
    {
        void WriteOutput(string message);
    }

    public interface IUserRegister
    {
        void RegisterUser();
    }
}
