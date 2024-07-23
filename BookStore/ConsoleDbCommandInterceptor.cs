using System;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using Microsoft.EntityFrameworkCore.Diagnostics;

public class MyDbCommandInterceptor : DbCommandInterceptor
{
    private readonly Stopwatch _stopwatch = new Stopwatch();

    public override InterceptionResult<DbDataReader> ReaderExecuting(
        DbCommand command,
        CommandEventData eventData,
        InterceptionResult<DbDataReader> result)
    {
        LogCommand(command);
        return base.ReaderExecuting(command, eventData, result);
    }

    private void LogCommand(DbCommand command)
    {
        if (command == null)
        {
            return;
        }

        _stopwatch.Restart();

        using (var writer = new StreamWriter("log.txt", true))
        {
            writer.WriteLine($"Executing SQL command: {command.CommandText}");
            foreach (DbParameter param in command.Parameters)
            {
                writer.WriteLine($"  {param.ParameterName} = {param.Value}");
            }
        }
    }
}

