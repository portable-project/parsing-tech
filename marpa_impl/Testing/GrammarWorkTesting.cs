﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace marpa_impl
{
    public class GrammarWorkTesting
    {
        public void CalculateTimeOfRecognise(string filePath, Grammar grammar, List<String> inputList)
        {
            ErrorHandler errorHandler = new ErrorHandler();
            if (grammar.IsGrammarValid())
            {
                Recogniser recogniser = new Recogniser(grammar, errorHandler);
                FileWorker.CleanFile(filePath);

                inputList.ForEach(input =>
                {
                    Stopwatch timePerParse = Stopwatch.StartNew();
                    bool result = recogniser.Recognise(input);
                    timePerParse.Stop();
                    long mstime = timePerParse.ElapsedMilliseconds;

                    if (result) FileWorker.WriteToCSVFile(filePath, input, mstime);
                });
            }
        }
        private static void DisplayTimerProperties()
        {
            // Display the timer frequency and resolution.
            if (Stopwatch.IsHighResolution)
            {
                Console.WriteLine("Operations timed using the system's high-resolution performance counter.");
            }
            else
            {
                Console.WriteLine("Operations timed using the DateTime class.");
            }

            long frequency = Stopwatch.Frequency;
            Console.WriteLine("  Timer frequency in ticks per second = {0}",
                frequency);
            long nanosecPerTick = (1000L * 1000L * 1000L) / frequency;
            Console.WriteLine("  Timer is accurate within {0} nanoseconds",
                nanosecPerTick);
        }
    }
}