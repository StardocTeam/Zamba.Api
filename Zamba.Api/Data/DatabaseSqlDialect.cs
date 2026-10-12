using System.Data.Common;
using System.Text.RegularExpressions;
using Oracle.ManagedDataAccess.Client;

namespace Zamba.Api.Data;

public sealed class DatabaseSqlDialect
{
    private DatabaseSqlDialect(bool isOracle)
    {
        IsOracle = isOracle;
    }

    public bool IsOracle { get; }

    public static DatabaseSqlDialect FromProviderName(string? providerName)
    {
        if (providerName?.StartsWith("Oracle.", StringComparison.Ordinal) == true)
        {
            return new DatabaseSqlDialect(true);
        }

        if (providerName?.StartsWith("Microsoft.EntityFrameworkCore.SqlServer", StringComparison.Ordinal) == true)
        {
            return new DatabaseSqlDialect(false);
        }

        throw new InvalidOperationException($"Unsupported EF Core provider '{providerName ?? "<null>"}'.");
    }

    public static DatabaseSqlDialect FromConnection(DbConnection connection)
    {
        var connectionNamespace = connection.GetType().Namespace;
        if (connectionNamespace?.StartsWith("Oracle.ManagedDataAccess", StringComparison.Ordinal) == true)
        {
            return new DatabaseSqlDialect(true);
        }

        if (connectionNamespace?.StartsWith("Microsoft.Data.SqlClient", StringComparison.Ordinal) == true)
        {
            return new DatabaseSqlDialect(false);
        }

        throw new InvalidOperationException($"Unsupported database connection '{connection.GetType().FullName}'.");
    }

    public string QuoteIdentifier(string identifier) => IsOracle
        ? $"\"{identifier.Replace("\"", "\"\"", StringComparison.Ordinal)}\""
        : $"[{identifier.Replace("]", "]]", StringComparison.Ordinal)}]";

    public string QuoteAlias(string alias) => IsOracle
        ? $"\"{alias.Replace("\"", "\"\"", StringComparison.Ordinal)}\""
        : $"[{alias.Replace("]", "]]", StringComparison.Ordinal)}]";

    public string Parameter(string name) => IsOracle ? $":{name}" : $"@{name}";

    public string CommandParameterName(string name) => IsOracle ? name : $"@{name}";

    public string PrepareCommandText(string sql)
    {
        if (!IsOracle)
        {
            return sql;
        }

        var prepared = Regex.Replace(sql, @"\bAS\s+\[([^\]]+)\]", match =>
            $"AS \"{match.Groups[1].Value.Replace("\"", "\"\"", StringComparison.Ordinal)}\"");
        prepared = Regex.Replace(prepared, @"\[([^\]]+)\]", match =>
            $"\"{match.Groups[1].Value.ToUpperInvariant()}\"");
        prepared = Regex.Replace(prepared, @"@([A-Za-z_][A-Za-z0-9_]*)", ":$1");
        return prepared.TrimEnd().TrimEnd(';');
    }

    public static void ConfigureCommand(DbCommand command)
    {
        if (command is OracleCommand oracleCommand)
        {
            oracleCommand.BindByName = true;
        }
    }

    public string SelectWithLimit(string parameterName) => IsOracle
        ? "SELECT "
        : $"SELECT TOP ({Parameter(parameterName)}) ";

    public string FetchFirst(string parameterName) => IsOracle
        ? $" FETCH FIRST {Parameter(parameterName)} ROWS ONLY"
        : string.Empty;

    public const string CurrentTimestamp = "CURRENT_TIMESTAMP";

    public string CastAsText(string expression, int length = 20) => IsOracle
        ? $"TO_CHAR({expression})"
        : $"CONVERT(varchar({length}), {expression})";

    public string CastAsFloat(string expression) => IsOracle
        ? $"CAST({expression} AS BINARY_DOUBLE)"
        : $"CONVERT(float, {expression})";

    public string ColumnsQuery => IsOracle
        ? "SELECT OWNER AS TABLE_SCHEMA, TABLE_NAME, COLUMN_NAME FROM ALL_TAB_COLUMNS WHERE OWNER = SYS_CONTEXT('USERENV', 'CURRENT_SCHEMA') ORDER BY OWNER, TABLE_NAME, COLUMN_ID"
        : "SELECT TABLE_SCHEMA, TABLE_NAME, COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS ORDER BY TABLE_SCHEMA, TABLE_NAME, ORDINAL_POSITION";
}