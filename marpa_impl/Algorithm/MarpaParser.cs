using System;
using System.Collections.Generic;
using System.Text;

namespace marpa_impl
{
    public interface IMarpaParser
    {
        RecogniseReport CheckString(String input);
        RecogniseReport CheckUpdatedString(String updatedInput);
        AnalyseReport BuildParseTree(String input);
        ProcessDetailsReport GetLastParseInformationOnSymbolPosition(int symbolPosition);
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

        public RecogniseReport CheckString(String input)
        {
            bool result = false;
            if (_recogniser != null)
            {
                result = _recogniser.Recognise(input);
            }
            return new RecogniseReport(result, _errorHandler.GetErrorDescriptionList());
        }

        public RecogniseReport CheckUpdatedString(String updatedInput)
        {
            bool result = false;
            if (_recogniser != null)
            {
                result = _recogniser.UpdateRecognise(updatedInput);
            }
            return new RecogniseReport(result, _errorHandler.GetErrorDescriptionList());
        }

        public ProcessDetailsReport GetLastParseInformationOnSymbolPosition(int symbolPosition)
        {
            if (_recogniser != null)
            {
                return _recogniser.GetLastParseInformation(symbolPosition);
            }
            else return new ProcessDetailsReport(new ErrorDescription(ErrorCode.NO_GRAMMAR));
        }

        public AnalyseReport BuildParseTree(String input)
        {
            RecogniseReport report = CheckString(input);
            List<TreeNode> forest = null;
            if (_parser != null && report.isSuccessfull && report.isRecognised)
            {
                forest = _parser.Parse(_recogniser.GetResultSetList());
            }
            return new AnalyseReport(forest, report.isRecognised);
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
