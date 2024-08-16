
using it.unical.mat.embasp.@base;
using System;


namespace Band.Otamatone.Mazer.Utils
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
