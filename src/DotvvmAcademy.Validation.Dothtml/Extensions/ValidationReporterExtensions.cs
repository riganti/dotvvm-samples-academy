﻿using DotVVM.Framework.Compilation;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotvvmAcademy.Validation.Dothtml;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotvvmAcademy.Validation
{
    public static class ValidationReporterExtensions
    {
        public static void Report(
            this IValidationReporter reporter,
            string message,
            IAbstractTreeNode node,
            ValidationSeverity severity = default)
        {
            reporter.Report(message, Enumerable.Empty<object>(), node, severity);
        }

        public static void Report(
            this IValidationReporter reporter,
            string message,
            IEnumerable<object> arguments,
            IAbstractTreeNode node,
            ValidationSeverity severity = default)
        {
            if (node is IAbstractTreeNode)
            {
                reporter.Report(message, severity);
                return;
            }

            var sourceCode = reporter.SourceCodeStorage.GetSourceCode(node.TreeRoot);
            reporter.Report(new ResolverDothtmlDiagnostic(message, arguments, node, sourceCode, severity));
        }

        public static void Report(
            this IValidationReporter reporter,
            string message,
            DothtmlNode node,
            string fileName,
            ValidationSeverity severity = default)
        {
            reporter.Report(message, Enumerable.Empty<object>(), node, fileName, severity);
        }

        public static void Report(
            this IValidationReporter reporter,
            string message,
            IEnumerable<object> arguments,
            DothtmlNode node,
            string fileName,
            ValidationSeverity severity = default)
        {
            if (reporter.SourceCodeStorage.Sources.TryGetValue(fileName, out var source))
            {
                reporter.Report(new ParserDothtmlDiagnostic(message, arguments, node, (DothtmlSourceCode)source, severity));
            }
            else
            {
                reporter.Report(message, severity);
            }
        }

        public static void Report(this IValidationReporter reporter, DotvvmCompilationException exception, ISourceCode source)
        {
            reporter.Report(new ExceptionDiagnostic(exception, source));
        }

        public static void Report(
            this IValidationReporter reporter,
            TokenError error, 
            DothtmlSourceCode source)
        {
            reporter.Report(new TokenErrorDiagnostic(error, source, ValidationSeverity.Error));
        }
    }
}
