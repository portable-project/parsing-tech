using System;
using System.Collections.Generic;
using System.Text;

namespace marpa_impl
{
    public interface IMarpaParser
    {
        RecogniserReport CheckString(String input);
        RecogniserReport CheckUpdatedString(String updatedInput);
        ParserReport ParseString(String input);
        ParserReport ParseUpdatedString(String updatedInput);
        ParseInfoReport GetLastParseInformationOnSymbolPosition(int symbolPosition);
        ParseInfoReport GetLastParseInformationOnError();
    }

    public class MarpaParser : IMarpaParser
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

        public RecogniserReport CheckUpdatedString(String updatedInput)
        {
            bool result = false;
            if (_recogniser != null)
            {
                result = _recogniser.UpdateRecognise(updatedInput);
            }
            return new RecogniserReport(result, _errorHandler.GetErrorDescriptionList());
        }

        public ParseInfoReport GetLastParseInformationOnSymbolPosition(int symbolPosition)
        {
            if (_recogniser != null)
            {
                return _recogniser.GetLastParseInformation(symbolPosition);
            }
            else return new ParseInfoReport(new ErrorDescription(ErrorCode.NO_GRAMMAR));
        }

        public ParserReport ParseString(String input)
        {
            RecogniserReport report = CheckString(input);
            if (_parser != null && report.isSuccessfull && report.isRecognised)
            {
                _parser.Parse(_recogniser.GetResultSetList(), _recogniser.FindFinalItems());
            }
            return new ParserReport();
        }

        public ParserReport ParseUpdatedString(String updatedInput)
        {
            // TODO
            return new ParserReport();
        }

        public ParseInfoReport GetLastParseInformationOnError()
        {
            // TODO
            return new ParseInfoReport();
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
