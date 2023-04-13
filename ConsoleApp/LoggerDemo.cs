using Microsoft.Extensions.Logging;

namespace ConsoleApp
{
    public class LoggerDemo
    {
        private ILogger<LoggerDemo> logger;

        public LoggerDemo(ILogger<LoggerDemo> logger)
        {
            this.logger = logger;
        }

        public void Work()
        {
            using var loggerScope = logger.BeginScope("Work");

            logger.LogTrace("Being work");


            for (int i = 0; i < 10; i++)
            {
                using (var loggerInnerScope = logger.BeginScope($"Inner: {i + 1}"))
                {
                    try
                    {
                        logger.LogDebug(i.ToString());
                        if (i == 5)
                            throw new Exception("Index 5");
                    }

                    catch (Exception e)
                    {
                        logger.LogError(e, "wyjątek");
                    }
                }
            }


            logger.LogTrace("End work");
        }
    }
}
