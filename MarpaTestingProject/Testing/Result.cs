using System;
using System.Collections.Generic;
using System.Text;

namespace MarpaTestingProject
{
    struct Result
    {
        public string input;
        public long mstime;

        public Result(string Input, long MsTime)
        {
            mstime = MsTime;
            input = Input;
        }
    }
}
