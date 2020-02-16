using System;
using System.Collections.Generic;
using System.Text;

namespace marpa_impl
{
    public class Recogniser
    {
        private readonly Grammar Grammar;
        public Recogniser(Grammar grammar)
        {
            if (!grammar.IsGrammarValid())
            {
                throw new Exception(ErrorHandler.getErrorMessageByCode(ErrorCode.INCOMPLETE_GRAMMAR));
            }
            Grammar = grammar;
        }

        public void Parse(String input)
        {

        }

    }
}
