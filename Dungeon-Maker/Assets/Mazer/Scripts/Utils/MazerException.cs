using it.unical.mat.embasp.@base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mazer.Utils
{
    public class MazerException : Exception
    {
        public MazerException() : base("Error: the program could not generate stable model.")
        {
    
        }

        public MazerException(Output output) : base($"Error: {output.errors}")
        {

        }

        public MazerException(string programName, Output output) : base($"Error on {programName}: {output.errors}")
        {

        }
    }
}
