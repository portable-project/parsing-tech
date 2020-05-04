using System;
using System.Collections.Generic;
using System.Text;

namespace marpa_impl
{
    public class MarpaParser
    {
        private Grammar _grammar;
        private ErrorHandler _errorHandler;
        private Recogniser _recogniser;
        private Parser _parser;

        public MarpaParser(Grammar grammar)
        {
            _errorHandler = new ErrorHandler();
            CheckGrammar(grammar);
            if (IsGrammarPrepared())
            {
                _grammar = grammar;
                _recogniser = new Recogniser(_grammar, _errorHandler);
                _parser = new Parser(_grammar, _errorHandler);
            }

        }

        public RecogniserReport CheckString(String input)
        {
            bool result = false;
            if (_recogniser != null)
            {
                result = _recogniser.Recognise(input);
            }
            return new RecogniserReport(result, _errorHandler.GetErrorDescriptionList());
        }

        public void ParseString()
        {
            // TODO
        }

        private void CheckGrammar(Grammar grammar)
        {
            if (grammar == null)
            {
                _errorHandler.AddNewError(ErrorCode.NO_GRAMMAR, grammar);
                return;
            }
            else if (!grammar.IsGrammarValid())
            {
                GrammarReport er = grammar.PrecomputeGrammar();
                if (!er.isSuccessfull)
                {
                    _errorHandler.AddNewError(ErrorCode.INCORRECT_GRAMMAR, grammar);
                    return;
                }
            }
        }
        private bool IsGrammarPrepared()
        {
            return _errorHandler.GetErrorDescriptionList().Count == 0;
        }
    }
}
