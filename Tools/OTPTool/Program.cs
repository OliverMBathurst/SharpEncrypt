﻿namespace OTPTool
{
    internal sealed class Program
    {
        static void Main(string[] args) => new ArgumentsHandler(args).Execute();
    }
}