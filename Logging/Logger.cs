namespace Logging
{
    public enum LogLevel
    {
        Info,
        Warning,
        Error
    }

    public class LoggingModule
    {
        private static readonly string logFilePath = "log.txt";
        private static readonly Queue<string> bufferedMessages = new Queue<string>();
        private static readonly object bufferLock = new object();
        private static readonly object fileWriteLock = new object();
        private static bool isFlushingBuffer = false;

        public static void LogInfo(string message)
        {
            string formattedMessage = FormatLogMessage(LogLevel.Info, message);
            lock (bufferLock)
            {
                bufferedMessages.Enqueue(formattedMessage);
                if (!isFlushingBuffer)
                {
                    isFlushingBuffer = true;
                    ThreadPool.QueueUserWorkItem(_ => FlushBufferedMessages());
                }
            }
        }

        public static void LogWarning(string message)
        {
            string formattedMessage = FormatLogMessage(LogLevel.Warning, message);
            WriteToFile(formattedMessage);
        }

        public static void LogError(string message)
        {
            string formattedMessage = FormatLogMessage(LogLevel.Error, message);
            WriteToFile(formattedMessage);
        }

        private static string FormatLogMessage(LogLevel level, string message)
        {
            return $"{DateTime.Now} [{level}]: {message}";
        }

        private static void WriteToFile(string formattedMessage)
        {
            lock (fileWriteLock)
            {
                using (StreamWriter writer = File.AppendText(logFilePath))
                {
                    writer.WriteLine(formattedMessage);
                }
            }
        }

        private static void FlushBufferedMessages()
        {
            while (true)
            {
                string[] messagesToWrite;
                lock (bufferLock)
                {
                    if (bufferedMessages.Count == 0)
                    {
                        isFlushingBuffer = false;
                        return;
                    }

                    messagesToWrite = bufferedMessages.ToArray();
                    bufferedMessages.Clear();
                }

                foreach (var message in messagesToWrite)
                {
                    WriteToFile(message);
                }
            }
        }
    }
}