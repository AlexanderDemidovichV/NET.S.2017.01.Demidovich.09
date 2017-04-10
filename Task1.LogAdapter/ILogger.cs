using System;
using System.ComponentModel;

namespace Task1.LogAdapter
{
    public interface ILogger
    {
        void Trace([Localizable(false)] string message);
        void Trace([Localizable(false)] string message, Exception exception, params object[] args);
        void Trace([Localizable(false)] string message, params object[] args);
        void Debug([Localizable(false)] string message);
        void Debug([Localizable(false)] string message, Exception exception, params object[] args);
        void Debug([Localizable(false)] string message, params object[] args);
        void Info([Localizable(false)] string message);
        void Info([Localizable(false)] string message, Exception exception, params object[] args);
        void Info([Localizable(false)] string message, params object[] args);
        void Warn([Localizable(false)] string message);
        void Warn([Localizable(false)] string message, Exception exception, params object[] args);
        void Warn([Localizable(false)] string message, params object[] args);
        void Error([Localizable(false)] string message);
        void Error([Localizable(false)] string message, Exception exception, params object[] args);
        void Error([Localizable(false)] string message, params object[] args);
        void Fatal([Localizable(false)] string message);
        void Fatal([Localizable(false)] string message, Exception exception, params object[] args);
        void Fatal([Localizable(false)] string message, params object[] args);
    }
}
